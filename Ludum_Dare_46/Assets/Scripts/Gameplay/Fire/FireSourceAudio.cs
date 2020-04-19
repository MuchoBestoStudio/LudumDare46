using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay.Fire
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
		private	float		_maxCombustible = 5f;

		[Header("Interact")]
		[SerializeField, Tooltip("")]
		private	AudioSource	_sfx = null;
		[SerializeField, Tooltip("")]
		private	AudioClip	_throwCombustible = null;
		[SerializeField, Tooltip("")]
		private AudioClip	_blowingFire = null;
		[SerializeField, Tooltip("")]
		private	AudioClip	_fireExtinguished = null;

		#endregion

		#region MonoBehaviour's methods

		private void OnEnable()
		{
			_source.onCombustibleAmountChanged += FireSource_OnCombustibleAmountChanged;
			_source.onNoCombustibleLeft += FireSource_OnNoCombustibleLeft;
			_source.onInteraction += FireSource_OnInteraction;
		}

		private void OnDisable()
		{
			_source.onCombustibleAmountChanged -= FireSource_OnCombustibleAmountChanged;
			_source.onNoCombustibleLeft -= FireSource_OnNoCombustibleLeft;
			_source.onInteraction -= FireSource_OnInteraction;
		}

		private void FireSource_OnCombustibleAmountChanged(uint amount, int delta)
		{
			ChangeAmbiantVolume(amount / _maxCombustible);
            if (delta > 0)
            {
                PlayThrowingCombustible();
            }
        }

		private void FireSource_OnNoCombustibleLeft()
		{
			PlayFireExtinguished();
		}

		private void FireSource_OnInteraction(ECharacter character)
		{
			if (character == ECharacter.ENEMY)
			{
				PlayBlowingFire();
			}
		}

		#endregion

		#region Sounds

		public void ChangeAmbiantVolume(float volume)
		{
			_ambiant.volume = Mathf.Clamp(volume, 0f, 1f);
		}

		public void PlayThrowingCombustible()
		{
			_sfx.PlayOneShot(_throwCombustible);
		}

		public void PlayBlowingFire()
		{
			if (!_sfx.isPlaying)
			{
				_sfx.clip = _blowingFire;
				_sfx.Play();
			}
		}

		public void PlayFireExtinguished()
		{
			_sfx.PlayOneShot(_fireExtinguished);
		}

		#endregion
	}
}
