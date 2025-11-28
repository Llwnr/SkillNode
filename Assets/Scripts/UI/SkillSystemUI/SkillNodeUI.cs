using TMPro;
using UnityEngine;

public class SkillNodeUI : MonoBehaviour{
    [SerializeField]private SkillNodeInstance associatedNodeInstance;
    [SerializeField] private TextMeshProUGUI nameBox, pwrBox, costBox;

    
    public void UpdateVisuals() {
        SkillData skillData = associatedNodeInstance.SkillData;
        
        nameBox.text = skillData.Name;
        pwrBox.text = skillData.Power.ToString("F00");
        costBox.text = skillData.Cost.ToString("F00");
    }
    
    
}