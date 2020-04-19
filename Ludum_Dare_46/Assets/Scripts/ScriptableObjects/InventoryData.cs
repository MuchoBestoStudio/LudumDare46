using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "ScriptableObjects/InventoryData", order = 1)]
    public class InventoryData : ScriptableObject
    {
        [Tooltip("Amount of combustibles the player can hold simultaneously")]
        public SkillData MaxCombustiblesAmount;
        public SkillData AxeDurability;
    }
}