using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuchoBestoStudio.LudumDare.UI.Skill
{
	public class SkillsPanel : MonoBehaviour
	{
		[SerializeField]
		private SkillButton[] _skillButtons = null;
		Gameplay.CurrencySystem system = null;

		private void OnEnable()
		{
			system = FindObjectOfType<Gameplay.CurrencySystem>();

		}

		private void UpdateVisualButtons()
		{

		}
	}
}