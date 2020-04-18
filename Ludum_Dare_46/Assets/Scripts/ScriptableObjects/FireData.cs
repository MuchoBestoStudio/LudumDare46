using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay
{
    [CreateAssetMenu(fileName = "FireData", menuName = "ScriptableObjects/FireData", order = 0)]
    public class FireData : ScriptableObject
    {
        [Tooltip("Amount of combustibles in the fire when the game starts")]
        public SkillData BaseCombustibles;
        [Tooltip("Timer before a combustible is consumed by the fireplace (in seconds)")]
        public SkillData CombustibleTimer;
    }
}