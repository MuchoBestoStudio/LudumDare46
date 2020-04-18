using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	public class CameraFollow : MonoBehaviour
	{
		#region Variables

		[SerializeField, Tooltip("")]
		private	Transform _target	= null;
		[SerializeField, Tooltip("")]
		private	Vector3 _offset = Vector3.zero;

		#endregion

		#region MonoBehaviour's Methods

		private void LateUpdate()
		{
			transform.position = _target.position + _offset;
			transform.LookAt(_target);
		}

		#endregion
	}
}
