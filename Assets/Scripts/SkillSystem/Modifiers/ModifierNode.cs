using System.Collections.Generic;
using UnityEngine;

public abstract class ModifierNode : ScriptableObject{
    // This function takes raw data and turns it into the Next Skill Nodes
    public abstract List<SkillNode> CalculateOutputs(SkillNode inputSkillNode);
}