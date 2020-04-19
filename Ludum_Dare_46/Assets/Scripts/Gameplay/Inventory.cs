using System;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay
{
    public class Axe
    {
        public uint Life;
    }

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
            _combustibleAmount = (uint)Mathf.Min(value, MaxCombustibleAmount);
            if (onCombustibleAmountChanged != null)
            {
                onCombustibleAmountChanged.Invoke(_combustibleAmount);
            }
        }

        public void AddCombustible(uint value)
        {
            if (CanAddCombustible())
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

        public bool CanAddCombustible()
        {
            return _combustibleAmount < _maxCombustiblesAmount;
        }

        private Axe _playerAxe = null;
        public Axe PlayerAxe => _playerAxe;

        private void SetAxe(uint axeDurability)
        {
            _playerAxe = new Axe();
            _playerAxe.Life = axeDurability;
        }

        public void UseAxe()
        {
            if (_playerAxe == null)
            {
                return;
            }
            if ((--_playerAxe.Life) == 0)
            {
                _playerAxe = null;
            }
        }

        public void PickUpAxe()
        {
            SetAxe((uint)_inventoryData.AxeDurability.LevelValue);
        }

        void Start()
        {
            SetMaxCombustibleAmount((uint)_inventoryData.MaxCombustiblesAmount.LevelValue);
            if (_inventoryData.AxeDurability.Level > 0)
            {
                PickUpAxe();
            }
            SetCombustibleAmount(0);
        }
    }
}