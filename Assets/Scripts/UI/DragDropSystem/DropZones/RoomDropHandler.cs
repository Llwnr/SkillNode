using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Room))]
public class RoomDropHandler : DropHandler{
    private Room _room;

    private void Start() {
        _room = GetComponent<Room>();
    }

    public override bool DropCondition(PointerEventData eventData) {
        return (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<UnitInstance>() != null);
    }

    public override void OnDropSuccess(PointerEventData eventData) {
        UnitInstance unit = eventData.pointerDrag.GetComponent<UnitInstance>();
        if (unit != null && _room.TryPlaceUnit(unit)) {
            unit.transform.SetParent(transform);
        }
    }

    public override void OnPickedUp(PointerEventData eventData) {
        UnitInstance unit = eventData.pointerDrag.GetComponent<UnitInstance>();
        if (unit != null) {
            _room.OnUnitRemoved(unit);
        }
    }
}