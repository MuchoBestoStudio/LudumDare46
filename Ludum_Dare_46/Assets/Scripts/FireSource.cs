using System;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay
{
    public class FireSource : MonoBehaviour
    {
        [SerializeField]
        private FireData _fireData = null;

        public Action<uint> onCombustibleAmountChanged = null;
        public Action onNoCombustibleLeft = null;

        // Timer
        private float _currentCombustibleUpdateTimer = 0.0f;
        private float _combustibleUpdateTimer = 0.0f;

        [SerializeField]
        private uint _combustibleAmount = 0;
        public uint CombustibleAmount => _combustibleAmount;

        public void SetCombustibleAmount(uint value)
        {
            _combustibleAmount = value;
            if (onCombustibleAmountChanged != null)
            {
                onCombustibleAmountChanged.Invoke(_combustibleAmount);
            }
        }

        public void AddCombustible(uint value) { SetCombustibleAmount(_combustibleAmount + value); }

        void Start()
        {
            SetCombustibleAmount(_fireData.BaseConbustibles);
            _combustibleUpdateTimer = _fireData.CombustibleTimer;
            _currentCombustibleUpdateTimer = 0.0f;
        }

        void Update()
        {
            if (_currentCombustibleUpdateTimer > _combustibleUpdateTimer)
            {
                _currentCombustibleUpdateTimer = 0;
                if (_combustibleAmount > 1)
                {
                    SetCombustibleAmount(_combustibleAmount - 1);
                }
                else if (onNoCombustibleLeft != null)
                {
                    onNoCombustibleLeft.Invoke();
                }
            }

            _currentCombustibleUpdateTimer += UnityEngine.Time.deltaTime;
        }
    }
}
