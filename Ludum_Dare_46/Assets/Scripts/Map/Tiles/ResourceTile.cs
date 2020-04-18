using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Map
{
	public class ResourceTile : Tile
	{
		[SerializeField]
		private Resource _resource = null;
		public override bool Free { get { return _resource == null; } }

		private void OnEnable()
		{
			if (_resource != null)
			{
				_resource.onCombustibleAmountChanged += OnResourceAmountChange;
			}
			else
			{
				Debug.LogWarning("Any resource set in : " + gameObject);
			}
		}

		private void OnDisable()
		{
			if (_resource != null)
			{
				_resource.onCombustibleAmountChanged -= OnResourceAmountChange;
			}
		}

		private void OnResourceAmountChange(uint amount)
		{
			if (amount == 0)
			{
				_resource.onCombustibleAmountChanged -= OnResourceAmountChange;
				_resource = null;
			}
		}

		public override void Interact()
		{
			if (_resource != null)
			{
				_resource.Interact();
			}
		}
	}
}