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

		#endregion

		#region MonoBehaviour's Methods

		private void OnEnable()
		{
			_movements.onEnemyReachEnd += EnemyMovements_onEnemyReachEnd;
		}

		private void OnDisable()
		{
            ClearTilesPresence();
            _movements.onEnemyReachEnd -= EnemyMovements_onEnemyReachEnd;
		}

		private void EnemyMovements_onEnemyReachEnd(EnemyMovements _)
		{
			InteractWithFrontTile();
            ClearTilesPresence();
        }

		#endregion

		#region Interact

		public override void Interact(ECharacter character)
		{
			if (character == ECharacter.PLAYER)
			{
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
            if (currentTile && currentTile.CharacterOnTile == gameObject)
            {
                currentTile.SetCharacterOnTile(null);
                currentTile.SetFree(true);
            }

            Tile frontTile = tilesManager.GetTile(transform.position, transform.forward);
            if (frontTile && frontTile.CharacterOnTile == gameObject)
            {
                frontTile.SetCharacterOnTile(null);
                frontTile.SetFree(true);
            }

            Tile backTile = tilesManager.GetTile(transform.position, -transform.forward);
            if (backTile && backTile.CharacterOnTile == gameObject)
            {
                backTile.SetCharacterOnTile(null);
                backTile.SetFree(true);
            }

            Tile leftTile = tilesManager.GetTile(transform.position, -transform.right);
            if (leftTile && leftTile.CharacterOnTile == gameObject)
            {
                leftTile.SetCharacterOnTile(null);
                leftTile.SetFree(true);
            }

            Tile rightTile = tilesManager.GetTile(transform.position, transform.right);
            if (rightTile && rightTile.CharacterOnTile == gameObject)
            {
                rightTile.SetCharacterOnTile(null);
                rightTile.SetFree(true);
            }
        }

		#endregion
	}
}
