using System;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay
{
    public class CurrencySystem : MonoBehaviour
    {
        [SerializeField]
        private CurrencyData _data = null;
        public CurrencyData Data => _data;

        [SerializeField]
        private uint _currentCurrency = 0;
        public uint Currency => _currentCurrency;

        public Action<uint> OnCurrencyChanged = null;

        void Start()
        {
            DontDestroyOnLoad(this.gameObject);

            _currentCurrency = (uint)PlayerPrefs.GetInt("Currency", (int)_data.Current);
            OnCurrencyChanged?.Invoke(_currentCurrency);
        }

        public bool CanAfford(uint value)
        {
            return _currentCurrency >= value;
        }

        public bool Pay(uint value)
        {
            if (CanAfford(value))
            {
                _currentCurrency -= value;
                OnCurrencyChanged?.Invoke(_currentCurrency);
                PlayerPrefs.SetInt("Currency", (int)_currentCurrency);
                return true;
            }

            return false;
        }

        public void Earn(float survivalTime)
        {
            _currentCurrency = (uint)(survivalTime * _data.TimeMultiplier);
            OnCurrencyChanged?.Invoke(_currentCurrency);
            PlayerPrefs.SetInt("Currency", (int)_currentCurrency);
        }
    }
}