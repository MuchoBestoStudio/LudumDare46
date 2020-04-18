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
		private Gameplay.SkillData _skillData;
		[SerializeField]
		private TextMeshProUGUI _nameText;
		[SerializeField]
		private TextMeshProUGUI _levelText;
		[SerializeField]
		private TextMeshProUGUI _priceText;
		[SerializeField]
		private TextMeshProUGUI _valueText;
		[SerializeField]
		private Button _upgradeButton;

		private void OnEnable()
		{
			int skillLevel = _skillData.Level;
			_nameText.text = _skillData.name;
			_levelText.text = skillLevel.ToString();
			_priceText.text = "Price : " + _skillData.PriceCurve.Evaluate(skillLevel).ToString();
			_valueText.text = "Value : " + _skillData.ValueCurve.Evaluate(skillLevel).ToString();

			_upgradeButton.onClick.AddListener(UpgradeSkill);
		}

		private void OnDisable()
		{
			_upgradeButton.onClick.RemoveListener(UpgradeSkill);
		}

		private void UpgradeSkill()
		{
			int skillLevel = ++_skillData.Level;
			_levelText.text = skillLevel.ToString();
			_priceText.text = "Price : " + _skillData.PriceCurve.Evaluate(skillLevel).ToString();
			_valueText.text = "Value : " + _skillData.ValueCurve.Evaluate(skillLevel).ToString();
		}
	}
}