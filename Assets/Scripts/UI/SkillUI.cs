using CliffLeeCL;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] SkillIconConfig skillIconConfig;
    [SerializeField] SkillType skillType;
    [SerializeField] Image skillIcon;
    
    int currentSkillIndex;

    public void Init(int skillIndex)
    {
        currentSkillIndex = skillIndex;
        skillIcon.sprite = skillIconConfig.GetSkillIcon(currentSkillIndex);
    }
    
    public void OnClicked()
    {
        if (skillType == SkillType.Chaser)
        {
            EventManager.Instance.OnChooseChaserSkill((ChaserSkill)currentSkillIndex);
        }
        else if (skillType == SkillType.Escaper)
        {
            EventManager.Instance.OnChooseEscaperSkill((EscaperSkill)currentSkillIndex);
        }
    }
}
