using System;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	public class PlayerAnimationsReceiver : MonoBehaviour
	{
		#region Events

		public Action	onTreeCut = null;

		#endregion

		#region Callbacks

		public void AnimationEvent_OnTreeCut()
		{
			if (onTreeCut != null)
			{
				onTreeCut.Invoke();
			}
		}

		#endregion
	}
}
