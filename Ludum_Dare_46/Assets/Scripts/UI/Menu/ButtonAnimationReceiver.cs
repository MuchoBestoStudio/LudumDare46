using UnityEngine;

namespace MuchoBestoStudio.LudumDare.UI.Menu
{
	public class ButtonAnimationReceiver : MonoBehaviour
	{
		#region Variables

		[SerializeField, Tooltip("")]
		public	MenuAudio	_menuAudio = null;

		#endregion

		#region Methods

		public void OnButtonHighlighted()
		{
			_menuAudio.PlayHighlightedClip();
		}

		public void OnButtonPressed()
		{
			_menuAudio.PlayPressedClip();
		}

		#endregion
	}
}
