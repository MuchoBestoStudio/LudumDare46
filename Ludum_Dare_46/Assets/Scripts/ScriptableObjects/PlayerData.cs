using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay
{
	public static class PlayerData
	{
		public static int BaseConbustiblesLevel
		{
			get
			{
				return PlayerPrefs.GetInt("BaseConbustiblesLevel");
			}
			set
			{
				PlayerPrefs.SetInt("BaseConbustiblesLevel", value);
			}
		}

		public static int CombustibleTimerLevel
		{
			get
			{
				return PlayerPrefs.GetInt("CombustibleTimerLevel");
			}
			set
			{
				PlayerPrefs.SetInt("CombustibleTimerLevel", value);
			}
		}

		public static void UpgradeSkill(string skillName, int addValue)
		{
			PlayerPrefs.SetInt(skillName, addValue);
		}
	}
}