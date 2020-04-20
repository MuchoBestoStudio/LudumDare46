using System;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay.Enemies
{
	[DisallowMultipleComponent]
	public class EnemyAnimator : MonoBehaviour
	{
		#region Variables

		[SerializeField, Tooltip("")]
		private	EnemyMovements	_movements = null;
		[SerializeField]
		private Animator _animator = null;

		#endregion

		#region Events

		public	Action<EnemyAnimator> onEnemyStartToAppear = null;
		public	Action<EnemyAnimator> onEnemyEndAppearing = null;

		public	Action<EnemyAnimator> onEnemyStartToDisappear = null;
		public	Action<EnemyAnimator> onEnemyEndDisappearing = null;

		#endregion

		#region MonoBehaviour's Methods

		private void OnEnable()
		{
			_movements.onEnemyReachEnd += Enemy_OnEnemyReachEnd;

			if (onEnemyEndAppearing != null)
			{
				onEnemyEndAppearing.Invoke(this);
			}
		}

		private void OnDisable()
		{
			_movements.onEnemyReachEnd -= Enemy_OnEnemyReachEnd;

			if (onEnemyEndDisappearing != null)
			{
				onEnemyEndDisappearing.Invoke(this);
			}
		}

		private void Enemy_OnEnemyReachEnd(EnemyMovements _)
		{
			Disappear();
		}

		#endregion

		#region Animations

		public void Appear()
		{
			if (onEnemyStartToAppear != null)
			{
				onEnemyStartToAppear.Invoke(this);
			}

			gameObject.SetActive(true);
		}

		public void Disappear()
		{
			if (onEnemyStartToDisappear != null)
			{
				onEnemyStartToDisappear.Invoke(this);
			}

			_animator.SetTrigger("Disappear");

			Invoke("SetEnemyInactive", 3f);
		}

		private void SetEnemyInactive()
		{
			gameObject.SetActive(false);
		}

		#endregion
	}
}
