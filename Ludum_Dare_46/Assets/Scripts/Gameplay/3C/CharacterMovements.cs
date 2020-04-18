using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	public class CharacterMovements : MonoBehaviour
	{
		#region Methods

		public void Move(EDirection direction)
		{
			if (direction == EDirection.NONE)
				return;

			switch (direction)
			{
				case EDirection.DOWN:
					transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
					transform.position += Vector3.back * Time.deltaTime;
					break;
				case EDirection.LEFT:
					transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
					transform.position += Vector3.left * Time.deltaTime;
					break;
				case EDirection.RIGHT:
					transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
					transform.position += Vector3.right * Time.deltaTime;
					break;
				case EDirection.UP:
					transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
					transform.position += Vector3.forward * Time.deltaTime;
					break;
				default:
					break;
			}
		}

		#endregion
	}
}
