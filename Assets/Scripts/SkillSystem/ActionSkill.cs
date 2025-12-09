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
        foreach (var skillData in skillDatas) {
            _skillNode.Execute(context, skillData);
        }
    }
}
