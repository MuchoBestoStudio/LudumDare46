using System;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay
{

    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private InventoryData _inventoryData = null;

        public Action<uint> onCombustibleAmountChanged = null;

        private uint _maxCombustiblesAmount = 0;

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

        public void AddCombustible(uint value)
        {
            if (CanAddCombustible(value))
            {
                SetCombustibleAmount(_combustibleAmount + value);
            }
        }

        public void RemoveCombustibles(uint value)
        {
            if (_combustibleAmount - value <= _combustibleAmount)
            {
                SetCombustibleAmount(_combustibleAmount - value);
            }
        }

        public void RemoveCombustible(){ RemoveCombustibles(1); }

        public bool CanAddCombustible(uint value)
        {
            return _combustibleAmount + value < _maxCombustiblesAmount;
        }

        void Start()
        {
            _maxCombustiblesAmount = _inventoryData.MaxCombustiblesAmount;
            SetCombustibleAmount(0);
        }
    }
}