using UnityEngine;

using MuchoBestoStudio.LudumDare.Gameplay;
using MuchoBestoStudio.LudumDare.UI.ViewModels;
using UnityEngine.UI;

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
        private GameObject PauseGameObject = null;

        // ViewModels
        private InventoryViewModel _inventoryViewModel = null;
        private FireSourceViewModel _fireSourceViewModel = null;

        void Start()
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.onGameOver += ShowGameOverPanel;
            gameManager.onPauseChanged += OnPauseChanged;
            if (_inventoryViewModel = GetComponent<InventoryViewModel>())
            {
                _inventoryViewModel.SetInventory(_playerInventory);
            }

            if (_fireSourceViewModel = GetComponent<FireSourceViewModel>())
            {
                _fireSourceViewModel.SetFireSource(_fireSource);
            }
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
            PauseGameObject.SetActive(isPaused);
        }
    }
}