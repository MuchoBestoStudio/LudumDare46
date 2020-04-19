using System;
using DG.Tweening;
using MuchoBestoStudio.LudumDare.Map;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	public class CharacterMovements : MonoBehaviour, IInteractable
	{
		#region Variables

		public	float	MoveDuration	=	1f;

		#endregion

		#region Methods

		public void Move(EDirection direction, Action<bool> onCompleted)
		{
			if (direction == EDirection.NONE || DOTween.IsTweening(transform) || TilesManager.Instance == null)
			{
				return;
			}
            Tile currentTile = TilesManager.Instance.GetTile(transform.position);
			Tile nextTile = TilesManager.Instance.GetTile(transform.position, transform.forward * Tile.SIZE);

			// note (rc) : Next Tile can be null if the arrive the the bounds of the arena
			if (nextTile != null && nextTile.Free)
			{
				DOTween.defaultEaseType = Ease.Linear;
				transform.DOMove(nextTile.Center, MoveDuration, false)
						 .OnComplete(() => {
												if (onCompleted != null)
												{
													onCompleted.Invoke(true);
												}

                                                if (currentTile.CharacterOnTile == this)
                                                {
                                                    currentTile.SetCharacterOnTile(null);
                                                }

                                                 nextTile.SetCharacterOnTile(this);
											});
			}
			else
			{
				if (onCompleted != null)
				{
					onCompleted.Invoke(false);
				}
			}
		}

		public void LookTo(EDirection direction)
		{
			if (direction == EDirection.NONE)
			{
				return;
			}

			switch (direction)
			{
				case EDirection.DOWN:
					{
						transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
					}
					break;
				case EDirection.LEFT:
					{
						transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
					}
					break;
				case EDirection.RIGHT:
					{
						transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
					}
					break;
				case EDirection.UP:
					{
						transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
					}
					break;
				default:
					return;
			}
		}

        public virtual void Interact(ECharacter character)
        {
        }

        #endregion
    }
}
