using System;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private InventoryData _inventoryData = null;

        public Action<uint> onCombustibleAmountChanged = null;
        public Action<uint> onMaxCombustibleAmountChanged = null;

        [SerializeField]
        private uint _maxCombustiblesAmount = 0;
        public uint MaxCombustibleAmount => _maxCombustiblesAmount;

        public void SetMaxCombustibleAmount(uint value)
        {
            _maxCombustiblesAmount = value;
            if (onMaxCombustibleAmountChanged != null)
            {
                onMaxCombustibleAmountChanged.Invoke(_maxCombustiblesAmount);
            }
        }

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
            return _combustibleAmount + value <= _maxCombustiblesAmount;
        }

        void Start()
        {
            SetMaxCombustibleAmount((uint)_inventoryData.MaxCombustiblesAmount.LevelValue);
            SetCombustibleAmount(0);
        }
    }
}