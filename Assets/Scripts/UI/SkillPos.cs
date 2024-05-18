using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class SkillPos : MonoBehaviour
{
    [SerializeField] 
    private RectTransform rectTransform;

    [Button("SetSkillPos")]
    public void SetSkillPos(float posRatio)
    {
        rectTransform.anchorMin = new Vector2(posRatio, rectTransform.anchorMin.y);
        rectTransform.anchorMax = new Vector2(posRatio, rectTransform.anchorMax.y);
        rectTransform.anchoredPosition = Vector2.zero;
    }
}
