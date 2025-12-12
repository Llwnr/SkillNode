using UnityEngine;
using UnityEngine.EventSystems;

public class NodeModificationDropZone : DropZone {
    private SkillNodeInstance _holdingNode;
    
    public override bool Condition(PointerEventData eventData) {
        return (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<SkillNodeInstance>() != null);
    }

    public override void Action(PointerEventData eventData) {
        _holdingNode = eventData.pointerDrag.GetComponent<SkillNodeInstance>();
        
        if (_holdingNode == null) return;
        _holdingNode.ToggleModSockets(true);
        
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggable != null) {
            eventData.pointerDrag.transform.SetParent(transform);
        }
    }

    public override void OnPickup(PointerEventData eventData) {
        if(_holdingNode == null) return;
        _holdingNode.ToggleModSockets(false);
    }
}