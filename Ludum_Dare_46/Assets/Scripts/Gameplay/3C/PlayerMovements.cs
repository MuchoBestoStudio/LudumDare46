using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	public class PlayerMovements : CharacterMovements
	{
		#region Variables

		[SerializeField, Tooltip("")]
		private PlayerController _controller = null;
		[Tooltip("")]
		public float RepeatDuration = 0.5f;

		public EDirection CurrentDirection { get; private set; } = EDirection.NONE;
		public float RepeatTime { get; private set; } = 0f;

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

		private void Update()
		{
			if (CurrentDirection != EDirection.NONE)
			{
				RepeatTime -= Time.deltaTime;

				if (RepeatTime <= 0f)
				{
					RepeatTime += RepeatDuration;

					Move(CurrentDirection);
				}
			}
		}

		private void PlayerController_OnMovementsPerformed(EDirection direction, bool started)
		{
			RepeatTime = 0f;

			if (started)
			{
				CurrentDirection = direction;
			}
			else
			{
				if (CurrentDirection == direction)
				{
					CurrentDirection = EDirection.NONE;
				}
			}
		}

		#endregion
	}
}
