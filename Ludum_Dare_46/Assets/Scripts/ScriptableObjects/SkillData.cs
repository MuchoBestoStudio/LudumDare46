using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay
{
	[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData", order = 1)]
	public class SkillData : ScriptableObject
	{
		public int Level { get { return PlayerPrefs.GetInt(name); } set { PlayerPrefs.SetInt(name, value); } }
		public int MaxLevel = 10;
		public AnimationCurve ValueCurve;
		public AnimationCurve PriceCurve;

		public float LevelValue { get { return ValueCurve.Evaluate(Level); } }

		[ContextMenu("ResetSkill")]
		private void ResetSkill()
		{
			PlayerPrefs.SetInt(name, 0);
		}
	}
}