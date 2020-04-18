using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Pathfinding
{
	public class PathFinder
	{
		private Map.TilesManager _currentMap;
		private PathNode[,] _nodes;

		public PathFinder(Map.TilesManager map, Map.Tile[] nodesMap)
		{
			_currentMap = map;
			Vector2Int tabIndex = Vector2Int.zero;
			_nodes = new PathNode[_currentMap.Column, _currentMap.Row];

			for (int i = 0 ; i < nodesMap.Length ; ++i)
			{
				tabIndex.x = Mathf.RoundToInt(nodesMap[i].transform.position.x / Map.Tile.SIZE);
				tabIndex.y = Mathf.RoundToInt(nodesMap[i].transform.position.z / Map.Tile.SIZE);

				PathNode p = new PathNode(nodesMap[i]);

				_nodes[tabIndex.y, tabIndex.x] = p;
			}
		}

		public Map.Tile[] GetPath(Map.Tile start, Map.Tile end)
		{
			bool pathSuccess = false;
			PathNode startNode = new PathNode(start);
			PathNode targetNode = new PathNode(end);
			Heap<PathNode> openSet = new Heap<PathNode>(_currentMap.Column * _currentMap.Row);
			HashSet<PathNode> closedSet = new HashSet<PathNode>();

			openSet.Push(startNode);

			while (openSet.Count > 0)
			{
				PathNode currentNode = openSet.Pop();
				closedSet.Add(currentNode);

				if (currentNode.TileNode == targetNode.TileNode)
				{
					pathSuccess = true;
					targetNode.SetParent(currentNode);
					//Find path;
					break;
				}

				PathNode[] nodeNeighbours = GetNeighbours(currentNode, end);

				foreach (PathNode neighbour in nodeNeighbours)
				{
					if (closedSet.Contains(neighbour))
						continue;

					int movementCost = currentNode.MovementCost + GetDistance(currentNode, neighbour);
					if (movementCost < neighbour.MovementCost || !openSet.Contains(neighbour))
					{
						neighbour.SetMovementCost(movementCost);
						neighbour.SetDistance(GetDistance(neighbour, targetNode));
						neighbour.SetParent(currentNode);

						if (!openSet.Contains(neighbour))
							openSet.Push(neighbour);
						else
							openSet.UpdateItem(neighbour);
					}
				}
			}

			if (pathSuccess)
			{
				return RetracePath(startNode, targetNode);
			}
			return new Map.Tile[0];
		}

		private Map.Tile[] RetracePath(PathNode startNode, PathNode endNode)
		{
			List<Map.Tile> path = new List<Map.Tile>();
			PathNode currentNode = endNode;

			while (currentNode != startNode)
			{
				path.Add(currentNode.TileNode);
				currentNode = currentNode.Parent;
			}
			path.Add(startNode.TileNode);
			Map.Tile[] waypoints = path.ToArray();
			System.Array.Reverse(waypoints);
			return waypoints;
		}

		private PathNode[] GetNeighbours(PathNode node, Map.Tile target)
		{
			List<PathNode> neighbours = new List<PathNode>();
			Map.Tile nodeMap = null;

			int checkX = Mathf.RoundToInt(node.Position.x - 1);
			int checkY = Mathf.RoundToInt(node.Position.z);

			if (checkX >= 0)
			{
				nodeMap = _nodes[checkX, checkY].TileNode;
				if (nodeMap.Free || nodeMap == target)
					neighbours.Add(_nodes[checkY, checkX]);
			}

			checkX = Mathf.RoundToInt(node.Position.x + 1);
			if (checkX < _currentMap.Column)
			{
				nodeMap = _nodes[checkX, checkY].TileNode;
				if (nodeMap.Free || nodeMap == target)
					neighbours.Add(_nodes[checkY, checkX]);
			}

			checkX = Mathf.RoundToInt(node.Position.x);
			checkY = Mathf.RoundToInt(node.Position.z - 1);

			if (checkY >= 0)
			{
				nodeMap = _nodes[checkX, checkY].TileNode;
				if (nodeMap.Free || nodeMap == target)
					neighbours.Add(_nodes[checkY, checkX]);
			}

			checkY = Mathf.RoundToInt(node.Position.z + 1);
			if (checkY < _currentMap.Row)
			{
				nodeMap = _nodes[checkX, checkY].TileNode;
				if (nodeMap.Free || nodeMap == target)
					neighbours.Add(_nodes[checkY, checkX]);
			}
			return neighbours.ToArray();
		}

		private int GetDistance(PathNode nodeA, PathNode nodeB)
		{
			return Mathf.RoundToInt(Vector3.Distance(nodeA.Position, nodeB.Position) * 10f);
		}
	}
}