using System;
using CliffLeeCL;
using UnityEngine;

public class OwnedSkillUI : MonoBehaviour
{
    [SerializeField] SkillUI skillUIPrefab;
    [SerializeField] Transform chaserSkillUIHolder;
    [SerializeField] Transform escaperSkillUIHolder;
    [SerializeField] SkillIconConfig chaserSkillIconConfig;
    [SerializeField] SkillIconConfig escaperSkillIconConfig;
    [SerializeField] Color chaserSkillBaseColor;
    [SerializeField] Color escaperSkillBaseColor;

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

    private void OnChooseChaserSkill(ChaserSkill skill)
    {
        var skillUI = Instantiate(skillUIPrefab, chaserSkillUIHolder);
        skillUI.Init((int)skill, SkillType.Chaser, chaserSkillIconConfig);
        skillUI.SetBaseColor(chaserSkillBaseColor);
    }
    
    private void OnChooseEscaperSkill(EscaperSkill skill)
    {
        var skillUI = Instantiate(skillUIPrefab, escaperSkillUIHolder);
        skillUI.Init((int)skill, SkillType.Escaper, escaperSkillIconConfig);
        skillUI.SetBaseColor(escaperSkillBaseColor);
    }
}
