using UnityEngine;
using UnityEngine.EventSystems;

public class ModifierDropZone : DropZone{
    public override bool Condition(PointerEventData eventData) {
        return (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<ModifierItemUI>() != null);
    }

    public override void Action(PointerEventData eventData) {
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggable != null) {
            eventData.pointerDrag.transform.SetParent(transform);
        }
        
        ModifierNodeInstance instance = eventData.pointerDrag.GetComponent<ModifierNodeInstance>();
        if (instance != null) {
            instance.GetComponentInParent<SkillNodeInstance>()?.AttachModifier(instance.ModifierNode);
        }
    }
}