using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Pathfinding
{
	public class PathNode : IComparable<PathNode>
	{
		private Map.Tile _tileNode;
		public Map.Tile TileNode => _tileNode;

		private PathNode parent;
		public PathNode Parent => parent;

		public Vector3 Position => _tileNode.transform.position;

		// g
		private int movementCost;
		public int MovementCost => movementCost;

		// h
		private int distanceToTarget;
		public int DistanceToTarget => distanceToTarget;

		// f
		public int TotalCost => movementCost + distanceToTarget;

		public PathNode(Map.Tile node)
		{
			_tileNode = node;
		}

		public void SetParent(PathNode node)
		{
			parent = node;
		}

		public void SetMovementCost(int cost)
		{
			movementCost = cost;
		}

		public void SetDistance(int distance)
		{
			distanceToTarget = distance;
		}

		public int CompareTo(PathNode nodeToCompare)
		{
			int compare = TotalCost.CompareTo(nodeToCompare.TotalCost);
			if (compare == 0)
			{
				compare = DistanceToTarget.CompareTo(nodeToCompare.DistanceToTarget);
			}
			return -compare;
		}
	}
}