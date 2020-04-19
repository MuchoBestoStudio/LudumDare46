using System;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	[DisallowMultipleComponent]
	public class PlayerMovements : CharacterMovements
	{
		#region Variables

		[SerializeField, Tooltip("")]
		private PlayerController _controller = null;

		public EDirection CurrentDirection { get; private set; } = EDirection.NONE;

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
				CurrentDirection = direction;

				if (!IsMoving())
				{
					LookTo(CurrentDirection);

					Move(CurrentDirection, OnMoveCompleted);
				}
			}
			else
			{
				if (CurrentDirection == direction)
				{
					CurrentDirection = EDirection.NONE;

					if (onStopMoving != null)
					{
						onStopMoving.Invoke();
					}
				}
			}
		}

		private void OnMoveCompleted(bool success)
		{
			if (success && CurrentDirection != EDirection.NONE)
			{
				LookTo(CurrentDirection);

				Move(CurrentDirection, OnMoveCompleted);
			}
		}

		#endregion
	}
}
