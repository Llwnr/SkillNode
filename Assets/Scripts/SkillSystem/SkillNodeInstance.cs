using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SkillNodeInstance : MonoBehaviour {
    [SerializeField]private SkillNode sourceSkillNode;
    [SerializeField] private GameObject connectionPort;
    
    //NODE LOGIC RELATED STUFF
    private ModifierNode _connectedModifier;
    private List<SkillNodeInstance> _childrenNodes = new  List<SkillNodeInstance>();
    public SkillData RuntimeData { get; private set; }
    
    //UI STUFF
    [SerializeField] private SkillNodeUI skillNodeUI;
    
    [SerializeField] private Transform outputSpawnPoint; // Assign a transform in the inspector where children appear
    private SkillNodeInstance _skillNodePrefab;
    
    public void Init(SkillNode skillNode, SkillData latestData, SkillNodeInstance skillNodePrefab) {
        sourceSkillNode = skillNode;
        _skillNodePrefab =  skillNodePrefab;
        RuntimeData = latestData;
        skillNodeUI.UpdateVisuals();
    }

    //Activated when an object with modifierInstance is dropped onto the connectionPort
    public void AttachModifier(ModifierNode modifier) {
        _connectedModifier = modifier;
        List<SkillData> nextNodesDatas = modifier.CalculateOutputs(RuntimeData);

        foreach (Transform child in outputSpawnPoint) {
            Destroy(child.gameObject);
        }

        //Reset before reinitializing
        _childrenNodes.Clear();

        foreach (var newSkillData in nextNodesDatas) {
            SkillNodeInstance newInstance = Instantiate(_skillNodePrefab, outputSpawnPoint);
            newInstance.Init(sourceSkillNode, newSkillData, _skillNodePrefab);
            
            _childrenNodes.Add(newInstance);
        }
    }
    
    //Recursing the node logic
    public List<SkillNodeInstance> GetNextNodes() {
        return _childrenNodes;
    }

    public List<SkillNodeInstance> GetLeaves() {
        if (_childrenNodes.Count == 0) {
            return new List<SkillNodeInstance> { this };
        }

        List<SkillNodeInstance> leaves = new List<SkillNodeInstance>();
        foreach (var childNode in _childrenNodes) {
            leaves.AddRange(childNode.GetLeaves());
        }

        return leaves;
    }

    public void Activate() {
        if (_childrenNodes.Count == 0) {
            sourceSkillNode.Execute(RuntimeData);
            return;
        }
        foreach (var leafNode in GetLeaves()) {
            leafNode.Activate();
        }
    }
}