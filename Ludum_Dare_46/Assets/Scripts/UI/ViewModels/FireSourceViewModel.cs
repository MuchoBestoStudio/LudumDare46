using UnityEngine;
using UnityEngine.UI;

using MuchoBestoStudio.LudumDare.Gameplay;
using MuchoBestoStudio.LudumDare.Gameplay.Fire;

namespace MuchoBestoStudio.LudumDare.UI.ViewModels
{
    public class FireSourceViewModel : MonoBehaviour
    {
        private FireSource _fireSource = null;
        [SerializeField]
        private Text _combustibleText = null;
        [SerializeField]
        private string _taggedString = null;

        public void SetFireSource(FireSource fireSource)
        {
            _fireSource = fireSource;
            UpdateCombustibleText(0, 0);
            _fireSource.onCombustibleAmountChanged += UpdateCombustibleText;
        }

        private void UpdateCombustibleText(uint value, int deltaValue)
        {
            string resultString;
            resultString = StringTagReplacer.ReplaceTag(_taggedString, "[VALUE]", _fireSource.CombustibleAmount.ToString());
            _combustibleText.text = resultString;
        }
    }
}