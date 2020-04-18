using UnityEngine;
using UnityEngine.AI;
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

		#region MonoBehaviour's Methods

		private void OnEnable()
		{
			RetrievePath();

			// Restart movement
		}

		private void OnDisable()
		{
			
		}

		private void Update()
		{
			if (_currentDir != EDirection.NONE)
			{
				if (HasArrived())
				{
					transform.position = _path[_index];

					MoveToNextPoint();
				}

				Move(_currentDir);
			}
		}

		#endregion

		#region Movements

		private bool HasArrived()
		{
			if (Vector3.Distance(transform.position, _path[_index]) <= 0.1f)
			{
				return true;
			}
			return false;
		}

		private void MoveToNextPoint()
		{
			++_index;

			if (_index >= _path.Length)
			{
				_currentDir = EDirection.NONE;
				return;
			}

			_currentDir =  DetermineDirection(transform.position, _path[_index]);

			//Move(EDirection.);
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
			Vector3[] points = new Vector3[] { new Vector3(0f, 1f, 7f),
											   new Vector3(2f, 1f, 7f),
											   new Vector3(2f, 1f, 0f),
											   new Vector3(0f, 1f, 0f),
											 };

			SetPath(points);
		}

		#endregion
	}
}
