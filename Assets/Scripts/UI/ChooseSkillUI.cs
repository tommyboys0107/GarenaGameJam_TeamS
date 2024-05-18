using System;
using System.Collections.Generic;
using CliffLeeCL;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChooseSkillUI : MonoBehaviour
{
    float chooseSkillTime;
    float elapsedChooseSkillTime = 0;
    bool startChooseSkill = false;
    
    [SerializeField] SkillType skillType;
    [SerializeField] List<SkillUI> skillUIList = new List<SkillUI>();
    
    void Start()
    {
        startChooseSkill = false;
        foreach (var skillUI in skillUIList)
        {
            skillUI.Hide();
        }
    }

    void Update()
    {
        if (startChooseSkill)
        {
            elapsedChooseSkillTime += Time.deltaTime;
            if (elapsedChooseSkillTime >= chooseSkillTime)
            {
                startChooseSkill = false;
            }
        }
    }
    
    public void Init(List<ChaserSkill> chaserSkillList, List<EscaperSkill> escaperSkillList, float chooseTime)
    {
        var remainingChaserSkillList = new List<ChaserSkill>((ChaserSkill[])Enum.GetValues(typeof(ChaserSkill)));
        var remainingEscaperSkillList = new List<EscaperSkill>((EscaperSkill[])Enum.GetValues(typeof(EscaperSkill)));
        chooseSkillTime = chooseTime;
        elapsedChooseSkillTime = 0;
        foreach (var skillUI in skillUIList)
        {
            // Find available skill
            var availableSkill = 0;
            var randomIndex = 0;
            if (skillType == SkillType.Chaser)
            {
                remainingChaserSkillList.RemoveAll(chaserSkillList.Contains);
                randomIndex = Random.Range(0, remainingChaserSkillList.Count);
                availableSkill = (int)remainingChaserSkillList[randomIndex];
                remainingChaserSkillList.RemoveAt(randomIndex);
            }
            else
            {
                remainingEscaperSkillList.RemoveAll(escaperSkillList.Contains);
                randomIndex = Random.Range(0, remainingEscaperSkillList.Count);
                availableSkill = (int)remainingEscaperSkillList[randomIndex];
                remainingEscaperSkillList.RemoveAt(randomIndex);
            }
            
            skillUI.Init(availableSkill);
        }
        
        startChooseSkill = true;
    }
    
}
