using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SkillNodeInstance : MonoBehaviour {
    [SerializeField]private SkillNode sourceSkillNode;
    [SerializeField] private GameObject connectionPort;
    private Transform _modifiersOrigContainer;
    
    //NODE LOGIC RELATED STUFF
    [SerializeField]private ModifierNodeInstance _connectedModifierInstance;
    private List<SkillNodeInstance> _childrenNodeInstances = new  List<SkillNodeInstance>();
    public SkillData RuntimeData { get; private set; }
    
    //UI STUFF
    [SerializeField] private SkillNodeUI skillNodeUI;
    
    // [SerializeField] private Transform outputSpawnPoint; // Assign a transform in the inspector where children appear
    private SkillNodeInstance _skillNodePrefab;
    
    public void Init(SkillNode skillNode, SkillData latestData, SkillNodeInstance skillNodePrefab, Transform modifierContainer) {
        sourceSkillNode = skillNode;
        _skillNodePrefab =  skillNodePrefab;
        RuntimeData = latestData;
        _modifiersOrigContainer = modifierContainer;
        skillNodeUI.UpdateVisuals();
    }

    //Activated when an object with modifierInstance is dropped onto the connectionPort
    public void AttachModifier(ModifierNodeInstance modifier) {
        //Reset before reinitializing
        ClearChildNodes();
        
        _connectedModifierInstance = modifier;
        _connectedModifierInstance.GetComponent<Draggable>().OnDragStart += ClearChildNodes;
        List<SkillData> nextNodesDatas = modifier.ModifierNode.CalculateOutputs(RuntimeData);

        for (int i = 0; i < nextNodesDatas.Count; i++) {
            SkillNodeInstance newInstance = Instantiate(_skillNodePrefab, transform.parent);
            newInstance.Init(sourceSkillNode, nextNodesDatas[i], _skillNodePrefab, _modifiersOrigContainer);
            newInstance.transform.position = transform.position + new Vector3(200, (i+1)*100, 0);
            
            _childrenNodeInstances.Add(newInstance);
        }
    }

    void ClearChildNodes() {
        foreach (var nodeInstance in _childrenNodeInstances) {
            Destroy(nodeInstance.gameObject);
        }
        _childrenNodeInstances.Clear();

        DetatchModifier();
    }

    void DetatchModifier() {
        if (_connectedModifierInstance) {
            _connectedModifierInstance.GetComponent<Draggable>().OnDragStart -= ClearChildNodes;
            _connectedModifierInstance.GetComponent<Draggable>().SetStartParent(_modifiersOrigContainer);
            _connectedModifierInstance = null;
        }
    }

    private void OnDestroy() {
        DetatchModifier();
    }


    //Gets the proper sequence to remove nodes, starting from the leaf nodes and moving upto root node
    public List<SkillNodeInstance> GetAllChildSequence() {
        List<SkillNodeInstance> sortedList = new List<SkillNodeInstance>();
        List<SkillNodeInstance> visited = new List<SkillNodeInstance>();
        
        PostOrderTraversal(this, sortedList, visited);
        return sortedList;
        
        void PostOrderTraversal(SkillNodeInstance node, List<SkillNodeInstance> sortedList, List<SkillNodeInstance> visited) {
            //Node is already visited and done
            if (visited.Contains(node)) {
                return;
            }
        
            visited.Add(node);

            foreach (var child in node.GetNextNodes()) {
                PostOrderTraversal(child, sortedList, visited);
            }
        
            sortedList.Add(node);
        } 
    }

    
    
    //Recursing the node logic
    public List<SkillNodeInstance> GetNextNodes() {
        return _childrenNodeInstances;
    }

    public List<SkillNodeInstance> GetLeaves() {
        if (_childrenNodeInstances.Count == 0) {
            return new List<SkillNodeInstance> { this };
        }

        List<SkillNodeInstance> leaves = new List<SkillNodeInstance>();
        foreach (var childNode in _childrenNodeInstances) {
            leaves.AddRange(childNode.GetLeaves());
        }

        return leaves;
    }

    public void Activate() {
        if (_childrenNodeInstances.Count == 0) {
            sourceSkillNode.Execute(RuntimeData);
            return;
        }
        foreach (var leafNode in GetLeaves()) {
            leafNode.Activate();
        }
    }
}