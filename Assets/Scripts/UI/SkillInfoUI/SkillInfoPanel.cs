using System.Text;
using TMPro;
using UnityEngine;

public class SkillInfoPanel : MonoBehaviour {
    public static SkillInfoPanel Instance;

    [SerializeField] private GameObject panelRoot; // The visual part (Image/Background)
    [SerializeField] private TextMeshProUGUI infoText; // The text component

    private void Awake() {
        Instance = this;
        Hide(); // Start hidden
    }

    public void Show(ActionSkill skill) {
        StringBuilder sb = new StringBuilder();
        
        sb.AppendLine($"<size=120%><b>{skill.SkillNode.name} x{skill.skillDatas.Count}</b></size>");
        sb.AppendLine($"<color=#CCCCCC>Total Cost: {skill.skillDatas[0].Cost * skill.skillDatas.Count}</color>");
        sb.AppendLine(""); 

        SkillData data = skill.skillDatas[0];
        sb.AppendLine($"   Power: {data.Power} | {data.Element}");

        if (data.StatusEffects.Count > 0) {
            sb.AppendLine($"   <color=orange>Effects: {string.Join(", ", data.StatusEffects.Keys)}</color>");
        }
        sb.AppendLine("");

        infoText.text = sb.ToString();
        panelRoot.SetActive(true);
    }

    public void Hide() {
        panelRoot.SetActive(false);
    }
}