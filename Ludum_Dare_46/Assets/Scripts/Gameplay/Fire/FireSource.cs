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
        private int _currentCombustibleUpdateTick = 0;
        private int _combustibleUpdateTick = 0;
		public uint _Damage = 1;

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
            _combustibleUpdateTick = (int)_fireData.CombustibleTimer.LevelValue;
            _currentCombustibleUpdateTick = _combustibleUpdateTick;
            GameManager.Instance.onTimeUpdated += OnUseCombustible;
        }

        void OnUseCombustible()
        {
            --_currentCombustibleUpdateTick;

            if (_currentCombustibleUpdateTick == 0)
            {
                _currentCombustibleUpdateTick = _combustibleUpdateTick;
                if (_combustibleAmount > 0)
                {
                    SetCombustibleAmount(_combustibleAmount - 1);
                }
            }
        }

        public void Interact(ECharacter character)
        {
			if (character == ECharacter.PLAYER)
			{
				Inventory inventory = FindObjectOfType<Inventory>();
				if (inventory)
				{
                    _interactEffect.SetActive(true);
                    Invoke("DisableVisualInteractif", .1f);
                    uint combustiblesAmount = inventory.CombustibleAmount;
                    inventory.RemoveCombustibles(combustiblesAmount);
                    AddCombustibles(combustiblesAmount);
                    GameManager.Instance.OnTickUpdate();
                }
            }
			else
			{
				RemoveCombustibles(_Damage);
			}

            onInteraction?.Invoke(character);
        }

        private void DisableVisualInteractif()
        {
            _interactEffect.SetActive(false);
        }
    }
}
