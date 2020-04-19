using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuchoBestoStudio.LudumDare.Gameplay;

namespace MuchoBestoStudio.LudumDare.Map
{
	public class AxeTile : Tile
	{
		[SerializeField]
		private GameObject _axe = null;
		public override bool Free { get { return !_axe; } }

		public override void Interact(ECharacter character)
		{
			if (!_axe || character != ECharacter.PLAYER)
			{
				return;
			}

			Inventory inventory = FindObjectOfType<Inventory>();
			inventory.PickUpAxe();
			_axe.SetActive(false);
			_axe = null;
		}
	}
}