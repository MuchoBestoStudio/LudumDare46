using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	public class PlayerFootsteps : MonoBehaviour
	{
		#region Variables

		[SerializeField, Tooltip("")]
		private	PlayerMovements	_movements = null;

		[Header("Audio")]
		[SerializeField, Tooltip("")]
		private	AudioSource	_source = null;
		[SerializeField, Tooltip("")]
		private AudioClip _stopFootStep = null;
		[SerializeField, Tooltip("")]
		private	AudioClip[]	_footSteps = new AudioClip[0];

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

		public void PlayRandomFootStep()
		{
			_source.PlayOneShot(_footSteps[Random.Range(0, _footSteps.Length)]);
		}

		public void PlayStopFootStep()
		{
			_source.PlayOneShot(_stopFootStep);
		}

		#endregion
	}
}
