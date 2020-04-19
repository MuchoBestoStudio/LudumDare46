using System;
using UnityEngine;
using MuchoBestoStudio.LudumDare.Map;
using UnityEngine.Assertions;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	[DisallowMultipleComponent]
	public class PlayerInteractions : CharacterInteractions
	{
		#region Variables

		[SerializeField, Tooltip("")]
		private PlayerController _controller = null;
		[SerializeField, Tooltip("")]
		private	PlayerAnimator _animator = null;
		[SerializeField]
		private TilesManager tilesManager = null;

		#endregion

		#region Events

		public Action	onPickingWoodLog = null;
		public Action	onPickingAxe = null;
		public Action	onCutingTree = null;
		public Action	onMenacingGhost = null;

		#endregion

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
			if (!IsPlayingIdleAnimation())
			{
				return;
			}

			transform.position = new Vector3((int)transform.position.x, transform.position.y, (int)transform.position.z);

			Tile forwardTile = tilesManager.GetTile(transform.position, transform.forward);
			if (forwardTile != null || forwardTile.Free)
			{
				forwardTile.Interact(ECharacter.PLAYER);

				if (forwardTile.CharacterOnTile != null)
				{
					if (onMenacingGhost != null)
					{
						onMenacingGhost.Invoke();
					}
					return;
				}
				if (forwardTile is AxeTile)
				{
					if (onPickingAxe != null)
					{
						onPickingAxe.Invoke();
					}
					return;
				}
				if (forwardTile is ResourceTile)
				{
					ResourceTile resTile = forwardTile as ResourceTile;

					Assert.IsNotNull(resTile, nameof(PlayerInteractions) + ": PlayerController_OnInteractActionPerformed(), resTile should not be null.");

					if (resTile.Resource != null)
					{
						if (resTile.Resource.AxeDependent)
						{
							if (onCutingTree != null)
							{
								onCutingTree.Invoke();
							}
						}
						else
						{
							if (onPickingWoodLog != null)
							{
								onPickingWoodLog.Invoke();
							}
						}
					}
					return;
				}
			}
		}

		private bool IsPlayingIdleAnimation()
		{
			return _animator.IsPlayingAnyIdles();
		}
	}
}