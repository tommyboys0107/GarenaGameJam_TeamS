using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Defines the status of enemies.
/// </summary>
[CreateAssetMenu(fileName = "SkillIconConfig", menuName = "Scriptable Objects/Skill icon config", order = 1)]
public class SkillIconConfig : ScriptableObject 
{
    [PreviewField] [SerializeField] Sprite[] skillIconArr;
    [SerializeField] string[] skillInputKeyArr;

    public Sprite GetSkillIcon(int skillIndex)
    {
        return skillIndex - 1 < skillIconArr.Length ? skillIconArr[skillIndex - 1] : null;
    }
    
    public string GetSkillInputKey(int skillIndex)
    {
        return skillIndex - 1 < skillInputKeyArr.Length ? skillInputKeyArr[skillIndex - 1] : "";
    }
}
