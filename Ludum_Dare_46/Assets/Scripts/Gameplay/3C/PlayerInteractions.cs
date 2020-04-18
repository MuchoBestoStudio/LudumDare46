using MuchoBestoStudio.LudumDare.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	public class PlayerInteractions : MonoBehaviour
	{
		[SerializeField, Tooltip("")]
		private PlayerController _controller = null;
		[SerializeField]
		private Map.TilesManager tilesManager = null;

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