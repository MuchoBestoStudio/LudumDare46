using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MuchoBestoStudio.LudumDare.Gameplay;

namespace MuchoBestoStudio.LudumDare.Map
{
	public class FireSourceTile : Tile
	{
		[SerializeField]
		private FireSource _fireSource = null;
		public override bool Free { get { return false; } }

		public override void Interact(ECharacter character)
		{
			if (_fireSource != null)
			{
				_fireSource.Interact(character);
			}
		}
	}
}