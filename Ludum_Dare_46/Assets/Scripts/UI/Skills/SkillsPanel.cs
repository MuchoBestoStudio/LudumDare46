using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MuchoBestoStudio.LudumDare.UI.Skill
{
	public class SkillsPanel : MonoBehaviour
	{
		[SerializeField]
		private SkillButton[] _skillButtons = null;
		[SerializeField]
		private TextMeshProUGUI _currencyText = null;
		Gameplay.CurrencySystem system = null;

		private void OnEnable()
		{
			system = FindObjectOfType<Gameplay.CurrencySystem>();
			_currencyText.text = system.Currency + " $";

			UpdateVisualButtons(0u);

			system.OnCurrencyChanged += UpdateVisualButtons;
		}

		private void OnDisable()
		{
			system.OnCurrencyChanged -= UpdateVisualButtons;
		}

		private void UpdateVisualButtons(uint currency)
		{
			_currencyText.text = system.Currency + " $";

			foreach (SkillButton skillButton in _skillButtons)
			{
				skillButton.UpdateVisual(system);
			}
		}
	}
}