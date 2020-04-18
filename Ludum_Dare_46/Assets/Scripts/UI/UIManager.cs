using UnityEngine;

using MuchoBestoStudio.LudumDare.Gameplay;
using MuchoBestoStudio.LudumDare.UI.ViewModels;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Inventory _playerInventory = null;
    [SerializeField]
    private FireSource _fireSource = null;

    // ViewModels
    private InventoryViewModel _inventoryViewModel = null;
    private FireSourceViewModel _fireSourceViewModel = null;

    private void Start()
    {
        if (_inventoryViewModel = GetComponent<InventoryViewModel>())
        {
            _inventoryViewModel.SetInventory(_playerInventory);
        }

        if (_fireSourceViewModel = GetComponent<FireSourceViewModel>())
        {
            _fireSourceViewModel.SetFireSource(_fireSource);
        }
    }
}