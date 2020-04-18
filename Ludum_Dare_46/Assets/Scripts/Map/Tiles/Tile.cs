using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Map
{
	public class Tile : MonoBehaviour
	{
		[SerializeField]
		protected Transform _center = null;

		public Vector3 Center => _center.position;
		public virtual bool Free => true;

		public virtual void Interact()
		{
		}
	}
}