using System;
using CliffLeeCL;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] SkillIconConfig skillIconConfig;
    [SerializeField] Image baseIcon;
    [SerializeField] Image skillIcon;
    [SerializeField] bool disableOnChoose = false;
    
    SkillType skillType;
    int currentSkillIndex;

    private void OnEnable()
    {
       EventManager.Instance.onChooseChaserSkill += OnChooseChaserSkill;
       EventManager.Instance.onChooseEscaperSkill += OnChooseEscaperSkill;
    }


    private void OnDisable()
    {
        EventManager.Instance.onChooseChaserSkill -= OnChooseChaserSkill;
        EventManager.Instance.onChooseEscaperSkill -= OnChooseEscaperSkill;
    }
    
    private void OnChooseEscaperSkill(EscaperSkill obj)
    {
        if (skillType == SkillType.Escaper && disableOnChoose)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnChooseChaserSkill(ChaserSkill obj)
    {
        if (skillType == SkillType.Chaser && disableOnChoose)
        {
            gameObject.SetActive(false);
        }
    }

    public void Init(int skillIndex, SkillType type, SkillIconConfig iconConfig = null)
    {
        gameObject.SetActive(true);
        currentSkillIndex = skillIndex;
        skillType = type;
        skillIconConfig = iconConfig ?? skillIconConfig;
        skillIcon.sprite = skillIconConfig.GetSkillIcon(currentSkillIndex);
    }
    
    public void SetBaseColor(Color color)
    {
        baseIcon.color = color;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
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
