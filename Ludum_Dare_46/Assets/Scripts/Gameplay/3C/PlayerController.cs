using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	[DisallowMultipleComponent]
	public class PlayerController : MonoBehaviour
	{
		#region Struct

		[Serializable]
		public struct MovementsBlock
		{
			public InputActionReference Up;
			public InputActionReference Down;
			public InputActionReference Left;
			public InputActionReference Right;
		}

		#endregion

		#region Variables

		[Header("Controls")]
		[SerializeField, Tooltip("")]
		private	MovementsBlock _movements = new MovementsBlock();
		[SerializeField, Tooltip("")]
		private	InputActionReference _interact = null;

		[Header("Gameplay")]
		[SerializeField, Tooltip("")]
		private	GameManager	_gameManager = null;

		#endregion

		#region Events

		public Action<EDirection, bool> onMovementPerformed = null;

		public Action onInteractPerformed = null;
		public Action onInventoryPerformed = null;

		#endregion

		#region MonoBehaviour's Methods

		private void OnEnable()
		{
			RegisterToGameOver();

			RegisterMovements();

			RegisterInteractAction();
		}

		private void OnDisable()
		{
			UnRegisterToGameOver();

			UnRegisterMovements();

			UnRegisterInteractAction();
		}

		#endregion

		#region GameOver

		private void RegisterToGameOver()
		{
			_gameManager.onGameOver += GameManager_onGameOver;
		}

		private void UnRegisterToGameOver()
		{
			_gameManager.onGameOver -= GameManager_onGameOver;
		}

		private void GameManager_onGameOver()
		{
			UnRegisterMovements();

			UnRegisterInteractAction();
		}

		#endregion

		#region Movements

		private void RegisterMovements()
		{
			_movements.Up.action.started -= OnUpActionPerformed;
			_movements.Up.action.started += OnUpActionPerformed;
			_movements.Up.action.canceled -= OnUpActionPerformed;
			_movements.Up.action.canceled += OnUpActionPerformed;
			_movements.Up.action.Enable();

			_movements.Down.action.started -= OnDownActionPerformed;
			_movements.Down.action.started += OnDownActionPerformed;
			_movements.Down.action.canceled -= OnDownActionPerformed;
			_movements.Down.action.canceled += OnDownActionPerformed;
			_movements.Down.action.Enable();

			_movements.Left.action.started -= OnLeftActionPerformed;
			_movements.Left.action.started += OnLeftActionPerformed;
			_movements.Left.action.canceled -= OnLeftActionPerformed;
			_movements.Left.action.canceled += OnLeftActionPerformed;
			_movements.Left.action.Enable();

			_movements.Right.action.started -= OnRightActionPerformed;
			_movements.Right.action.started += OnRightActionPerformed;
			_movements.Right.action.canceled -= OnRightActionPerformed;
			_movements.Right.action.canceled += OnRightActionPerformed;
			_movements.Right.action.Enable();
		}

		private void UnRegisterMovements()
		{
			_movements.Up.action.started -= OnUpActionPerformed;
			_movements.Up.action.canceled -= OnUpActionPerformed;
			_movements.Up.action.Disable();

			_movements.Down.action.started -= OnDownActionPerformed;
			_movements.Down.action.canceled -= OnDownActionPerformed;
			_movements.Down.action.Disable();

			_movements.Left.action.started -= OnLeftActionPerformed;
			_movements.Left.action.canceled -= OnLeftActionPerformed;
			_movements.Left.action.Disable();

			_movements.Right.action.started -= OnRightActionPerformed;
			_movements.Right.action.canceled -= OnRightActionPerformed;
			_movements.Right.action.Disable();
		}

		private void OnUpActionPerformed(InputAction.CallbackContext context)
		{
			if (onMovementPerformed != null)
			{
				onMovementPerformed.Invoke(EDirection.UP, _movements.Up.action.phase == InputActionPhase.Started);
			}
		}

		private void OnDownActionPerformed(InputAction.CallbackContext _)
		{
			if (onMovementPerformed != null)
			{
				onMovementPerformed.Invoke(EDirection.DOWN, _movements.Down.action.phase == InputActionPhase.Started);
			}
		}

		private void OnLeftActionPerformed(InputAction.CallbackContext _)
		{
			if (onMovementPerformed != null)
			{
				onMovementPerformed.Invoke(EDirection.LEFT, _movements.Left.action.phase == InputActionPhase.Started);
			}
		}

		private void OnRightActionPerformed(InputAction.CallbackContext _)
		{
			if (onMovementPerformed != null)
			{
				onMovementPerformed.Invoke(EDirection.RIGHT, _movements.Right.action.phase == InputActionPhase.Started);
			}
		}

		#endregion

		#region Interact

		private void RegisterInteractAction()
		{
			// Tricks to avoid having multiple callbacks linked
			_interact.action.performed -= OnInteractActionPerformed;
			_interact.action.performed += OnInteractActionPerformed;
			_interact.action.Enable();
		}

		private void UnRegisterInteractAction()
		{
			_interact.action.performed -= OnInteractActionPerformed;
			_interact.action.Disable();
		}

		private void OnInteractActionPerformed(InputAction.CallbackContext _)
		{
			if (onInteractPerformed != null)
			{
				onInteractPerformed.Invoke();
			}
		}

		#endregion
	}
}
