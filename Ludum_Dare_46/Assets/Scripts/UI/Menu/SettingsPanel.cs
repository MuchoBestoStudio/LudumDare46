using UnityEngine;
using UnityEngine.Audio;
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

		[Header("Sounds")]
		[SerializeField]
		private Slider _masterSlider = null;
		[SerializeField]
		private Slider _musicsSlider = null;
		[SerializeField]
		private Slider _sfxSlider = null;
		[SerializeField]
		private AudioMixer _audioMixer = null;


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

		private void Start()
		{
			_masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
			_musicsSlider.value = PlayerPrefs.GetFloat("MusicsVolume", 1f);
			_sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
		}

		private void OnEnable()
		{
			_back.onClick.AddListener(OnBackButtonClicked);
			_reset.onClick.AddListener(OnShowResetPanel);

			_resetYes.onClick.AddListener(OnResetButtonClicked);
			_resetNo.onClick.AddListener(OnSwitchResetPanel);

			_masterSlider.onValueChanged.AddListener(ChangeMasterVolumeGoupe);
			_musicsSlider.onValueChanged.AddListener(ChangeMusicsVolumeGoupe);
			_sfxSlider.onValueChanged.AddListener(ChangeSFXVolumeGoupe);

			_resetPanel.SetActive(false);
		}

		private void OnDisable()
		{
			_back.onClick.RemoveListener(OnBackButtonClicked);
			_reset.onClick.RemoveListener(OnShowResetPanel);

			_resetYes.onClick.RemoveListener(OnResetButtonClicked);
			_resetNo.onClick.RemoveListener(OnSwitchResetPanel);

			_masterSlider.onValueChanged.RemoveListener(ChangeMasterVolumeGoupe);
			_musicsSlider.onValueChanged.RemoveListener(ChangeMusicsVolumeGoupe);
			_sfxSlider.onValueChanged.RemoveListener(ChangeSFXVolumeGoupe);
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

		private void OnShowResetPanel()
		{
			_resetPanel.SetActive(true);
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

		#region Slider
		private void ChangeMasterVolumeGoupe(float value)
		{
			_audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20f);
			PlayerPrefs.SetFloat("MasterVolume", value);
		}

		private void ChangeMusicsVolumeGoupe(float value)
		{
			_audioMixer.SetFloat("MusicsVolume", Mathf.Log10(value) * 20f - 8f);
			PlayerPrefs.SetFloat("MusicsVolume", value);
		}

		private void ChangeSFXVolumeGoupe(float value)
		{
			_audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20f - 12f);
			PlayerPrefs.SetFloat("SFXVolume", value);
		}
		#endregion
	}
}
