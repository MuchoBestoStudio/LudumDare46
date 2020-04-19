using UnityEngine;
using MuchoBestoStudio.LudumDare.Gameplay;
using MuchoBestoStudio.LudumDare.Gameplay._3C;

namespace MuchoBestoStudio.LudumDare.Map
{
	public class Tile : MonoBehaviour
	{
		public const float SIZE = 1f;

		[SerializeField]
		protected Transform _center = null;

		public Vector3 Center => _center.position;
		public virtual bool Free => true;

        [SerializeField]
        private CharacterMovements _characterOnTile = null;
        public CharacterMovements CharacterOnTile => _characterOnTile;

        public void SetCharacterOnTile(CharacterMovements character) { _characterOnTile = character; }

        public virtual void Interact(ECharacter character)
		{
            if (_characterOnTile)
            {
                _characterOnTile.Interact(character);
            }
		}
	}
}