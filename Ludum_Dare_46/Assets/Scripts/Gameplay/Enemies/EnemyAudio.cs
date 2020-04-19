using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay.Enemies
{
	public class EnemyAudio : MonoBehaviour
	{
		#region Variables

		[Header("Audio")]
		[SerializeField, Tooltip("")]
		private AudioSource _sfx = null;
		[SerializeField, Tooltip("")]
		private AudioClip[] _appearing = null;

		#endregion

		#region MonoBehaviour's methods

		private void OnEnable()
		{
			PlayAppearingSound();
		}

		#endregion

		#region Sounds

		public void PlayAppearingSound()
		{
			_sfx.PlayOneShot(_appearing[Random.Range(0, _appearing.Length)]);
		}

		#endregion
	}
}
