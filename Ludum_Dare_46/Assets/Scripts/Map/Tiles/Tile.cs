using UnityEngine;
using MuchoBestoStudio.LudumDare.Gameplay;
using MuchoBestoStudio.LudumDare.Gameplay._3C;
using MuchoBestoStudio.LudumDare.Gameplay.Enemies;
using UnityEngine.Assertions;

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
        private GameObject _characterGO = null;
        public GameObject CharacterOnTile => _characterGO;

        public void SetCharacterOnTile(GameObject character)
		{
			_characterGO = character;
		}

        public virtual void Interact(ECharacter character)
		{
            if (_characterGO != null)
            {
                CharacterInteractions interactions = _characterGO.GetComponent<CharacterInteractions>();
				
				Assert.IsNotNull(interactions, nameof(Tile) + ": Interact(), character should contains an interaction component.");

				interactions.Interact(character);
            }
		}
	}
}