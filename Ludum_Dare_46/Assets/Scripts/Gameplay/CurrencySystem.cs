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

        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            _currentCurrency = _data.Current;
        }

        public bool CanAfford(uint value)
        {
            return _data.Current >= value;
        }

        public void Pay(uint value)
        {
            if (CanAfford(value))
            {
                _data.Current -= value;
            }
        }

        public void Earn(float survivalTime)
        {
            _currentCurrency += (uint)(survivalTime * _data.TimeMultiplier);
        }
    }
}