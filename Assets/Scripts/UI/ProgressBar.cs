using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    Transform skillTriggerHolder;
    [SerializeField]
    GameObject skillTriggerPrefab;
    
    [Button("GenerateSkillTrigger")]
    public void GenerateSkillTrigger(int number)
    {
        while (skillTriggerHolder.childCount > 0)
        {
            DestroyImmediate(skillTriggerHolder.GetChild(0).gameObject);
        }
        DelayGenerateSkillTrigger(number);
    }
    
    async void DelayGenerateSkillTrigger(int number)
    {
        await UniTask.Delay(100);
        for (int i = 0; i < number; i++)
        {
            var skillTrigger = Instantiate(skillTriggerPrefab, skillTriggerHolder);
            skillTrigger.GetComponent<SkillPos>().SetSkillPos((i + 1f) / (number + 1f));
        }
    }
}
