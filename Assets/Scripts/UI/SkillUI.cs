using CliffLeeCL;
using Coffee.UISoftMask;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] SkillIconConfig skillIconConfig;
    [SerializeField] Image baseIcon;
    [SerializeField] Image skillIcon;
    [SerializeField] SoftMask softMask;
    [SerializeField] TMP_Text text;
    [SerializeField] bool disableOnChoose = false;
    [SerializeField] bool showInputKey = true;
    [SerializeField] bool chooseWithKeyboard = false;
    [SerializeField] [ShowIf("chooseWithKeyboard")] InputAction inputAction;
    
    SkillType skillType;
    int currentSkillIndex;

    private void OnEnable()
    {
        if (chooseWithKeyboard)
        {
            inputAction.Enable();
        }
        EventManager.Instance.onChooseChaserSkill += OnChooseChaserSkill;
        EventManager.Instance.onChooseEscaperSkill += OnChooseEscaperSkill;
    }


    private void OnDisable()
    {
        EventManager.Instance.onChooseChaserSkill -= OnChooseChaserSkill;
        EventManager.Instance.onChooseEscaperSkill -= OnChooseEscaperSkill;
    }

    private void Update()
    {
        if (chooseWithKeyboard)
        {
            if (inputAction.triggered)
            {
                OnClicked();
            }
        }
        softMask.showMaskGraphic = false;
        softMask.showMaskGraphic = true;
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
        if (showInputKey)
        {
            text.text = skillIconConfig.GetSkillInputKey(currentSkillIndex);
        }
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
