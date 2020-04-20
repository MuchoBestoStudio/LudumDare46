using MuchoBestoStudio.LudumDare.Map;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay._3C
{
	[DisallowMultipleComponent]
	public class PlayerAudio : MonoBehaviour
	{
		#region Variables

		[Header("Globals")]
		[SerializeField, Tooltip("")]
		private PlayerMovements _movements = null;
		[SerializeField, Tooltip("")]
		private	PlayerInteractions _interactions = null;
		[SerializeField, Tooltip("")]
		private Inventory _inventory = null;

		[Header("Voices")]
		[SerializeField, Tooltip("")]
		private AudioSource _voiceSource = null;
		[SerializeField, Tooltip("")]
		private	AudioClip	_voiceLoseClip = null;
		[SerializeField, Tooltip("")]
		private AudioClip[] _voicesClip = new AudioClip[0];
		[SerializeField, Tooltip("")]
		private Vector2		_interval = Vector2.one;

		public float VoiceTimer { get; private set; } = 0f;

		[Header("Interactions")]
		[SerializeField, Tooltip("")]
		private	AudioSource	_interactSource = null;
		[SerializeField, Tooltip("")]
		private	AudioClip	_errorInteractClip = null;
		[SerializeField, Tooltip("")]
		private	AudioClip	_pickAxeClip = null;
		[SerializeField, Tooltip("")]
		private	AudioClip	_pickWoodLogClip = null;
		[SerializeField, Tooltip("")]
		private	AudioClip	_axeBrokeClip = null;
		[SerializeField, Tooltip("")]
		private	AudioClip	_treeFall = null;
		[SerializeField, Tooltip("")]
		private AudioClip[]	_cuttingTreeClips = new AudioClip[0];

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
			_movements.onMovePerformed += PlayRandomFootStep;
			_movements.onStopMoving += PlayStopFootStep;

			_interactions.onAxeNotAvailable += PlayErrorInteract;
			_interactions.onInventoryFull += PlayErrorInteract;
			_interactions.onPickingAxe += PlayPickingAxe;
			_interactions.onPickingWoodLog += PlayPickingWoodLog;
			_interactions.onTreeCut += PlayerInteractions_OnTreeCut;

			_inventory.onAxeBroke += PlayAxeBroke;
		}

		private void OnDisable()
		{
			_movements.onMovePerformed -= PlayRandomFootStep;
			_movements.onStopMoving -= PlayStopFootStep;

			_interactions.onPickingAxe -= PlayPickingAxe;
			_interactions.onPickingWoodLog -= PlayPickingWoodLog;
			_interactions.onTreeCut -= PlayerInteractions_OnTreeCut;

			_inventory.onAxeBroke -= PlayAxeBroke;
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

		private void PlayerInteractions_OnTreeCut()
		{
			PlayRandomCuttingTree();

			ResourceTile resTile = _interactions.InteractedTile as ResourceTile;
			if (resTile != null && resTile.Resource == null)
			{
				PlayTreeFall();
			}
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

		public void PlayErrorInteract()
		{
			_interactSource.PlayOneShot(_errorInteractClip);
		}

		public void PlayPickingAxe()
		{
			_interactSource.PlayOneShot(_pickAxeClip);
		}

		public void PlayPickingWoodLog()
		{
			_interactSource.PlayOneShot(_pickWoodLogClip);
		}

		public void PlayAxeBroke()
		{
			_interactSource.PlayOneShot(_axeBrokeClip);
		}

		public void PlayTreeFall()
		{
			_interactSource.PlayOneShot(_treeFall);
		}

		public void PlayRandomCuttingTree()
		{
			_interactSource.PlayOneShot(_cuttingTreeClips[Random.Range(0, _cuttingTreeClips.Length)]);
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
