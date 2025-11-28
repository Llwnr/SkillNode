using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireElement", menuName = "ModifierNode/FireElement")]
public class FireElementModifier : ModifierNode {
    public override List<SkillNode> CalculateOutputs(SkillNode inputSkillNode){
        var outputNodes = new List<SkillNode>();
        SkillData inputData =  inputSkillNode.Data;

        // Logic: Mutate data to be Fire
        var dataC = inputData.Clone();
        dataC.Cost += 1;
        dataC.Element = "Fire";
        dataC.Name = dataC.Name.Replace("B", "C"); // Just to match your diagram naming

        // Create the Output Node (Fireball C)
        SkillNode nodeC = UnityEngine.Object.Instantiate(inputSkillNode);
        nodeC.Init(dataC);
            
        outputNodes.Add(nodeC);

        return outputNodes;
    }
}