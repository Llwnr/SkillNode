using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldSpaceDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public GameObject runtimeObject;
    private Vector3 _startPosition;
    private Transform _startParent;

    private Collider2D _collider2D;

    public Action<PointerEventData> OnDragStart;

    private void Awake() {
        _collider2D = GetComponent<Collider2D>();
    }

    public void OnBeginDrag(PointerEventData eventData){
        _startPosition = transform.position;
        _startParent = transform.parent;
        
        transform.SetParent(transform.root);

        _collider2D.enabled = false;

        OnDragStart?.Invoke(eventData);
        OnDragStart = null;
    }

    public void OnDrag(PointerEventData eventData) {
        Vector3 mouseToWorldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        mouseToWorldPos.z = transform.position.z;
        transform.position = mouseToWorldPos;
    }

    public void OnEndDrag(PointerEventData eventData){
        _collider2D.enabled = true;
        if (transform.parent == transform.root) {
            Reset(eventData);
        }
    }

    private void Reset(PointerEventData eventData){
        transform.position = _startPosition;
        _startParent.GetComponent<DropHandler>()?.OnDrop(eventData);
        transform.SetParent(_startParent);
    }
}