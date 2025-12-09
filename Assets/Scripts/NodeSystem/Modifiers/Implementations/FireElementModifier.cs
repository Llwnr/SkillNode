using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireElement", menuName = "ModifierNode/FireElement")]
public class FireElementModifier : ModifierNode {
    public float powerBoost;
    public override List<SkillData> Process(SkillData inputData) {
        SkillData modifiedData = inputData.Clone();
        modifiedData.Element = "Fire";
        modifiedData.Name += " C";
        modifiedData.Cost += cost;
        modifiedData.Power += powerBoost;
        
        return new List<SkillData>{modifiedData};
    }
}