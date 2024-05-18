using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class SkillPos : MonoBehaviour
{
    [SerializeField] 
    private RectTransform rectTransform;
    [SerializeField]
    private float ratio = 0.5f;
    
    private void Start()
    {
        SetSkillPos();
    }

    [Button("SetSkillPos")]
    public void SetSkillPos()
    {
        rectTransform.anchorMin = new Vector2(ratio, rectTransform.anchorMin.y);
        rectTransform.anchorMax = new Vector2(ratio, rectTransform.anchorMax.y);
        rectTransform.anchoredPosition = Vector2.zero;
    }
}
