using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MuchoBestoStudio.LudumDare.UI.Skill
{
	public class SkillButton : MonoBehaviour
	{
		[System.Serializable]
		private struct SSkillDependence
		{
			public Gameplay.SkillData Skill;
			public int Level;
		}

		[Header("UI")]
		[SerializeField]
		private TextMeshProUGUI _priceText = null;
		[SerializeField]
		private Button _upgradeButton = null;
		[SerializeField]
		private Outline _validateEffect = null;

		[Header("Dependence")]
		[SerializeField]
		private SSkillDependence[] _skillsDependence = null;

		[Header("Effect")]
		[SerializeField]
		private Gameplay.SkillData _skillData = null;
		[SerializeField]
		private int _setLevel = 1;

		private void OnEnable()
		{
			_upgradeButton.onClick.AddListener(UpgradeSkill);
		}

		private void OnDisable()
		{
			_upgradeButton.onClick.RemoveListener(UpgradeSkill);
		}

		public void UpdateVisual(Gameplay.CurrencySystem currencySystem)
		{
			_validateEffect.enabled = false;

			if (!CheckDependence())
			{
				_upgradeButton.interactable = false;
				_priceText.enabled = false;
				return;
			}
			if (_skillData.Level >= _setLevel)
			{
				_upgradeButton.interactable = false;
				_priceText.enabled = false;
				_validateEffect.enabled = true;
				return;
			}

			uint price = (uint)_skillData.PriceCurve.Evaluate(_setLevel);
			_priceText.enabled = true;
			_priceText.text = "$ " + price;

			if (currencySystem == null || !currencySystem.CanAfford(price))
			{
				_upgradeButton.interactable = false;
				return;
			}

			_upgradeButton.interactable = true;
		}

		private bool CheckDependence()
		{
			foreach (SSkillDependence dependence in _skillsDependence)
			{
				if (dependence.Skill.Level < dependence.Level)
				{
					return false;
				}
			}

			return true;
		}

		private void UpgradeSkill()
		{
			Gameplay.CurrencySystem system = FindObjectOfType<Gameplay.CurrencySystem>();
			if (!system)
			{
				Debug.LogError("No currencySystem found");
				return;
			}

			if (system.CanAfford((uint)_skillData.ValueCurve.Evaluate(_setLevel)) != true)
			{
				return;
			}

			_skillData.Level = _setLevel;
			system.Pay((uint)_skillData.ValueCurve.Evaluate(_setLevel));
		}
	}
}