using UnityEngine;
using MuchoBestoStudio.LudumDare.Map;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	[DisallowMultipleComponent]
	public class PlayerInteractions : CharacterInteractions
	{
		[SerializeField, Tooltip("")]
		private PlayerController _controller = null;
		[SerializeField]
		private TilesManager tilesManager = null;

		private void OnEnable()
		{
			_controller.onInteractPerformed += PlayerController_OnInteractActionPerformed;
		}

		private void OnDisable()
		{
			_controller.onInteractPerformed -= PlayerController_OnInteractActionPerformed;
		}

		private void PlayerController_OnInteractActionPerformed()
		{
			transform.position = new Vector3((int)transform.position.x, transform.position.y, (int)transform.position.z);

			Tile forwardTile = tilesManager.GetTile(transform.position, transform.forward);
			if (forwardTile != null)
			{
				forwardTile.Interact(ECharacter.PLAYER);
			}
		}
	}
}