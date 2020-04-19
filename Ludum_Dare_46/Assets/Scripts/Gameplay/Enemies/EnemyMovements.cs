using System;
using UnityEngine;
using MuchoBestoStudio.LudumDare.Gameplay._3C;
using MuchoBestoStudio.LudumDare.Map;
using UnityEngine.Assertions;

namespace MuchoBestoStudio.LudumDare.Gameplay.Enemies
{
	public class EnemyMovements : CharacterMovements
	{
		#region Variables

		[SerializeField, Tooltip("")]
		private	EnemyInteraction _interaction = null;
		[SerializeField, Tooltip("")]
		private	Tile _target = null;

		private	int	_index = 0;
		private Tile[]	_path = new Tile[0];
		private EDirection _currentDir = EDirection.NONE;

		#endregion

		#region Events

		public Action<EnemyMovements>	onEnemyReachEnd = null;

		#endregion

		#region Movements

		private void MoveToNextPoint()
		{
			++_index;

			if (_index + 1 >= _path.Length)
			{
				_currentDir = EDirection.NONE;

				LookTo(DetermineDirection(transform.position, _path[_path.Length - 1].transform.position));

				_interaction.Interact();

				if (onEnemyReachEnd != null)
				{
					onEnemyReachEnd.Invoke(this);
				}
				return;
			}

			_currentDir =  DetermineDirection(transform.position, _path[_index].transform.position);

			LookTo(_currentDir);

			Move(_currentDir, OnMoveCompleted);
		}

		private void OnMoveCompleted(bool success)
		{
			if (success)
			{
				MoveToNextPoint();
			}
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

        #endregion

        override public void Interact(ECharacter character) 
        {
            if (character == ECharacter.PLAYER)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
