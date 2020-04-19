using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	public class PlayerAudio : MonoBehaviour
	{
		#region Variables

		[Header("Globals")]
		[SerializeField, Tooltip("")]
		private PlayerMovements _movements = null;

		[Header("Voices")]
		[SerializeField, Tooltip("")]
		private AudioSource _voiceSource = null;
		[SerializeField, Tooltip("")]
		private	AudioClip	_voiceLoseClip = null;
		[SerializeField, Tooltip("")]
		private AudioClip[] _voicesClip = new AudioClip[0];
		[SerializeField, Tooltip("")]
		private Vector2 _interval = Vector2.one;

		public float VoiceTimer { get; private set; } = 0f;

		[Header("FootSteps")]
		[SerializeField, Tooltip("")]
		private AudioSource _footStepsSource = null;
		[SerializeField, Tooltip("")]
		private AudioClip _stopFootStepClip = null;
		[SerializeField, Tooltip("")]
		private AudioClip[] _footStepsClips = new AudioClip[0];

		#endregion

		#region MonoBehaviour's methods

		private void OnEnable()
		{
			_movements.onMovePerformed += PlayerMovements_OnMovePerformed;
			_movements.onStopMoving += PlayerMovements_OnStopMoving;
		}

		private void OnDisable()
		{
			_movements.onMovePerformed -= PlayerMovements_OnMovePerformed;
			_movements.onStopMoving -= PlayerMovements_OnStopMoving;
		}

		private void Update()
		{
			VoiceTimer -= Time.deltaTime;

			if (VoiceTimer <= 0f)
			{
				VoiceTimer += Random.Range(_interval.x, _interval.y);

				PlayRandomVoices();
			}
		}

		private void PlayerMovements_OnMovePerformed()
		{
			PlayRandomFootStep();
		}

		private void PlayerMovements_OnStopMoving()
		{
			PlayStopFootStep();
		}

		#endregion

		#region Sounds

		public void PlayVoiceLose()
		{
			_voiceSource.PlayOneShot(_voiceLoseClip);
		}

		public void PlayRandomVoices()
		{
			if (!_voiceSource.isPlaying)
			{
				_voiceSource.clip = _voicesClip[Random.Range(0, _voicesClip.Length)];
				_voiceSource.Play();
			}
		}

		public void PlayRandomFootStep()
		{
			_footStepsSource.PlayOneShot(_footStepsClips[Random.Range(0, _footStepsClips.Length)]);
		}

		public void PlayStopFootStep()
		{
			_footStepsSource.PlayOneShot(_stopFootStepClip);
		}

		#endregion
	}
}
