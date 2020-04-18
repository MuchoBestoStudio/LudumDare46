using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Map
{
	public class Resource : MonoBehaviour
	{
		public enum EType
		{
			Tree,
			Stick,
		}

		[SerializeField]
		private EType _type;
		[SerializeField]
		private uint _amount;
	}
}