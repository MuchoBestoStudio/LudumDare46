using UnityEngine;

namespace MuchoBestoStudio.LudumDare.UI.Menu
{
	public class MenuAudio : MonoBehaviour
	{
		#region Variables

		[SerializeField, Tooltip("")]
		private	AudioSource	_source = null;
		[SerializeField, Tooltip("")]
		private	AudioClip	_highlightedClip = null;
		[SerializeField, Tooltip("")]
		private	AudioClip	_pressedClip = null;

		#endregion

		#region Sounds

		public void PlayHighlightedClip()
		{
			_source.PlayOneShot(_highlightedClip);
		}

		public void PlayPressedClip()
		{
			_source.PlayOneShot(_pressedClip);
		}

		#endregion
	}
}
