using System;
using TMPro;
using UnityEngine;

public class SkillInstanceUI : MonoBehaviour {
    public TextMeshProUGUI skillName;
    public SkillDataInfoUI skillDataInfoPrefab;
    public SkillInstance mySkillInstance;

    private void OnEnable() {
        mySkillInstance.OnSkillSet += Init;
    }

    private void OnDisable() {
        if (mySkillInstance) mySkillInstance.OnSkillSet -= Init;
    }

    private void Init(ActionSkill skill) {
        skillName.text = skill.SkillNode.name;
        foreach (var skillData in skill.skillDatas) {
            SkillDataInfoUI dataInfo = Instantiate(skillDataInfoPrefab, transform);
            dataInfo.Init(skillData);
        }
    }
}