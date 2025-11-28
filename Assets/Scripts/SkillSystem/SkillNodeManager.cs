using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillNodeManager : MonoBehaviour{
    [SerializeField]List<SkillNode> skillInventory = new List<SkillNode>();
    [SerializeField]List<ModifierNode> modifierInventory = new List<ModifierNode>();
    [SerializeField] private SkillNodeInstance skillNodeInstancePrefab;
    [SerializeField] private ModifierNodeInstance modifierNodeInstancePrefab;
    [SerializeField] private GameObject skillNodeContainer, modifierNodeContainer;
    
    private void Start() {
        InstantiateNodes();
    }

    void InstantiateNodes() {
        foreach (var skillNode in skillInventory) {
            SkillNodeInstance newSkillNode = Instantiate(skillNodeInstancePrefab, skillNodeContainer.transform);
            newSkillNode.Init(skillNode, skillNodeInstancePrefab);
        }

        foreach (var modifierNode in modifierInventory) {
            ModifierNodeInstance newModifierNode = Instantiate(modifierNodeInstancePrefab, modifierNodeContainer.transform);
            newModifierNode.Init(modifierNode);
        }
    }
}