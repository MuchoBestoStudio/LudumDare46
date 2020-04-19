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

		private	List<EDirection>	_directionsPressed	=	new List<EDirection>();

		#endregion

		#region Events

		public Action onStopMoving = null;

		#endregion

		#region Methods

		private void OnEnable()
		{
			_controller.onMovementPerformed += PlayerController_OnMovementsPerformed;
		}

		private void OnDisable()
		{
			_controller.onMovementPerformed -= PlayerController_OnMovementsPerformed;
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

			if (IsMoving() || IsPlayingIdleAnimations())
			{
				return;
			}

			if (LookDirection == NextMoveDirection)
			{
				Move(direction, OnMoveCompleted);
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

			if (NextMoveDirection != LookDirection)
			{
				LookTo(NextMoveDirection, OnLookCompleted);
			}
			else
			{
				Move(NextMoveDirection, OnMoveCompleted);
			}
		}

		private bool IsPlayingIdleAnimations()
		{
			return _animator.IsPlayingAnyIdles();
		}


		#endregion
	}
}
