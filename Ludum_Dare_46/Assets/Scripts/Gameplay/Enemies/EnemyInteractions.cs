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
			_movements.onEnemyReachEnd -= EnemyMovements_onEnemyReachEnd;
		}

		private void EnemyMovements_onEnemyReachEnd(EnemyMovements _)
		{
			InteractWithFrontTile();
            Tile currentTile = TilesManager.Instance.GetTile(transform.position);
            if (currentTile.CharacterOnTile == gameObject)
            {
                currentTile.SetCharacterOnTile(null);
                currentTile.SetFree(true);
            }
        }

		#endregion

		#region Interact

		public override void Interact(ECharacter character)
		{
			if (character == ECharacter.PLAYER)
			{
				_animator.Disappear();
                Tile currentTile = TilesManager.Instance.GetTile(transform.position);
                if (currentTile.CharacterOnTile == gameObject)
                {
                    currentTile.SetCharacterOnTile(null);
                    currentTile.SetFree(true);
                }
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

		#endregion
	}
}
