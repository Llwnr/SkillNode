using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SplitterMod", menuName = "ModifierNode/SplitterMod")]
public class SplitterModifier : ModifierNode
{
    public int numOfSplits;
    public float powerMult;
    public float costMult;
    public override List<SkillData> Process(SkillData inputData) {
        var outputDatas = new List<SkillData>();

        for (int i = 0; i < numOfSplits; i++) {
            SkillData splitData = inputData.Clone();
            splitData.Name += "Split " + 1f/numOfSplits + " ";
            splitData.Power *= powerMult;
            splitData.Cost *= costMult;
            splitData.Cost += costMult * cost;
            outputDatas.Add(splitData);
        }
        return outputDatas;
    }
}