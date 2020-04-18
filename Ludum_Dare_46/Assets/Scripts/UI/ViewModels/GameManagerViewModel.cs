using UnityEngine;
using UnityEngine.UI;

using MuchoBestoStudio.LudumDare.Gameplay;

namespace MuchoBestoStudio.LudumDare.UI.ViewModels
{
    public class GameManagerViewModel : MonoBehaviour
    {
        private GameManager _gameManager = null;

        [SerializeField]
        private Text _timeText = null;
        [SerializeField]
        private string _taggedString = null;

        public void Start()
        {
            _gameManager = GameManager.Instance;
            UpdateTimeText();
            _gameManager.onTimeUpdated += UpdateTimeText;
        }

        private void UpdateTimeText()
        {
            string resultString;
            resultString = StringTagReplacer.ReplaceTag(_taggedString, "[VALUE]", ((int)_gameManager.GameTime).ToString());
            _timeText.text = resultString;
        }
    }
}