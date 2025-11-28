using System.Collections.Generic;
using UnityEngine;

public class ModifierNodeInstance : MonoBehaviour {
    private List<SkillNodeInstance> _outputNodes = new List<SkillNodeInstance>();
    [SerializeField] private ModifierNode modifierNode;
    public ModifierNode ModifierNode => modifierNode;
    [SerializeField] private ModifierItemUI modifierItemUI;
    
    public void Init(ModifierNode modifierNode) {
        this.modifierNode = Instantiate(modifierNode);
        modifierItemUI.UpdateVisuals();
    }
}