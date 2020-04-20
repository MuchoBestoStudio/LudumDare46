using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	[DisallowMultipleComponent]
	public class PlayerMovements : CharacterMovements
	{
		#region Variables

		public EDirection NextMoveDirection { get; private set; } = EDirection.NONE;

		[SerializeField, Tooltip("")]
		private PlayerController	_controller = null;
		[SerializeField, Tooltip("")]
		private	PlayerAnimator		_animator = null;
		[SerializeField, Tooltip("")]
		private	PlayerInteractions	_interactions = null;

		private	List<EDirection>	_directionsPressed	=	new List<EDirection>();

		#endregion

		#region Events

		public Action onStopMoving = null;

		#endregion

		#region Methods

		private void OnEnable()
		{
			_controller.onMovementPerformed += PlayerController_OnMovementsPerformed;

			_interactions.onInteractionCompleted += PlayerInteractions_OnInteractionCompleted;
		}

		override protected void OnDisable()
		{
            base.OnDisable();

			_controller.onMovementPerformed -= PlayerController_OnMovementsPerformed;

			_interactions.onInteractionCompleted -= PlayerInteractions_OnInteractionCompleted;

		}

		private void PlayerController_OnMovementsPerformed(EDirection direction, bool started)
		{
			if (started)
			{
				Assert.IsFalse(_directionsPressed.Contains(direction), nameof(PlayerMovements) + ": PlayerController_OnMovementsPerformed(), _directionsPressed already contains the direction: " + direction);

				_directionsPressed.Add(direction);

				NextMoveDirection = direction;
			}
			else
			{
				_directionsPressed.Remove(direction);

				if (_directionsPressed.Count == 0)
				{
					NextMoveDirection = EDirection.NONE;

					if (onStopMoving != null)
					{
						onStopMoving.Invoke();
					}

					return;
				}

				NextMoveDirection = _directionsPressed[_directionsPressed.Count - 1];
			}

			if (_animator.IsPlayingAnyInteractions())
			{
				return;
			}

			if (LookDirection == NextMoveDirection)
			{
				Move(direction, OnMoveCompleted);

				Map.Tile nextTile = Map.TilesManager.Instance.GetTile(transform.position, transform.forward * Map.Tile.SIZE);
				if (nextTile.CharacterOnTile.Count > 0)
				{
					nextTile.Interact(ECharacter.PLAYER);
				}
			}
			else
			{
				LookTo(NextMoveDirection, OnLookCompleted);
			}
		}

		private void OnLookCompleted(bool _)
		{
			if (NextMoveDirection == EDirection.NONE)
			{
				return;
			}
			if (NextMoveDirection != LookDirection)
			{
				LookTo(NextMoveDirection, OnLookCompleted);
			}
			else
			{
				Move(NextMoveDirection, OnMoveCompleted);
			}
		}

		private void OnMoveCompleted(bool success)
		{
			if (!success || NextMoveDirection == EDirection.NONE)
			{
				return;
			}

			Map.Tile nextTile = Map.TilesManager.Instance.GetTile(transform.position);
			if (nextTile.CharacterOnTile.Count > 1)
			{
				nextTile.Interact(ECharacter.PLAYER);
			}

			if (NextMoveDirection != LookDirection)
			{
				LookTo(NextMoveDirection, OnLookCompleted);
			}
			else
			{
				Move(NextMoveDirection, OnMoveCompleted);
			}
		}

		private void PlayerInteractions_OnInteractionCompleted()
		{
			if (IsMoving())
			{
				return;
			}

			OnMoveCompleted(true);
		}

		#endregion
	}
}
