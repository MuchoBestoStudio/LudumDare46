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
		[SerializeField, Tooltip("")]
		private PlayerAnimationsReceiver _receiver = null;
		[SerializeField]
		private Inventory _playerInventory = null;
		[SerializeField]
		private TilesManager tilesManager = null;

		private	Tile _forwardTile = null;

		#endregion

		#region Events

		public Action	onPickingWoodLog = null;
		public Action	onPickingAxe = null;
		public Action	onCutingTree = null;
		public Action	onInteractionCompleted = null;

		#endregion

		private void OnEnable()
		{
			_controller.onInteractPerformed += PlayerController_OnInteractActionPerformed;

			_receiver.onTreeCut += PlayerAnimationReceiver_OnTreeCut;
		}

		private void OnDisable()
		{
			_controller.onInteractPerformed -= PlayerController_OnInteractActionPerformed;

			_receiver.onTreeCut -= PlayerAnimationReceiver_OnTreeCut;
		}

		private void PlayerController_OnInteractActionPerformed()
		{
			if (_animator.IsPlayingAnyInteractions())
			{
				return;
			}

			transform.position = new Vector3((int)transform.position.x, transform.position.y, (int)transform.position.z);

			_forwardTile = tilesManager.GetTile(transform.position, transform.forward);
			if (_forwardTile != null || _forwardTile.Free)
			{
				if (_forwardTile.CharacterOnTile.Count > 0)
					return;
				if (_forwardTile is AxeTile)
				{
					if (onPickingAxe != null)
					{
						onPickingAxe.Invoke();
					}
				}
				else if (_forwardTile is ResourceTile)
				{
					ResourceTile resTile = _forwardTile as ResourceTile;

					Assert.IsNotNull(resTile, nameof(PlayerInteractions) + ": PlayerController_OnInteractActionPerformed(), resTile should not be null.");

					if (resTile.Resource != null)
					{
						if (resTile.Resource.AxeDependent)
						{
							if (_playerInventory.PlayerAxe == null)
								return;
							if (onCutingTree != null)
							{
								onCutingTree.Invoke();
							}

							// NOTE : Return to avoid interacting with Tree, waiting for animation event
							return;
						}
						else
						{
							if (onPickingWoodLog != null)
							{
								onPickingWoodLog.Invoke();
							}
						}
					}
				}

				_forwardTile.Interact(ECharacter.PLAYER);

				if (onInteractionCompleted != null)
				{
					onInteractionCompleted.Invoke();
				}
			}
		}

		private void PlayerAnimationReceiver_OnTreeCut()
		{
			_forwardTile.Interact(ECharacter.PLAYER);

			if (onInteractionCompleted != null)
			{
				onInteractionCompleted.Invoke();
			}
		}
	}
}