using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSkillCompiler : MonoBehaviour
{
    [SerializeField]private SkillNodeManager skillNodeManager;
    [SerializeField] private SkillInventory skillInventory;

    public void CompileAndMoveToNextScene() {
        skillInventory.mySkills.Clear();

        foreach (var nodeInstance in skillNodeManager.Instances) {
            ActionSkill actionSkill =
                CompileSkillModifiers(nodeInstance.CoreSkillNode, nodeInstance.GetLinearModifierList());
            skillInventory.mySkills.Add(actionSkill);
        }
        
        SceneManager.LoadScene("TestScene");
    }

    public ActionSkill CompileSkillModifiers(SkillNode coreNode, List<ModifierNode> modNodes) {
        List<SkillData> currentBatch = new List<SkillData>();
        currentBatch.Add(coreNode.BaseData.Clone());

        foreach (var mod in modNodes) {
            List<SkillData> nextBatch = new List<SkillData>();

            foreach (var data in currentBatch) {
                nextBatch.AddRange(mod.Process(data));
            }

            currentBatch = nextBatch;
        }

        return new ActionSkill(coreNode, currentBatch);
    }
}