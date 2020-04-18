using UnityEngine;
using UnityEngine.UI;

using MuchoBestoStudio.LudumDare.Gameplay;

namespace MuchoBestoStudio.LudumDare.UI.ViewModels
{
    public class FireSourceViewModel : MonoBehaviour
    {
        private FireSource _fireSource = null;
        [SerializeField]
        private Text _combustibleText = null;

        private int tagIndex = -1;

        void Start()
        {
            if (_combustibleText)
            {
                tagIndex = _combustibleText.text.IndexOf("[VALUE]");
            }
        }

        public void SetFireSource(FireSource fireSource)
        {
            _fireSource = fireSource;
            UpdateCombustibleText(_fireSource.CombustibleAmount);
            _fireSource.onCombustibleAmountChanged += UpdateCombustibleText;
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