using System;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	[DisallowMultipleComponent]
	public class PlayerAnimator : MonoBehaviour
	{
		#region Variables

		#region Const Keys

		// Animations' Tags
		public const string IDLE_TAG = "Idle";
		public const string MOVE_TAG = "Move";
		public const string INTERACTION_TAG = "Interaction";

		// Animator Parameters Key
		public const string MOVE_KEY = "Move";
		public const string PICK_AXE_KEY = "PickAxe";
		public const string PICK_WOOD_LOG_KEY = "PickWoodLog";
		public const string CUT_TREE_KEY = "CutTree";
		public const string MENACE_KEY = "Menace";
		public const string SCARED_KEY = "Scared";

		#endregion

		[SerializeField, Tooltip("")]
		private	Animator		_animator	=	null;
		[SerializeField, Tooltip("")]
		private	PlayerMovements	_movements	=	null;
		[SerializeField, Tooltip("")]
		private	PlayerInteractions _interactions = null;

		private int _idlesTagHash = 0;
		private int _moveTagHash = 0;
		private int _interactionTagHash = 0;

		#endregion

		#region MonoBehaviours' methods

		private void Awake()
		{
			_idlesTagHash = Animator.StringToHash(IDLE_TAG);
			_moveTagHash = Animator.StringToHash(MOVE_TAG);
			_interactionTagHash = Animator.StringToHash(INTERACTION_TAG);
		}

		private void OnEnable()
		{
			_movements.onMovePerformed += StartMoving;
			_movements.onStopMoving += StopMoving;

			_interactions.onCutingTree += CutTree;
			_interactions.onMenacingGhost += MenaceGhost;
			//_interactions.onPickingAxe += PickAxe;
			//_interactions.onPickingWoodLog += PickWoodLog;
		}

		private void OnDisable()
		{
			_movements.onMovePerformed -= StartMoving;
			_movements.onStopMoving -= StopMoving;

			_interactions.onCutingTree -= CutTree;
			_interactions.onMenacingGhost -= MenaceGhost;
			_interactions.onPickingAxe -= PickAxe;
			_interactions.onPickingWoodLog -= PickWoodLog;
		}

		#endregion

		#region Animator

		public void StartMoving()
		{
			_animator.SetBool(MOVE_KEY, true);
		}

		public void StopMoving()
		{
			_animator.SetBool(MOVE_KEY, false);
		}

		public void	PickAxe()
		{
			_animator.SetTrigger(PICK_AXE_KEY);
		}

		public void PickWoodLog()
		{
			_animator.SetTrigger(PICK_WOOD_LOG_KEY);
		}

		public void CutTree()
		{
			_animator.SetTrigger(CUT_TREE_KEY);
		}

		public void MenaceGhost()
		{
			_animator.SetTrigger(MENACE_KEY);
		}

		public void Scared()
		{
			_animator.SetTrigger(SCARED_KEY);
		}

		public bool IsPlayingAnyIdles()
		{
			return _animator.GetCurrentAnimatorStateInfo(0).tagHash == _idlesTagHash;
		}

		public bool IsPlayingAnyMovements()
		{
			return _animator.GetCurrentAnimatorStateInfo(0).tagHash == _moveTagHash;
		}

		public bool IsPlayingAnyInteractions()
		{
			return _animator.GetCurrentAnimatorStateInfo(0).tagHash == _interactionTagHash;
		}

		#endregion
	}
}
