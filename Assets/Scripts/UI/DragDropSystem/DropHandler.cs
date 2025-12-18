using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DropHandler : MonoBehaviour, IDropHandler {
    [SerializeField] private int capacity = 1;

    public abstract bool Condition(PointerEventData eventData);
    public abstract void Action(PointerEventData eventData);

    public void OnDrop(PointerEventData eventData) {
        if (transform.childCount >= capacity) {
            Debug.Log($"{gameObject.name} + {GetType()} is at max capacity");
            return;
        }
        if (Condition(eventData)) {
            Action(eventData);
        }
    }
}