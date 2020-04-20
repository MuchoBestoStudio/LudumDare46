using UnityEngine;
using MuchoBestoStudio.LudumDare.Map;
using MuchoBestoStudio.LudumDare.Gameplay._3C;

namespace MuchoBestoStudio.LudumDare.Gameplay.Enemies
{
	[DisallowMultipleComponent]
	public class EnemyInteractions : CharacterInteractions
	{
		#region Variables

		[SerializeField, Tooltip("")]
		private	EnemyMovements	_movements = null;
		[SerializeField, Tooltip("")]
		private EnemyAnimator	_animator = null;

		private bool _isKilled = false;

		#endregion

		#region MonoBehaviour's Methods

		private void OnEnable()
		{
			_movements.onEnemyReachEnd += EnemyMovements_onEnemyReachEnd;
			_isKilled = false;
		}

		private void OnDisable()
		{
            ClearTilesPresence();
            _movements.onEnemyReachEnd -= EnemyMovements_onEnemyReachEnd;
		}

		private void EnemyMovements_onEnemyReachEnd(EnemyMovements _)
		{
			if (_isKilled)
			{
				return;
			}
			InteractWithFrontTile();
            ClearTilesPresence();
        }

		#endregion

		#region Interact

		public override void Interact(ECharacter character)
		{
			if (!_isKilled && character == ECharacter.PLAYER)
			{
				_isKilled = true;
                ClearTilesPresence();

				_animator.Disappear();
            }
        }

		public void InteractWithFrontTile()
		{
			if (TilesManager.Instance == null)
			{
				return;
			}

			Tile forwardTile = TilesManager.Instance.GetTile(transform.position, transform.forward);
			if (forwardTile != null)
			{
				forwardTile.Interact(ECharacter.ENEMY);
			}
		}

        private void ClearTilesPresence()
        {
            TilesManager tilesManager = TilesManager.Instance;
            if (tilesManager == null)
                return;

            Tile currentTile = tilesManager.GetTile(transform.position);
            if (currentTile)
            {
                currentTile.RemoveCharacterOnTile(gameObject);
            }

            Tile frontTile = tilesManager.GetTile(transform.position, transform.forward);
            if (frontTile)
            {
                frontTile.RemoveCharacterOnTile(gameObject);
            }

            Tile backTile = tilesManager.GetTile(transform.position, -transform.forward);
            if (backTile)
            {
                backTile.RemoveCharacterOnTile(gameObject);
            }

            Tile leftTile = tilesManager.GetTile(transform.position, -transform.right);
            if (leftTile)
            {
                leftTile.RemoveCharacterOnTile(gameObject);
            }

            Tile rightTile = tilesManager.GetTile(transform.position, transform.right);
            if (rightTile)
            {
                rightTile.RemoveCharacterOnTile(gameObject);
            }
        }

		#endregion
	}
}
