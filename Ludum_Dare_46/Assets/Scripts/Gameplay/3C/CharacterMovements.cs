using DG.Tweening;
using MuchoBestoStudio.LudumDare.Map;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	public class CharacterMovements : MonoBehaviour
	{
		#region Variables

		public	float	MoveDuration	=	1f;

		#endregion

		#region Methods

		public void Move(EDirection direction, TweenCallback onCompleted)
		{
			if (direction == EDirection.NONE || DOTween.IsTweening(transform) || TilesManager.Instance == null)
			{
				return;
			}

			Tile nextTile = TilesManager.Instance.GetTile(transform.position, transform.forward * Tile.SIZE);

			// note (rc) : Next Tile can be null if the arrive the the bounds of the arena
			if (nextTile != null && nextTile.Free)
			{
				transform.DOMove(nextTile.Center, MoveDuration, true).OnComplete(onCompleted);
			}
			else
			{
				onCompleted.Invoke();
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

		#endregion
	}
}
