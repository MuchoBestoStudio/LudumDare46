using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Map
{
	public class BlockTile : Tile
	{
		public override bool Free { get { return false; } }
	}
}