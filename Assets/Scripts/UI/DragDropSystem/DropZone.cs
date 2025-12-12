using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DropZone : MonoBehaviour, IDropHandler {
    [SerializeField] private int capacity = 1;

    public abstract bool Condition(PointerEventData eventData);
    public abstract void Action(PointerEventData eventData);

    public void OnDrop(PointerEventData eventData) {
        if (transform.childCount >= capacity) return;
        if (Condition(eventData)) {
            Action(eventData);
        }
    }

    public virtual void OnPickup(PointerEventData eventData) {
        
    }
}