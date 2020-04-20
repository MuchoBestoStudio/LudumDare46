using System;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay.Fire
{
    public class FireSource : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private FireData _fireData = null;

        [SerializeField]
        private Inventory _playerInventory = null;

        [SerializeField]
        private GameObject _interactEffect = null;

        public Action<uint, int> onCombustibleAmountChanged = null;
        public Action onNoCombustibleLeft = null;
        public Action<ECharacter> onInteraction = null;

        // Timer
        private float _currentCombustibleUpdateTimer = 0.0f;
        private float _combustibleUpdateTimer = 0.0f;

        [SerializeField]
        private uint _combustibleAmount = 0;
        public uint CombustibleAmount => _combustibleAmount;

        public void SetCombustibleAmount(uint value)
        {
            int deltaValue = (int)value - (int)_combustibleAmount;
            _combustibleAmount = value;
            if (onCombustibleAmountChanged != null)
            {
                onCombustibleAmountChanged.Invoke(_combustibleAmount, deltaValue);
            }

            if (_combustibleAmount == 0 && onNoCombustibleLeft != null)
            {
                onNoCombustibleLeft.Invoke();
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
            _currentCombustibleUpdateTimer += UnityEngine.Time.deltaTime;

            if (_currentCombustibleUpdateTimer > _combustibleUpdateTimer)
            {
                _currentCombustibleUpdateTimer = 0.0f;
                if (_combustibleAmount > 0)
                {
                    SetCombustibleAmount(_combustibleAmount - 1);
                }
            }
        }

        public void Interact(ECharacter character)
        {
            _interactEffect.SetActive(true);
            Invoke("DisableVisualInteractif", .1f);
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

            onInteraction?.Invoke(character);
        }

        private void DisableVisualInteractif()
        {
            _interactEffect.SetActive(false);
        }
    }
}
