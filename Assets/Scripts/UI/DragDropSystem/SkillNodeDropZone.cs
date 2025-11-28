using UnityEngine.EventSystems;

public class SkillNodeDropZone : DropZone{
    public override bool Condition(PointerEventData eventData) {
        return (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<SkillNodeUI>() != null);
    }

    public override void Action(PointerEventData eventData) {
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggable != null) {
            eventData.pointerDrag.transform.SetParent(transform);
        }
    }
}