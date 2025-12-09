using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillNodeManager : MonoBehaviour{
    [SerializeField]List<SkillNode> skillInventory = new List<SkillNode>();
    [SerializeField]List<ModifierNode> modifierInventory = new List<ModifierNode>();
    [SerializeField] private SkillNodeInstance skillNodeInstancePrefab;
    [SerializeField] private ModifierNodeInstance modifierNodeInstancePrefab;
    [SerializeField] private GameObject skillNodeContainer, modifierNodeContainer;

    private List<SkillNodeInstance> _instances = new List<SkillNodeInstance>();
    public List<SkillNodeInstance> Instances => _instances;
    
    private void Start() {
        InstantiateNodes();
    }

    void InstantiateNodes() {
        foreach (var skillNode in skillInventory) {
            SkillNodeInstance newSkillInstance = Instantiate(skillNodeInstancePrefab, skillNodeContainer.transform);
            newSkillInstance.Init(skillNode, skillNode.DefaultData.Clone(), skillNodeInstancePrefab, modifierNodeContainer.transform);
            _instances.Add(newSkillInstance);
        }

        foreach (var modifierNode in modifierInventory) {
            ModifierNodeInstance newModifierNode = Instantiate(modifierNodeInstancePrefab, modifierNodeContainer.transform);
            newModifierNode.Init(modifierNode);
        }
    }

    public void Activate() {
        foreach (var skill in _instances) {
            skill.Activate();
        }
    }
}