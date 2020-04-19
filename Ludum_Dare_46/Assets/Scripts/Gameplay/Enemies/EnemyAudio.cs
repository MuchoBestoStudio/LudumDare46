using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay.Enemies
{
	[DisallowMultipleComponent]
	public class EnemyAudio : MonoBehaviour
	{
		#region Variables

		[Header("Globals")]
		[SerializeField, Tooltip("")]
		private	EnemyAnimator _animator = null;

		[Header("Audio")]
		[SerializeField, Tooltip("")]
		private AudioSource _sfx = null;
		[SerializeField, Tooltip("")]
		private AudioClip[] _spookyClips = null;

		#endregion

		#region MonoBehaviour's methods

		private void OnEnable()
		{
			_animator.onEnemyStartToAppear += EnemyAnimator_StartToAppear;
			_animator.onEnemyStartToDisappear += EnemyAnimator_StartToDisappear;
		}

		private void OnDisable()
		{
			_animator.onEnemyStartToAppear -= EnemyAnimator_StartToAppear;
			_animator.onEnemyStartToDisappear -= EnemyAnimator_StartToDisappear;
		}

		private void EnemyAnimator_StartToAppear(EnemyAnimator _)
		{
			PlaySpookySound();
		}

		private void EnemyAnimator_StartToDisappear(EnemyAnimator _)
		{
			PlaySpookySound();
		}

		#endregion

		#region Sounds

		public void PlaySpookySound()
		{
			_sfx.PlayOneShot(_spookyClips[Random.Range(0, _spookyClips.Length)]);
		}

		#endregion
	}
}
