using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay
{
	public class FireSourceAudio : MonoBehaviour
	{
		#region Variables

		[Header("Globals")]
		[SerializeField, Tooltip("")]
		private	FireSource	_source = null;

		[Header("Ambiant")]
		[SerializeField, Tooltip("")]
		private	AudioSource	_ambiant = null;
		[SerializeField, Tooltip("")]
		private	float		_minAmbiantVolume = 0f;
		[SerializeField, Tooltip("")]
		private	float		_maxAmbiantVolume = 0f;

		[Header("Interact")]
		[SerializeField, Tooltip("")]
		private	AudioSource	_interact = null;
		[SerializeField, Tooltip("")]
		private	AudioClip	_throwCombustible = null;
		[SerializeField, Tooltip("")]
		private AudioClip	_blowingFire = null;

		#endregion

		#region MonoBehaviour's methods

		private void OnEnable()
		{
			//_source.onCombustibleAmountChanged += ;
			//_source.onNoCombustibleLeft += ;
		}

		private void OnDisable()
		{
			
		}

		#endregion

		#region Sounds

		public void ChangeAmbiantVolume(float volume)
		{
			_ambiant.volume = Mathf.Clamp(volume, _minAmbiantVolume, _maxAmbiantVolume);
		}

		public void PlayThrowingCombustible()
		{
			_interact.PlayOneShot(_throwCombustible);
		}

		public void PlayBlowingFire()
		{
			_interact.PlayOneShot(_blowingFire);
		}

		#endregion
	}
}
