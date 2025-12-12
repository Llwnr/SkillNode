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
            dropZone.gameObject.SetActive(false);
            modifierSlots.Add(dropZone);
        }

        CompileAndReturnSkill();
    }

    public List<ModifierNode> GetModifiers() {
        List<ModifierNode> foundModifiers = new List<ModifierNode>();

        foreach (var slot in modifierSlots) {
            ModifierNode mod = slot.GetAttachedModifier();
            if (mod != null) {
                foundModifiers.Add(mod);
            }
        }

        return foundModifiers;
    }

    public void ToggleModSockets(bool value) {
        foreach (var modSlot in modifierSlots) {
            modSlot.gameObject.SetActive(value);
        }
    }
    
    public ActionSkill CompileAndReturnSkill() {
        List<SkillData> currentBatch = new List<SkillData>();
        currentBatch.Add(coreSkillNode.BaseData.Clone());

        foreach (var mod in GetModifiers()) {
            List<SkillData> nextBatch = new List<SkillData>();

            foreach (var data in currentBatch) {
                nextBatch.AddRange(mod.Process(data));
            }

            currentBatch = nextBatch;
        }

        ActionSkill compiledSkill = new ActionSkill(coreSkillNode, currentBatch);
        skillNodeUI?.UpdateVisuals(compiledSkill);
        return compiledSkill;
    }
}