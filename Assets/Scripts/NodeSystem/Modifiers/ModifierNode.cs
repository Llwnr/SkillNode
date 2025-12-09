using System.Collections.Generic;
using UnityEngine;

public abstract class ModifierNode : ScriptableObject{
    // This function takes raw data, manipulates it then returns 
    public abstract List<SkillData> Process(SkillData inputData);
}