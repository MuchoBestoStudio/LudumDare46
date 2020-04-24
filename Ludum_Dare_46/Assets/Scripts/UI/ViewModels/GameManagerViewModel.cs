using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
            _timeText.DOFade(1f, 3f).SetDelay(5f);
        }

        private void OnDisable()
        {
            _gameManager.onTimeUpdated -= UpdateTimeText;
            if (_currencySystem)
            {
                _currencySystem.OnCurrencyChanged -= UpdateCurrencyText;
            }
        }

        private void UpdateTimeText()
        {
            _timeText.text = _gameManager.GameTime.ToString();
        }

        private void UpdateCurrencyText(uint value)
        {
            if (_currencySystem)
            {
                string resultString;
                resultString = StringTagReplacer.ReplaceTag(_currencyTaggedString, "[VALUE]", value.ToString());
                _currencyText.text = resultString;
            }
        }
    }
}