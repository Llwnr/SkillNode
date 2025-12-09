using System.Collections.Generic;

public class ActionSkill : IExecutable{
    private SkillNode _skillNode;
    private List<SkillData> _skillDatas;

    public ActionSkill(SkillNode skillNode, List<SkillData> skillDatas) {
        _skillNode = skillNode;
        _skillDatas = skillDatas;
    }

    //You would include logic here, such as if enemy has vulnerable then increase the skill's power by 50%.
    public void Execute(string someTargetContexts) {
        foreach (var skillData in _skillDatas) {
            _skillNode.Execute(skillData);
        }
    }
}
