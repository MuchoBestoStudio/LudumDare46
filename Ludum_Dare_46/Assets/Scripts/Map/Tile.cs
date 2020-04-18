using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Map
{
	public class Tile : MonoBehaviour
	{
		public const float SIZE = 1f;

		[SerializeField]
		private Transform _center = null;
		[SerializeField]
		private Resource _resource = null;

		public Vector3 Center => _center.position;
		public bool Free => _resource == null;
	}
}