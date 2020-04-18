using System;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay
{
    using MuchoBestoStudio.LudumDare.Gameplay._3C;

    public class FireSource : MonoBehaviour
    {
        [SerializeField]
        private FireData _fireData = null;

        [SerializeField]
        private Inventory _playerInventory = null;

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

        public void AddCombustibles(uint value) { SetCombustibleAmount(_combustibleAmount + value); }
        public void RemoveCombustibles(uint value)
        {
            if (_combustibleAmount - value <= _combustibleAmount)
            {
                SetCombustibleAmount(_combustibleAmount - value);
            }
        }

        private void AddCombustible()
        {
            if (_playerInventory && _playerInventory.CombustibleAmount > 0)
            {
                AddCombustibles(1);
            }
        }

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

        private void OnTriggerEnter(Collider other)
        {
            PlayerController pc = FindObjectOfType<PlayerController>();
            Inventory inventory = other.GetComponent<Inventory>();

            if (pc && inventory)
            {
                pc.onInteractPerformed -= AddCombustible;
                pc.onInteractPerformed -= inventory.RemoveCombustible;

                pc.onInteractPerformed += AddCombustible;
                pc.onInteractPerformed += inventory.RemoveCombustible;
            }

            
        }

        private void OnTriggerExit(Collider other)
        {
            PlayerController pc = FindObjectOfType<PlayerController>();
            if (pc)
            {
                pc.onInteractPerformed -= AddCombustible;
                Inventory inventory = other.GetComponent<Inventory>();
                if (inventory)
                {
                    pc.onInteractPerformed -= inventory.RemoveCombustible;
                }
            }
        }
    }
}
