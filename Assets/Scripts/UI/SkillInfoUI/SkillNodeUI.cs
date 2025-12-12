using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillNodeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private SkillNodeInstance associatedNodeInstance;
    [SerializeField] private TextMeshProUGUI nameBox, pwrBox, costBox;

    private ActionSkill _cachedFinalSkill;
    
    public void UpdateVisuals(ActionSkill compiledSkill) {
        nameBox.text = compiledSkill.SkillNode.name;
        pwrBox.text = (compiledSkill.skillDatas[0].Power * compiledSkill.skillDatas.Count).ToString("F00");
        costBox.text = (compiledSkill.skillDatas[0].Cost * compiledSkill.skillDatas.Count).ToString("F00");

        _cachedFinalSkill = compiledSkill;
        SkillInfoPanel.Instance.Show(_cachedFinalSkill);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (SkillInfoPanel.Instance != null) {
            SkillInfoPanel.Instance.Show(_cachedFinalSkill);
        }
    }

    // 4. Detect Exit
    public void OnPointerExit(PointerEventData eventData) {
        if (SkillInfoPanel.Instance != null) {
            SkillInfoPanel.Instance.Hide();
        }
    }
}