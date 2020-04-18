using UnityEngine;
using UnityEngine.UI;

namespace MuchoBestoStudio.LudumDare.UI.Menu
{
	public class MainMenuPanel : MonoBehaviour
	{
		#region Variables

		[Header("Canvas")]
		[SerializeField, Tooltip("")]
		private	Canvas	_mainCanvas = null;
		[SerializeField, Tooltip("")]
		private Canvas	_playCanvas = null;
		[SerializeField, Tooltip("")]
		private	Canvas	_settingsCanvas = null;
		[SerializeField, Tooltip("")]
		private	Canvas	_creditsCanvas = null;

		[Header("Buttons")]
		[SerializeField, Tooltip("")]
		private Button _play = null;
		[SerializeField, Tooltip("")]
		private Button _settings = null;
		[SerializeField, Tooltip("")]
		private	Button _credits = null;
		[SerializeField, Tooltip("")]
		private Button _quit = null;

		#endregion

		#region MonoBehaviour's Methods

		private void OnEnable()
		{
			_play.onClick.AddListener(OnPlayButtonClicked);
			_settings.onClick.AddListener(OnSettingsButtonClicked);
			_credits.onClick.AddListener(OnCreditsButtonClicked);
			_quit.onClick.AddListener(OnQuitButtonClicked);

			_mainCanvas.enabled = true;
			_playCanvas.enabled = false;
			_settingsCanvas.enabled = false;
			_creditsCanvas.enabled = false;
		}

		private void OnDisable()
		{
			_play.onClick.RemoveListener(OnPlayButtonClicked);
			_settings.onClick.RemoveListener(OnSettingsButtonClicked);
			_credits.onClick.RemoveListener(OnCreditsButtonClicked);
			_quit.onClick.RemoveListener(OnQuitButtonClicked);
		}

		#endregion

		#region Buttons' callback

		private void OnPlayButtonClicked()
		{
			_mainCanvas.enabled = false;
			_playCanvas.enabled = true;
		}

		private void OnSettingsButtonClicked()
		{
			_mainCanvas.enabled = false;
			_settingsCanvas.enabled = true;
		}

		private void OnCreditsButtonClicked()
		{
			_mainCanvas.enabled = false;
			_creditsCanvas.enabled = true;
		}

		private void OnQuitButtonClicked()
		{
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#else
				Application.Quit();
			#endif
		}

		#endregion
	}
}
