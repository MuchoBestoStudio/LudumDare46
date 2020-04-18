using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay
{
    [CreateAssetMenu(fileName = "CurrencyData", menuName = "ScriptableObjects/CurrencyData", order = 2)]
    public class CurrencyData : ScriptableObject
    {
        public uint Current;
        public float TimeMultiplier;
    }

}