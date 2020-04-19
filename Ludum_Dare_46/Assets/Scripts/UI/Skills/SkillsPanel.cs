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
            if (system)
            {
                _currencyText.text = "$" + system.Currency;
                system.OnCurrencyChanged += UpdateVisualButtons;
            }

            UpdateVisualButtons(0u);
		}

		private void OnDisable()
		{
            if (system)
            {
                system.OnCurrencyChanged -= UpdateVisualButtons;
            }
		}

		private void UpdateVisualButtons(uint currency)
		{
            if (system)
            {
                _currencyText.text = "$" + system.Currency;
            }
            else
            {
                _currencyText.text = "$0";
            }

			foreach (SkillButton skillButton in _skillButtons)
			{
				skillButton.UpdateVisual(system);
			}
		}
	}
}