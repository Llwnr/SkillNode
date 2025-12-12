using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SkillNodeManager : MonoBehaviour{
    [FormerlySerializedAs("skillInventory")] [SerializeField]List<SkillNode> ownedNodes = new List<SkillNode>();
    [SerializeField]List<ModifierNode> modifierInventory = new List<ModifierNode>();
    [SerializeField] private SkillNodeInstance skillNodeInstancePrefab;
    [SerializeField] private ModifierNodeInstance modifierNodeInstancePrefab;
    [SerializeField] private GameObject skillNodeContainer, modifierNodeContainer;
    [SerializeField] private SkillInventory skillInventory;
    
    private List<SkillNodeInstance> _instances = new List<SkillNodeInstance>();
    public List<SkillNodeInstance> Instances => _instances;
    
    private void Start() {
        InstantiateNodes();
    }

    void InstantiateNodes() {
        foreach (var skillNode in ownedNodes) {
            InstantiateNodeInstance(skillNode);
        }

        foreach (var modifierNode in modifierInventory) {
            ModifierNodeInstance newModifierNode = Instantiate(modifierNodeInstancePrefab, modifierNodeContainer.transform);
            newModifierNode.Init(modifierNode);
        }
    }

    private void InstantiateNodeInstance(SkillNode skillNode) {
        SkillNodeInstance newSkillInstance = Instantiate(skillNodeInstancePrefab, skillNodeContainer.transform);
        newSkillInstance.Init(skillNode);
        _instances.Add(newSkillInstance);
    }

    public void CompileAndSwitchScene() {
        skillInventory.mySkills.Clear();
        foreach (var nodeInstance in Instances) {
            ActionSkill actionSkill = nodeInstance.CompileAndReturnSkill();
            skillInventory.mySkills.Add(actionSkill);
        }
        
        SceneManager.LoadScene("TestScene");
    }
}