using System;
using UnityEngine;
using MuchoBestoStudio.LudumDare.Gameplay._3C;
using MuchoBestoStudio.LudumDare.Map;
using UnityEngine.Assertions;

namespace MuchoBestoStudio.LudumDare.Gameplay.Enemies
{
	[DisallowMultipleComponent]
	public class EnemyMovements : CharacterMovements
	{
		#region Variables

		[SerializeField, Tooltip("")]
		private	EnemyAnimator _animator = null;
		[SerializeField, Tooltip("")]
		private	Tile _target = null;

		private	int	_index = 0;
		private Tile[]	_path = new Tile[0];
		private EDirection _currentDir = EDirection.NONE;

		#endregion

		#region Events

		public Action<EnemyMovements>	onEnemyReachEnd = null;

		#endregion

		#region MonoBehaviour's Methods

		private void OnEnable()
		{
			_animator.onEnemyEndAppearing += EnemyAnimator_OnEnemyEndAppearing;
		}

		override protected void OnDisable()
		{
            base.OnDisable();
			_animator.onEnemyEndAppearing -= EnemyAnimator_OnEnemyEndAppearing;
		}

		private	void EnemyAnimator_OnEnemyEndAppearing(EnemyAnimator _)
		{
			StartPath();
		}

		#endregion

		#region Movements

		private void MoveToNextPoint()
		{
			if (!enabled || _path == null || _path.Length == 0)
			{
				return;
			}

			++_index;

			if (_index + 1 >= _path.Length)
			{
				_currentDir = EDirection.NONE;

				LookTo(DetermineDirection(transform.position, _target.Center), OnLookFireSource);
				return;
			}

			_currentDir =  DetermineDirection(transform.position, _path[_index].Center);

			if (_currentDir != LookDirection)
			{
				LookTo(_currentDir, OnLookToCompleted);
			}
			else
			{
				Move(_currentDir, OnMoveCompleted);
			}
		}

		private void OnLookToCompleted(bool _)
		{
			Move(_currentDir, OnMoveCompleted);
		}

		private void OnLookFireSource(bool _)
		{
			if (onEnemyReachEnd != null)
			{
				onEnemyReachEnd.Invoke(this);
			}
		}

		private void OnMoveCompleted(bool success)
		{
			Tile[] path = TilesManager.Instance.PathFinder.GetPath(_path[_index], _target);
			SetPath(path);
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

		private void SetPath(Tile[] path)
		{
			_index = 0;

			_path = path;
		}

		private void RetrievePath()
		{
			Tile startTile = TilesManager.Instance.GetTile(transform.position);

			Assert.IsNotNull(startTile, nameof(EnemyMovements) + ": RetrievePath(), startTile should not be null.");

			Tile[] path = TilesManager.Instance.PathFinder.GetPath(startTile, _target);

			SetPath(path);
		}

		public void SetTarget(Tile newTarget)
		{
			_target = newTarget;

			RetrievePath();
		}

		public void StartPath()
		{
			MoveToNextPoint();
		}

        #endregion
    }
}
