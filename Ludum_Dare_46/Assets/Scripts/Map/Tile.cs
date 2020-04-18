using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Map
{
	public class Tile : MonoBehaviour
	{
		[SerializeField]
		private Transform _center = null;
		[SerializeField]
		private Resource _resource = null;

		public Vector3 Center => _center.position;
		public bool Free => _resource == null;
	}
}