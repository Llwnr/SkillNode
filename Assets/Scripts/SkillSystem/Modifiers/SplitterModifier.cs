using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SplitterMod", menuName = "ModifierNode/SplitterMod")]
public class SplitterModifier : ModifierNode{
    public override List<SkillData> CalculateOutputs(SkillData inputData) {
        var outputDatas = new List<SkillData>();
        // Logic: Create Fireball A (Top Branch)
        var dataA = inputData.Clone();
        dataA.Name += " A";
        dataA.Power *= 0.6f;
        dataA.Cost *= 0.5f;
        outputDatas.Add(dataA);

        // Logic: Create Fireball B (Bottom Branch)
        var dataB = inputData.Clone();
        dataB.Name += " B";
        dataB.Power *= 0.6f;
        dataB.Cost *= 0.5f;
        outputDatas.Add(dataB);

        return outputDatas;
    }
}