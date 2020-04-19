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
		}

		private void OnDisable()
		{
			_movements.onEnemyReachEnd -= Enemy_OnEnemyReachEnd;
		}

		private void Enemy_OnEnemyReachEnd(EnemyMovements _)
		{
			Disappear();
		}

		#endregion

		#region Animations

		public void Appear()
		{
			gameObject.SetActive(true);

			if (onEnemyStartToAppear != null)
			{
				onEnemyStartToAppear.Invoke(this);
			}
			if (onEnemyEndAppearing != null)
			{
				onEnemyEndAppearing.Invoke(this);
			}
		}

		public void Disappear()
		{
			gameObject.SetActive(false);

			if (onEnemyStartToDisappear != null)
			{
				onEnemyStartToDisappear.Invoke(this);
			}
			if (onEnemyEndDisappearing != null)
			{
				onEnemyEndDisappearing.Invoke(this);
			}
		}

		#endregion
	}
}
