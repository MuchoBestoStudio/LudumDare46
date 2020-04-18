using UnityEngine;
using UnityEngine.UI;

namespace MuchoBestoStudio.LudumDare.UI.Menu
{
	public class SettingsPanel : MonoBehaviour
	{
		#region Variables

		[Header("Canvas")]
		[SerializeField, Tooltip("")]
		private Canvas _settingsCanvas = null;
		[SerializeField, Tooltip("")]
		private Canvas _mainMenuCanvas = null;

		[Header("Buttons")]
		[SerializeField, Tooltip("")]
		private Button _back = null;

		#endregion

		#region MonoBehaviour's Methods

		private void OnEnable()
		{
			_back.onClick.AddListener(OnBackButtonClicked);
		}

		private void OnDisable()
		{
			_back.onClick.RemoveListener(OnBackButtonClicked);
		}

		#endregion

		#region Buttons

		private void OnBackButtonClicked()
		{
			_settingsCanvas.enabled = false;
			_mainMenuCanvas.enabled = true;
		}

		#endregion
	}
}
