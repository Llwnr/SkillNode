using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SplitterMod", menuName = "ModifierNode/SplitterMod")]
public class SplitterModifier : ModifierNode{
    public override List<SkillNode> CalculateOutputs(SkillNode inputSkillNode) {
        var outputNodes = new List<SkillNode>();
        SkillData inputData = inputSkillNode.Data;
        
        SkillData modifiedData = inputData.Clone();
        modifiedData.Cost += 2;
        modifiedData.Cost *= 0.5f;
        modifiedData.Power *= 0.6f;

        // Logic: Create Fireball A (Top Branch)
        var dataA = modifiedData.Clone();
        dataA.Name += " A";
        SkillNode nodeA = ScriptableObject.Instantiate(inputSkillNode);
        nodeA.Init(dataA);
        outputNodes.Add(nodeA);

        // Logic: Create Fireball B (Bottom Branch)
        var dataB = modifiedData.Clone();
        dataB.Name += " B";
        SkillNode nodeB = ScriptableObject.Instantiate(inputSkillNode);
        nodeB.Init(dataB);
        outputNodes.Add(nodeB);


        Debug.Log($"SplitterMod outputs: {outputNodes.Count}");
        return outputNodes;
    }
}