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

		public	Tile InteractedTile { get; private set; } = null;
		public	bool IsCuttingTree { get; private set; } = false;

		#endregion

		#region Events

		public Action	onPickingWoodLog = null;
		public Action	onPickingAxe = null;
		public Action	onCutingTree = null;
		public Action	onTreeCut = null;
		public Action	onInteractionCompleted = null;

		public Action	onAxeNotAvailable = null;
		public Action	onInventoryFull = null;

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
			if (_animator.IsPlayingAnyInteractions() || IsCuttingTree)
			{
				return;
			}

			transform.position = new Vector3((int)transform.position.x, transform.position.y, (int)transform.position.z);

			InteractedTile = tilesManager.GetTile(transform.position, transform.forward);
			if (InteractedTile != null || InteractedTile.Free)
			{
				if (InteractedTile.CharacterOnTile.Count > 0)
					return;
				if (InteractedTile is AxeTile)
				{
					if (onPickingAxe != null)
					{
						onPickingAxe.Invoke();
					}
				}
				else if (InteractedTile is ResourceTile)
				{
					if (_playerInventory.IsFullOfCombustible())
					{
						if (onInventoryFull != null)
						{
							onInventoryFull.Invoke();
						}
						return;
					}

					ResourceTile resTile = InteractedTile as ResourceTile;

					Assert.IsNotNull(resTile, nameof(PlayerInteractions) + ": PlayerController_OnInteractActionPerformed(), resTile should not be null.");

					if (resTile.Resource != null)
					{
						if (resTile.Resource.AxeDependent)
						{
							if (_playerInventory.PlayerAxe == null)
							{
								if (onAxeNotAvailable != null)
								{
									onAxeNotAvailable.Invoke();
								}
								return;
							}

							IsCuttingTree = true;

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

				InteractedTile.Interact(ECharacter.PLAYER);

				if (onInteractionCompleted != null)
				{
					onInteractionCompleted.Invoke();
				}
			}
		}

		private void PlayerAnimationReceiver_OnTreeCut()
		{
			IsCuttingTree = false;

			InteractedTile.Interact(ECharacter.PLAYER);

			if (onTreeCut != null)
			{
				onTreeCut.Invoke();
			}
			if (onInteractionCompleted != null)
			{
				onInteractionCompleted.Invoke();
			}
		}
	}
}