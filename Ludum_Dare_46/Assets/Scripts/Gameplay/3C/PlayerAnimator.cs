using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	[DisallowMultipleComponent]
	public class PlayerAnimator : MonoBehaviour
	{
		#region Variables

		#region Const Keys

		public const string MOVING_KEY = "Move";
		public const string CUT_TREE_KEY = "CutTree";
		public const string MENACE_KEY = "Menace";
		public const string SCARED_KEY = "Scared";

		#endregion

		[SerializeField, Tooltip("")]
		private	Animator		_animator	=	null;
		[SerializeField, Tooltip("")]
		private	PlayerMovements	_movements	=	null;

		#endregion

		#region MonoBehaviours' methods

		private void OnEnable()
		{
			_movements.onMovePerformed += StartMoving;
			_movements.onStopMoving += StopMoving;
		}

		private void OnDisable()
		{
			_movements.onMovePerformed -= StartMoving;
			_movements.onStopMoving -= StopMoving;
		}

		#endregion

		#region Animator

		public void StartMoving()
		{
			_animator.SetBool(MOVING_KEY, true);
		}

		public void StopMoving()
		{
			_animator.SetBool(MOVING_KEY, false);
		}

		public void CutTree()
		{
			_animator.SetTrigger(CUT_TREE_KEY);
		}

		public void Menace()
		{
			_animator.SetTrigger(MENACE_KEY);
		}

		public void Scared()
		{
			_animator.SetTrigger(SCARED_KEY);
		}

		#endregion
	}
}
