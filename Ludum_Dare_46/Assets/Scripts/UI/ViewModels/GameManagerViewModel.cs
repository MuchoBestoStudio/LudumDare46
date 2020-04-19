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
        private Text _currencyText = null;
        [SerializeField]
        private string _timeTaggedString = null;
        [SerializeField]
        private string _currencyTaggedString = null;

        private CurrencySystem _currencySystem = null;

        public void Start()
        {
            _currencySystem = FindObjectOfType<CurrencySystem>();
            _gameManager = GameManager.Instance;
            UpdateTimeText();
            _gameManager.onTimeUpdated += UpdateTimeText;
            if (_currencySystem)
            {
                _currencySystem.OnCurrencyChanged += UpdateCurrencyText;
            }
        }

        private void UpdateTimeText()
        {
            string resultString;
            resultString = StringTagReplacer.ReplaceTag(_timeTaggedString, "[VALUE]", ((int)_gameManager.GameTime).ToString());
            _timeText.text = resultString;
        }

        private void UpdateCurrencyText(uint value)
        {
            if (_currencySystem)
            {
                string resultString;
                resultString = StringTagReplacer.ReplaceTag(_currencyTaggedString, "[VALUE]", _currencySystem.Currency.ToString());
                _currencyText.text = resultString;
            }
        }
    }
}