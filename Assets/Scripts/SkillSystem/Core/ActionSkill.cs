using System;
using System.Collections.Generic;

[Serializable]
public class ActionSkill : IExecutable{
    private SkillNode _skillNode; 
    public SkillNode SkillNode => _skillNode;
    public List<SkillData> skillDatas;

    public ActionSkill(SkillNode skillNode, List<SkillData> skillDatas) {
        _skillNode = skillNode;
        this.skillDatas = skillDatas;
    }

    //You would include logic here, such as if enemy has vulnerable then increase the skill's power by 50%.
    public void Execute(SkillExecutionContext context) {
        ExecutionLogicManager executionLogic = new ExecutionLogicManager(context);
        foreach (var skillData in skillDatas) {
            SkillData finalizedData =  executionLogic.ProcessBeforeExecution(skillData);
            _skillNode.Execute(context, finalizedData);
        }
    }
}

public class ExecutionLogicManager {
    private SkillExecutionContext _context;

    public ExecutionLogicManager(SkillExecutionContext context) {
        _context = context;
    }

    public SkillData ProcessBeforeExecution(SkillData data) {
        SkillData modifiedData = data.Clone();
        if (_context.Target.statusEffects.ContainsKey("Rupture") && _context.Target.statusEffects["Rupture"]) {
            modifiedData.Power *= 1.5f;
        }
        return modifiedData;
    }
}
