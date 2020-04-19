using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuchoBestoStudio.LudumDare.UI.Menu
{
	public class CreditsPanel : MonoBehaviour
	{
		[Header("Canvas")]
		[SerializeField, Tooltip("")]
		private Canvas _mainCanvas = null;
		[SerializeField, Tooltip("")]
		private Canvas _creditsCanvas = null;

		[Header("Buttons")]
		[SerializeField, Tooltip("")]
		private Button _back = null;
		private void OnEnable()
		{
			_back.onClick.AddListener(OnBackButtonClicked);
		}

		private void OnDisable()
		{
			_back.onClick.RemoveListener(OnBackButtonClicked);
		}

		private void OnBackButtonClicked()
		{
			_mainCanvas.enabled = true;
			_creditsCanvas.enabled = false;
		}
	}
}
