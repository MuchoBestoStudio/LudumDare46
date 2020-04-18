using UnityEngine;

[CreateAssetMenu(fileName = "FireData", menuName = "ScriptableObjects/FireData", order = 0)]
public class FireData : ScriptableObject
{
    [Tooltip("Amount of combustibles in the fire when the game starts")]
    public MuchoBestoStudio.LudumDare.Gameplay.SkillData BaseCombustibles;
    [Tooltip("Timer before a combustible is consumed by the fireplace (in seconds)")]
    public MuchoBestoStudio.LudumDare.Gameplay.SkillData CombustibleTimer;
}