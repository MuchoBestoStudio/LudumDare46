using System;
using System.Linq;
using UnityEngine;
using MuchoBestoStudio.LudumDare.Gameplay._3C;

namespace MuchoBestoStudio.LudumDare.Gameplay.Enemies
{
	public class EnemyMovements : CharacterMovements
	{
		#region Variables

		[SerializeField, Tooltip("")]
		private	Map.Tile _target = null;

		private	int	_index = 0;
		private Map.Tile[]	_path = new Map.Tile[0];
		private EDirection _currentDir = EDirection.NONE;

		#endregion

		#region Events

		public Action<EnemyMovements>	onEnemyReachEnd = null;

		#endregion

		#region MonoBehaviour's Methods
		#endregion

		#region Movements

		private void MoveToNextPoint()
		{
			++_index;

			if (_index >= _path.Length)
			{
				_currentDir = EDirection.NONE;

				if (onEnemyReachEnd != null)
				{
					onEnemyReachEnd.Invoke(this);
				}
				return;
			}

			_currentDir =  DetermineDirection(transform.position, _path[_index].transform.position);

			Debug.Log(_index + " Move to " + _currentDir + "   /  " + _path[_index]);
			Move(_currentDir, OnMoveCompleted);
		}

		private void OnMoveCompleted()
		{
			MoveToNextPoint();
		}

		private EDirection DetermineDirection(Vector3 start, Vector3 destination)
		{
			if (start.x > destination.x)
			{
				return EDirection.LEFT;
			}
			if (start.x < destination.x)
			{
				return EDirection.RIGHT;
			}
			if (start.z > destination.z)
			{
				return EDirection.DOWN;
			}
			if (start.z < destination.z)
			{
				return EDirection.UP;
			}
			return EDirection.NONE;
		}

		private void SetPath(Map.Tile[] path)
		{
			_index = 0;

			_path = path;

			MoveToNextPoint();
		}

		private void RetrievePath()
		{
			Map.Tile startTile = Map.TilesManager.Instance.GetTile(transform.position);
			Map.Tile[] path = Map.TilesManager.Instance.PathFinder.GetPath(startTile, _target);

			SetPath(path);
		}

		public void SetTarget(Map.Tile newTarget)
		{
			_target = newTarget;
			RetrievePath();
		}

		#endregion
	}
}
