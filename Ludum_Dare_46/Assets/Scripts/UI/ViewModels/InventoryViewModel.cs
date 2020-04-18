using UnityEngine;
using UnityEngine.UI;

using MuchoBestoStudio.LudumDare.Gameplay;

namespace MuchoBestoStudio.LudumDare.UI.ViewModels
{
    public class InventoryViewModel : MonoBehaviour
    {
        private Inventory _playerInventory = null;

        [SerializeField]
        private Text _combustibleText = null;

        private int tagIndex = -1;

        public void SetInventory(Inventory inventory)
        {
            if (_combustibleText)
            {
                tagIndex = _combustibleText.text.IndexOf("[VALUE]");
            }

            _playerInventory = inventory;
            UpdateCombustibleText(_playerInventory.CombustibleAmount);
            _playerInventory.onCombustibleAmountChanged += UpdateCombustibleText;
        }

        private void UpdateCombustibleText(uint value)
        {
            if (tagIndex >= 0)
            {
                string resultString = _combustibleText.text.Substring(0, tagIndex) + value.ToString();
                _combustibleText.text = resultString;
            }
        }
    }
}