using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MuchoBestoStudio.LudumDare.UI.Menu
{
	public class PlayPanel : MonoBehaviour
	{
		#region Variables

		[Header("Scenes")]
		[SerializeField, Tooltip("")]
		private string _gameplayScene = string.Empty;

		[Header("Canvas")]
		[SerializeField, Tooltip("")]
		private Canvas _mainCanvas = null;
		[SerializeField, Tooltip("")]
		private Canvas _playCanvas = null;

		[Header("Buttons")]
		[SerializeField, Tooltip("")]
		private Button _play = null;
		[SerializeField, Tooltip("")]
		private Button _back = null;

		#endregion

		#region MonoBehaviour's Methods

		private void OnEnable()
		{
			_play.onClick.AddListener(OnPlayButtonClicked);
			_back.onClick.AddListener(OnBackButtonClicked);
		}

		private void OnDisable()
		{
			_play.onClick.RemoveListener(OnPlayButtonClicked);
			_back.onClick.RemoveListener(OnBackButtonClicked);
		}

		#endregion

		#region Buttons

		private void OnPlayButtonClicked()
		{

			SceneManager.LoadScene(_gameplayScene, LoadSceneMode.Single);
		}

		private IEnumerator LoadGame()
		{
			var gameScene = SceneManager.LoadSceneAsync(_gameplayScene, LoadSceneMode.Single);
			gameScene.allowSceneActivation = false;

			yield return new WaitForSeconds(3f);
			gameScene.allowSceneActivation = true;
		}

		private void OnBackButtonClicked()
		{
			_mainCanvas.enabled = true;
			_playCanvas.enabled = false;
		}

		#endregion
	}
}
