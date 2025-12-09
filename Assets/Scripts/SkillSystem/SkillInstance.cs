using System;
using UnityEngine;

public class SkillInstance : MonoBehaviour {
    private ActionSkill _skill;
    public Action<ActionSkill> OnSkillSet;

    public void Init(ActionSkill skill) {
        _skill = skill;
        OnSkillSet?.Invoke(skill);
    }
}