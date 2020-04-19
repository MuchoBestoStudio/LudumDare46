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
			_upgradeButton.onClick.AddListener(UpgradeSkill);
		}

		private void OnDisable()
		{
			_upgradeButton.onClick.RemoveListener(UpgradeSkill);
		}

		public void UpdateVisual(Gameplay.CurrencySystem currencySystem)
		{
			int skillLevel = _skillData.Level;

			_nameText.text = _skillData.name;
			_levelText.text = skillLevel.ToString();

			if (skillLevel == _skillData.MaxLevel)
			{
				_levelText.text = "Max level";
				_priceText.text = "Price : ...";
			}
			else
			{
				_priceText.text = "Price : " + _skillData.PriceCurve.Evaluate(skillLevel).ToString();
				_valueText.text = "Value : " + _skillData.ValueCurve.Evaluate(skillLevel).ToString();
			}

			uint newPrice = (uint)_skillData.ValueCurve.Evaluate(_skillData.Level);
			if (currencySystem && (!currencySystem.CanAfford(newPrice) || skillLevel == _skillData.MaxLevel))
			{
				_upgradeButton.interactable = false;
			}
		}

		private void UpgradeSkill()
		{
			Gameplay.CurrencySystem system = FindObjectOfType<Gameplay.CurrencySystem>();
            if (!system)
            {
                return;
            }

			int skillLevel = _skillData.Level;

			if (system.Pay((uint)_skillData.ValueCurve.Evaluate(skillLevel)) != true)
			{
				return;
			}

			_skillData.Level = ++skillLevel;
			UpdateVisual(system);
		}
	}
}