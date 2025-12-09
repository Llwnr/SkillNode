using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class SkillNodeInstance : MonoBehaviour {
    [SerializeField]private SkillNode coreSkillNode;
    public SkillNode CoreSkillNode => coreSkillNode;

    [SerializeField] private int numOfModSlots = 3;
    private List<ModifierDropZone> modifierSlots = new List<ModifierDropZone>();

    [SerializeField] private ModifierDropZone modDropZonePrefab;

    [SerializeField] private Transform dropZoneContainer;
    //UI STUFF
    [SerializeField] private SkillNodeUI skillNodeUI;
    
    public void Init(SkillNode skillNode) {
        coreSkillNode = skillNode;
        for (int i = 0; i < numOfModSlots; i++) {
            ModifierDropZone dropZone = Instantiate(modDropZonePrefab, dropZoneContainer);
            modifierSlots.Add(dropZone);
        }
        
        skillNodeUI.UpdateVisuals();
    }

    public List<ModifierNode> GetLinearModifierList() {
        List<ModifierNode> foundModifiers = new List<ModifierNode>();

        foreach (var slot in modifierSlots) {
            ModifierNode mod = slot.GetAttachedModifier();
            if (mod != null) {
                foundModifiers.Add(mod);
            }
        }

        return foundModifiers;
    }
}