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
        [SerializeField]
        private string _taggedString = null;

        public void SetInventory(Inventory inventory)
        {
            _playerInventory = inventory;
            UpdateCombustibleText(0);
            _playerInventory.onCombustibleAmountChanged += UpdateCombustibleText;
            _playerInventory.onMaxCombustibleAmountChanged += UpdateCombustibleText;
        }

        private void UpdateCombustibleText(uint value)
        {
            string resultString;
            resultString = StringTagReplacer.ReplaceTag(_taggedString, "[VALUE]", _playerInventory.CombustibleAmount.ToString());
            resultString = StringTagReplacer.ReplaceTag(resultString, "[MAX]", _playerInventory.MaxCombustibleAmount.ToString());
            _combustibleText.text = resultString;
        }
    }
}