using System;
using DG.Tweening;
using MuchoBestoStudio.LudumDare.Map;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	[DisallowMultipleComponent]
	public class CharacterMovements : MonoBehaviour
	{
		#region Variables

		public	float	MoveDuration	=	1f;
		public	float	RotateDuration	=	1f;

		public	EDirection	MoveDirection { get; private set; } = EDirection.NONE;
		public	EDirection	LookDirection { get; private set; } = EDirection.UP;

		protected Tile _onTile = null;

		#endregion

		#region Events

		public Action onMovePerformed = null;

		#endregion

		#region Methods

        protected virtual void OnDisable()
        {
			StopMove();
		}

		public void StopMove()
		{
			DOTween.Kill(transform);
		}

		public void Move(EDirection direction, Action<bool> onCompleted)
		{
			if (direction == EDirection.NONE || TilesManager.Instance == null)
			{
				MoveDirection = EDirection.NONE;
				return;
			}
			if (IsMoving())
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
						 .OnStart(OnStartMoving)
						 .OnComplete(() => {
                                currentTile.RemoveCharacterOnTile(gameObject);
								nextTile.AddCharacterOnTile(gameObject);

                                if (onCompleted != null)
								{
									onCompleted.Invoke(true);
								}
							});

				if (onMovePerformed != null)
				{
					onMovePerformed.Invoke();
				}
			}
			else
			{
				if (onCompleted != null)
				{
					onCompleted.Invoke(false);
				}
			}
		}

		public void LookTo(EDirection direction, Action<bool> onCompleted)
		{
			if (direction == EDirection.NONE)
			{
				return;
			}

			float angle = 0f;

			switch (direction)
			{
				case EDirection.DOWN:
					{
						angle = 180f;
					}
					break;
				case EDirection.LEFT:
					{
						angle = -90f;
					}
					break;
				case EDirection.RIGHT:
					{
						angle = 90f;
					}
					break;
				case EDirection.UP:
					{
						angle = 0f;
					}
					break;
				default:
					return;
			}

			transform.DORotateQuaternion(Quaternion.AngleAxis(angle, Vector3.up), RotateDuration)
					 .OnComplete(() => {
											LookDirection = direction;

											if (onCompleted != null)
											{
												onCompleted.Invoke(true);
											}
										});
		}

		public bool IsMoving()
		{
			return DOTween.IsTweening(transform);
		}

		protected virtual void OnStartMoving()
		{ }

        #endregion

    }
}
