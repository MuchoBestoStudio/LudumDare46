using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MuchoBestoStudio.LudumDare.UI.Skill
{
	public class SkillButton : MonoBehaviour
	{
		[SerializeField]
		private Gameplay.SkillData _skillData = null;
		[SerializeField]
		private TextMeshProUGUI _nameText = null;
		[SerializeField]
		private TextMeshProUGUI _levelText = null;
		[SerializeField]
		private TextMeshProUGUI _priceText = null;
		[SerializeField]
		private TextMeshProUGUI _valueText = null;
		[SerializeField]
		private Button _upgradeButton = null;

		private void OnEnable()
		{
			int skillLevel = _skillData.Level;
			_nameText.text = _skillData.name;
			_levelText.text = skillLevel.ToString();
			_priceText.text = "Price : " + _skillData.PriceCurve.Evaluate(skillLevel).ToString();
			_valueText.text = "Value : " + _skillData.ValueCurve.Evaluate(skillLevel).ToString();

			Gameplay.CurrencySystem system = FindObjectOfType<Gameplay.CurrencySystem>();
			UpdateVisual(system);

			_upgradeButton.onClick.AddListener(UpgradeSkill);
		}

		private void OnDisable()
		{
			_upgradeButton.onClick.RemoveListener(UpgradeSkill);
		}

		public void UpdateVisual(Gameplay.CurrencySystem system)
		{
			if (system.CanAfford((uint)_skillData.ValueCurve.Evaluate(_skillData.Level)) != true)
			{
				_upgradeButton.interactable = false;
			}
		}

		private void UpgradeSkill()
		{
			Gameplay.CurrencySystem system = FindObjectOfType<Gameplay.CurrencySystem>();
			int skillLevel = _skillData.Level;

			if (skillLevel == _skillData.MaxLevel)
			{
				_levelText.text = "Max level";
				_priceText.text = "Price : ...";
				_upgradeButton.interactable = false;
				return;
			}

			if (system.Pay((uint)_skillData.ValueCurve.Evaluate(skillLevel)) != true)
			{
				return;
			}

			_skillData.Level = ++skillLevel;
			_levelText.text = skillLevel.ToString();
			_priceText.text = "Price : " + _skillData.PriceCurve.Evaluate(skillLevel).ToString();
			_valueText.text = "Value : " + _skillData.ValueCurve.Evaluate(skillLevel).ToString();
		}
	}
}