using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireElement", menuName = "ModifierNode/FireElement")]
public class FireElementModifier : ModifierNode {
    public override List<SkillData> CalculateOutputs(SkillData inputData) {
        SkillData modifiedData = inputData.Clone();
        modifiedData.Element = "Fire";
        modifiedData.Name += " C";
        modifiedData.Cost += 1;
        
        return new List<SkillData>{modifiedData};
    }
}