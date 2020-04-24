using System;
using System.Linq;
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

		private Tile	_path = null;
		private Tile	_currentTile = null;
		private EDirection _currentDir = EDirection.NONE;

		#endregion

		#region Events

		public Action<EnemyMovements>	onEnemyReachEnd = null;

		#endregion

		#region MonoBehaviour's Methods

		private void OnEnable()
		{
			_animator.onEnemyEndAppearing += EnemyAnimator_OnEnemyEndAppearing;
			_animator.onEnemyStartToDisappear += EnemyAnimator_OnEnemyEndDisappearing;
			GameManager.Instance.onTimeUpdated += OnTickUpdate;
		}

		override protected void OnDisable()
		{
            base.OnDisable();

			_animator.onEnemyEndAppearing -= EnemyAnimator_OnEnemyEndAppearing;
			_animator.onEnemyStartToDisappear -= EnemyAnimator_OnEnemyEndDisappearing;
			GameManager.Instance.onTimeUpdated -= OnTickUpdate;
		}

		private	void EnemyAnimator_OnEnemyEndAppearing(EnemyAnimator _)
		{
			StartPath();
			_currentDir = DetermineDirection(transform.position, _path.Center);

			if (_currentDir != LookDirection)
			{
				LookTo(_currentDir, null);
			}
		}

		private void EnemyAnimator_OnEnemyEndDisappearing(EnemyAnimator _)
		{
			StopMove();
		}

		#endregion

		#region Movements

		private void MoveToNextPoint()
		{
			if (_target == null || !_path)
			{
				return;
			}

			_currentDir =  DetermineDirection(transform.position, _path.Center);

			if (_currentDir != LookDirection)
			{
				LookTo(_currentDir, null);
			}
			else
			{
				if (_path == _target)
				{
					OnLookFireSource(true);
					_target = null;
				}
				else
				{
					Move(_currentDir, OnMoveComplete);
				}
			}
		}

		private void OnLookFireSource(bool _)
		{
			if (onEnemyReachEnd != null)
			{
				onEnemyReachEnd.Invoke(this);
			}
		}

		private void OnTickUpdate()
		{
			_path = TilesManager.Instance.PathFinder.GetPath(_currentTile, _target);
			MoveToNextPoint();
		}

		private void OnMoveComplete(bool _)
		{
			_currentTile = _path;
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

		private void RetrievePath()
		{
			_currentTile = TilesManager.Instance.GetTile(transform.position);

			Assert.IsNotNull(_currentTile, nameof(EnemyMovements) + ": RetrievePath(), startTile should not be null.");

			_path = TilesManager.Instance.PathFinder.GetPath(_currentTile, _target);
		}

		public void SetTarget(Tile newTarget)
		{
			_target = newTarget;
		}

		public void StartPath()
		{
			RetrievePath();
		}

		public void StopPath()
		{
			StopMove();
		}

        #endregion
    }
}
