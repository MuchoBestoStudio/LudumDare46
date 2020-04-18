using System;
using UnityEngine;
using MuchoBestoStudio.LudumDare.Gameplay._3C;

namespace MuchoBestoStudio.LudumDare.Gameplay.Enemies
{
	public class EnemyMovements : CharacterMovements
	{
		#region Variables

		[SerializeField, Tooltip("")]
		private	Transform _target = null;

		private	int	_index = 0;
		private Vector3[]	_path = new Vector3[0];
		private EDirection _currentDir = EDirection.NONE;

		#endregion

		#region Events

		public Action<EnemyMovements>	onEnemyReachEnd = null;

		#endregion

		#region MonoBehaviour's Methods

		private void OnEnable()
		{
			RetrievePath();
		}

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

			_currentDir =  DetermineDirection(transform.position, _path[_index]);

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

		private void SetPath(Vector3[] path)
		{
			_index = 0;

			_path = path;

			MoveToNextPoint();
		}

		private void RetrievePath()
		{
			Vector3[] points = new Vector3[] { transform.position,
											   transform.position + new Vector3(10f, 0f, 0f),
											   transform.position + new Vector3(10f, 0f, 10f),
											   transform.position + new Vector3(0f, 0f, 10f),
											 };

			SetPath(points);
		}

		#endregion
	}
}
