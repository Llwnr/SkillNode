using System;
using UnityEngine;

public class SkillNodeInstance : MonoBehaviour {
    [SerializeField]private SkillNode skillNode;
    public SkillData SkillData => skillNode.Data;
    [SerializeField] private GameObject connectionPort;
    [SerializeField] private SkillNodeUI skillNodeUI;
    
    [SerializeField] private Transform outputSpawnPoint; // Assign a transform in the inspector where children appear
    private SkillNodeInstance _skillNodePrefab;
    
    public void Init(SkillNode skillNode, SkillNodeInstance skillNodePrefab) {
        this.skillNode = Instantiate(skillNode);
        _skillNodePrefab =  skillNodePrefab;
        skillNodeUI.UpdateVisuals();
    }

    //Activated when an object with modifierInstance is dropped onto the connectionPort
    public void AttachModifier(ModifierNode modifierNode) {
        skillNode.ConnectedModifier = modifierNode;
        
        Debug.Log($"Attached modifier: {modifierNode}");
        
        var nextNodes = skillNode.GetNextNodes();
    
        // 2. Clear old outputs if any exist
        foreach(Transform child in outputSpawnPoint) {
            Destroy(child.gameObject);
        }

        // 3. Spawn the new visual nodes
        if (nextNodes != null) {
            foreach (var nodeData in nextNodes) {
                SkillNodeInstance newNode = Instantiate(_skillNodePrefab, outputSpawnPoint);
                newNode.Init(nodeData, _skillNodePrefab);
            }
        }
    }
}