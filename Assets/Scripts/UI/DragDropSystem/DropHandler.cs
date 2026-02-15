using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DropHandler : MonoBehaviour, IDropHandler {
    public abstract bool DropCondition(PointerEventData eventData);
    public abstract void OnDropSuccess(PointerEventData eventData);

    public void OnDrop(PointerEventData eventData) {
        if (DropCondition(eventData)) {
            ListenForPickup(eventData);
            OnDropSuccess(eventData);
        }
    }

    void ListenForPickup(PointerEventData eventData) {
        if (eventData.pointerDrag.TryGetComponent<Draggable>(out var draggable)) {
            draggable.OnDragStart += OnPickedUp;
        }

        if (eventData.pointerDrag.TryGetComponent<WorldSpaceDraggable>(out var worldDraggable)) {
            worldDraggable.OnDragStart += OnPickedUp;
        }
    }

    public abstract void OnPickedUp(PointerEventData eventData);
}