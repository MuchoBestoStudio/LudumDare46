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
		[SerializeField, Tooltip("")]
		private Button _reset = null;

		[Header("Reset")]
		[SerializeField]
		private GameObject _resetPanel = null;
		[SerializeField, Tooltip("")]
		private Button _resetYes = null;
		[SerializeField, Tooltip("")]
		private Button _resetNo = null;
		[SerializeField, Tooltip("")]
		private Gameplay.SkillData[] _allSkills = null;
		private Gameplay.CurrencySystem _currenySystem = null;

		#endregion

		#region MonoBehaviour's Methods

		private void OnEnable()
		{
			_back.onClick.AddListener(OnBackButtonClicked);
			_reset.onClick.AddListener(OnSwitchResetPanel);

			_resetYes.onClick.AddListener(OnResetButtonClicked);
			_resetNo.onClick.AddListener(OnSwitchResetPanel);

			_resetPanel.SetActive(false);

		}

		private void OnDisable()
		{
			_back.onClick.RemoveListener(OnBackButtonClicked);
			_reset.onClick.RemoveListener(OnSwitchResetPanel);

			_resetYes.onClick.RemoveListener(OnResetButtonClicked);
			_resetNo.onClick.RemoveListener(OnSwitchResetPanel);
		}

		#endregion

		#region Buttons

		private void OnBackButtonClicked()
		{
			_settingsCanvas.enabled = false;
			_mainMenuCanvas.enabled = true;
		}

		private void OnSwitchResetPanel()
		{
			_resetPanel.SetActive(!_resetPanel.activeSelf);
		}

		private void OnResetButtonClicked()
		{
			foreach(Gameplay.SkillData skill in _allSkills)
			{
				skill.ResetSkill();
			}
			FindObjectOfType<Gameplay.CurrencySystem>().ResetCurrency();
			_resetPanel.SetActive(false);
		}
		#endregion
	}
}
