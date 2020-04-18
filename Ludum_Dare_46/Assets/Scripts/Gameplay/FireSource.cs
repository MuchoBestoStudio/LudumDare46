using System;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay
{
    public class FireSource : MonoBehaviour, IInteractable
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
            SetCombustibleAmount((uint)_fireData.BaseCombustibles.LevelValue);
            _combustibleUpdateTimer = _fireData.CombustibleTimer.LevelValue;
            _currentCombustibleUpdateTimer = 0.0f;
        }

        void Update()
        {
            if (_currentCombustibleUpdateTimer > _combustibleUpdateTimer)
            {
                _currentCombustibleUpdateTimer = 0;
                if (_combustibleAmount > 0)
                {
                    SetCombustibleAmount(_combustibleAmount - 1);
                    if (_combustibleAmount == 0 && onNoCombustibleLeft != null)
                    {
                        onNoCombustibleLeft.Invoke();
                    }
                }
            }

            _currentCombustibleUpdateTimer += UnityEngine.Time.deltaTime;
        }

        public void Interact(ECharacter character)
        {
			if (character == ECharacter.PLAYER)
			{
				AddCombustible();
				Inventory inventory = FindObjectOfType<Inventory>();
				if (inventory)
				{
					inventory.RemoveCombustible();
				}
			}
			else
			{
				RemoveCombustibles(1);
			}
        }
    }
}
