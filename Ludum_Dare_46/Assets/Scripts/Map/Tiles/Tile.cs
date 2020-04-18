using UnityEngine;
using MuchoBestoStudio.LudumDare.Gameplay;

namespace MuchoBestoStudio.LudumDare.Map
{
	public class Tile : MonoBehaviour
	{
		public const float SIZE = 1f;

		[SerializeField]
		protected Transform _center = null;

		public Vector3 Center => _center.position;
		public virtual bool Free => true;

		public virtual void Interact(ECharacter character)
		{
		}
	}
}