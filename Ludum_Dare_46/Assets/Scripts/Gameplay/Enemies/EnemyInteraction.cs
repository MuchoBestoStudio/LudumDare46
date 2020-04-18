using UnityEngine;
using MuchoBestoStudio.LudumDare.Map;

namespace MuchoBestoStudio.LudumDare.Gameplay.Enemies
{
	public class EnemyInteraction : MonoBehaviour
	{
		#region Variables

		#endregion

		#region Interact

		public void Interact()
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
