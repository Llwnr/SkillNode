using UnityEngine;
using UnityEngine.EventSystems;

public class ModifierDropZone : DropZone{
    public override bool Condition(PointerEventData eventData) {
        return (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<ModifierItemUI>() != null);
    }

    public override void Action(PointerEventData eventData) {
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        ModifierNodeInstance instance = eventData.pointerDrag.GetComponent<ModifierNodeInstance>();
        
        if (instance != null && draggable != null) {
            eventData.pointerDrag.transform.SetParent(transform);
            instance.GetComponentInParent<SkillNodeInstance>()?.AttachModifier(instance);
        }
    }
}