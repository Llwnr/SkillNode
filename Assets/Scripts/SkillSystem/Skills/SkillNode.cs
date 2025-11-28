using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillNode", menuName = "SkillNode/")]
public abstract class SkillNode : ScriptableObject {
    [SerializeField]
    private SkillData _data;
    public SkillData Data => _data;
    public ModifierNode ConnectedModifier; // The single connection
    
    public void Init(SkillData data) {
        _data =  data;
    }
    
    public abstract void Activate();

    // RECURSIVE FUNCTION: This is the magic that finds the leaves.
    public List<SkillNode> GetLeaves() {
        // Base Case: If there is no modifier, THIS is a leaf node.
        if (ConnectedModifier == null) {
            return new List<SkillNode> { this };
        }

        List<SkillNode> leaves = new List<SkillNode>();

        //Modifier calculates the next step
        List<SkillNode> outputs = ConnectedModifier.CalculateOutputs(this);

        // Recursive Step
        foreach (var outputNode in outputs) {
            leaves.AddRange(outputNode.GetLeaves());
        }

        return leaves;
    }

    [CanBeNull]
    public List<SkillNode>  GetNextNodes() {
        if (ConnectedModifier == null) {
            return null;
        }
        
        return ConnectedModifier.CalculateOutputs(this);
    }
}