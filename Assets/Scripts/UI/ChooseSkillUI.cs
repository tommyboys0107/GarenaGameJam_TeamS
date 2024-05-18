using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChooseSkillUI : MonoBehaviour
{
    enum SkillType
    {
        Chaser,
        Escaper,
    }
    
    float chooseSkillTime;
    float elapsedChooseSkillTime = 0;
    bool startChooseSkill = false;
    
    [SerializeField] SkillType skillType;
    [FormerlySerializedAs("skillPrefab")] [SerializeField] SkillUI skillUIPrefab;
    [FormerlySerializedAs("skillHolderList")] [FormerlySerializedAs("skillHolder")] [SerializeField] List<Transform> skillUIHolderList = new List<Transform>();
    
    void Start()
    {
        startChooseSkill = false;
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
        chooseSkillTime = chooseTime;
        elapsedChooseSkillTime = 0;

        foreach (var skillUIHolder in skillUIHolderList)
        {
            // Find available skill
            var availableSkill = 0;
            if (skillType == SkillType.Chaser)
            {
                do
                {
                    availableSkill = Random.Range(1, (int)ChaserSkill.Max);
                } while (chaserSkillList.Contains((ChaserSkill)availableSkill));

            }
            else
            {
                do
                {
                    availableSkill = Random.Range(1, (int)EscaperSkill.Max);
                } while (escaperSkillList.Contains((EscaperSkill)availableSkill));
            }
            
            var skillUI = Instantiate(skillUIPrefab, skillUIHolder);
            skillUI.Init(availableSkill);
        }
        
        startChooseSkill = true;
    }
    
}
