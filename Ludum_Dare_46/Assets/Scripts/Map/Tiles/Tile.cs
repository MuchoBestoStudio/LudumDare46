using System.Collections.Generic;
using UnityEngine;
using MuchoBestoStudio.LudumDare.Gameplay;
using MuchoBestoStudio.LudumDare.Gameplay._3C;
using MuchoBestoStudio.LudumDare.Gameplay.Enemies;
using UnityEngine.Assertions;

namespace MuchoBestoStudio.LudumDare.Map
{
	[DisallowMultipleComponent]
	public class Tile : MonoBehaviour
	{
		public const float SIZE = 1f;

		[SerializeField]
		protected Transform _center = null;

		public Vector3 Center => _center.position;
        public virtual bool Free => true;

        [SerializeField]
        private List<GameObject> _characterGO = new List<GameObject>();
        public List<GameObject> CharacterOnTile => _characterGO;

        public void AddCharacterOnTile(GameObject character)
		{
			if (!_characterGO.Contains(character))
				_characterGO.Add(character);
		}

		public void RemoveCharacterOnTile(GameObject character)
		{
			if (_characterGO.Contains(character))
				_characterGO.Remove(character);
		}

        public virtual void Interact(ECharacter character)
		{
            if (_characterGO.Count > 0)
            {
				var list = _characterGO.ToArray();
				foreach (GameObject go in list)
				{
		            CharacterInteractions interactions = go.GetComponent<CharacterInteractions>();
				
					Assert.IsNotNull(interactions, nameof(Tile) + ": Interact(), character should contains an interaction component.");

					interactions.Interact(character);
				}
            }
		}
	}
}