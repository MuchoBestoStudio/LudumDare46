using System;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay.Enemies
{
	[DisallowMultipleComponent]
	public class EnemyAnimator : MonoBehaviour
	{
		#region Events

		public	Action<EnemyAnimator> onEnemyStartToAppear = null;
		public	Action<EnemyAnimator> onEnemyEndAppearing = null;

		public	Action<EnemyAnimator> onEnemyStartToDisappear = null;
		public	Action<EnemyAnimator> onEnemyEndDisappearing = null;

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
