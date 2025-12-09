using TMPro;
using UnityEngine;

public class SkillDataInfoUI : MonoBehaviour {
    public TextMeshProUGUI dataName, power, cost;

    public void Init(SkillData data) {
        dataName.SetText(data.Name);
        power.SetText(data.Power.ToString("F1"));
        cost.SetText(data.Cost.ToString("F1"));
    }
}