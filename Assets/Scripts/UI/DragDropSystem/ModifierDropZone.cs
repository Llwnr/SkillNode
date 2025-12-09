using UnityEngine;
using UnityEngine.EventSystems;

public class ModifierDropZone : DropZone {
    
    public ModifierNode GetAttachedModifier() {
        if (transform.childCount > 0) {
            var instance = transform.GetChild(0).GetComponent<ModifierNodeInstance>();
            if (instance != null) {
                return instance.ModifierNode;
            }
        }
        return null;
    }
    
    public override bool Condition(PointerEventData eventData) {
        return (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<ModifierItemUI>() != null);
    }

    public override void Action(PointerEventData eventData) {
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        ModifierNodeInstance instance = eventData.pointerDrag.GetComponent<ModifierNodeInstance>();
        
        if (instance != null && draggable != null) {
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
        }
    }
}