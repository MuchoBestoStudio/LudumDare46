using System;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	[DisallowMultipleComponent]
	public class PlayerAnimator : MonoBehaviour
	{
		#region Variables

		#region Const Keys

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

		#endregion

		#region MonoBehaviours' methods

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
			return false;
		}

		public bool IsPlayingAnyMovements()
		{
			return false;
		}

		public bool IsPlayingAnyInteractions()
		{
			return false;
		}

		#endregion
	}
}
