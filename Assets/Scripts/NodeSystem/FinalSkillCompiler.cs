using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FinalSkillCompiler : MonoBehaviour
{
    [SerializeField]private SkillNodeManager skillNodeManager;

    public void GetAllActionSkills()
    {
        List<ActionSkill> actionSkills = new List<ActionSkill>();
        
        List<SkillNodeInstance> instances = skillNodeManager.Instances;
        foreach (var instance in instances) {
            actionSkills.Add(GetActionableSkill(instance));
        }

        foreach (var skill in actionSkills) {
            skill.Execute("");
        }
        // return actionSkills;
    }

    private ActionSkill GetActionableSkill(SkillNodeInstance instance) {
        List<SkillNodeInstance> leafInstances = instance.GetLeaves();
        List<SkillData> finalSkillDatas = new List<SkillData>();
        leafInstances.ForEach(i => finalSkillDatas.Add(i.RuntimeData));
        return new ActionSkill(instance.SourceSkillNode, finalSkillDatas);
    }
}