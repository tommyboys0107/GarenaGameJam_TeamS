using System;
using CliffLeeCL;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] SkillIconConfig skillIconConfig;
    [SerializeField] SkillType skillType;
    [SerializeField] Image skillIcon;
    
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
        if (skillType == SkillType.Escaper)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnChooseChaserSkill(ChaserSkill obj)
    {
        if (skillType == SkillType.Chaser)
        {
            gameObject.SetActive(false);
        }
    }

    public void Init(int skillIndex)
    {
        gameObject.SetActive(true);
        currentSkillIndex = skillIndex;
        skillIcon.sprite = skillIconConfig.GetSkillIcon(currentSkillIndex);
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
