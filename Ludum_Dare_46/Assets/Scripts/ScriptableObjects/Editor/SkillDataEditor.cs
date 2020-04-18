using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MuchoBestoStudio.LudumDare.Gameplay
{
	[CustomEditor(typeof(SkillData))]
	public class SkillDataEditor : Editor
	{
		private SkillData _skill;

		public override void OnInspectorGUI()
		{
			if (_skill == null)
			{
				_skill = (SkillData)target;
			}

			_skill.Level = EditorGUILayout.DelayedIntField("Level", _skill.Level);
			base.OnInspectorGUI();
		}
	}
}