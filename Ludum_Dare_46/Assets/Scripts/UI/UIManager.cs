using UnityEngine;

using MuchoBestoStudio.LudumDare.Gameplay;
using MuchoBestoStudio.LudumDare.Gameplay.Fire;
using MuchoBestoStudio.LudumDare.UI.ViewModels;
using UnityEngine.UI;
using System.Collections;

namespace MuchoBestoStudio.LudumDare.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        GameObject _gameOverObject = null;
        [SerializeField]
        private Inventory _playerInventory = null;
        [SerializeField]
        private FireSource _fireSource = null;

        [SerializeField]
        private GameObject _pauseGameObject = null;
        [SerializeField]
        private GameObject _CombustibleWarningObject = null;

        [SerializeField]
        private uint _criticalCombustibleAmount = 0;

        // ViewModels
        private InventoryViewModel _inventoryViewModel = null;
        private FireSourceViewModel _fireSourceViewModel = null;

        private bool _isCombustibleLow = false;

        void Start()
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.onGameOver += ShowGameOverPanel;
            gameManager.onPauseChanged += OnPauseChanged;
            gameManager.onFireCombustibleChanged += OnFireCombustibleChanged;
            if (_inventoryViewModel = GetComponent<InventoryViewModel>())
            {
                _inventoryViewModel.SetInventory(_playerInventory);
            }

            if (_fireSourceViewModel = GetComponent<FireSourceViewModel>())
            {
                _fireSourceViewModel.SetFireSource(_fireSource);
            }

            if (_CombustibleWarningObject)
            {
                _CombustibleWarningObject.SetActive(false);
            }

            _CombustibleWarningObject.GetComponent<Animation>().Play();
        }

        private void ShowGameOverPanel()
        {
            if (_gameOverObject)
            {
                _gameOverObject.SetActive(true);
            }
        }

        private void HideGameOverPanel()
        {
            _gameOverObject.SetActive(false);
        }

        private void OnPauseChanged(bool isPaused)
        {
            _pauseGameObject.SetActive(isPaused);
        }

        private void OnFireCombustibleChanged(uint value, int delta)
        {
            // Fire combustible became low
            if (!_isCombustibleLow && delta < 0 && value < _criticalCombustibleAmount)
            {
                _isCombustibleLow = true;
                if (_CombustibleWarningObject)
                {
                    _CombustibleWarningObject.SetActive(true);
                }
            }
            // Fire combustible is not considered low anymore
            else if (delta > 0 && value > _criticalCombustibleAmount)
            {
                _isCombustibleLow = false;
                if (_CombustibleWarningObject)
                {
                    _CombustibleWarningObject.SetActive(false);
                }
            }
        }
    }
}