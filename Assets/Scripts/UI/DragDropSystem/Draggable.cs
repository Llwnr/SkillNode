using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Vector3 _startPosition;
    private Transform _startParent;

    private DropZone _droppedZone;

    public Action OnDragStart;

    public void SetStartParent(Transform startParent) {
        _startParent = startParent;
    }
    
    private void Awake(){
        _rectTransform =  GetComponent<RectTransform>();
        _canvasGroup =  GetComponent<CanvasGroup>();
        if (_canvasGroup == null){
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData){
        _startPosition = _rectTransform.position;
        _startParent = transform.parent;
        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
        transform.SetParent(transform.root);

        OnDragStart?.Invoke();
        _droppedZone?.OnPickup(eventData);
    }

    public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        if (transform.parent == transform.root) {
            Reset(eventData);
        }
        else {
            transform.parent.TryGetComponent(out _droppedZone);
        }
    }

    private void Reset(PointerEventData eventData) {
        transform.position = _startPosition;
        transform.SetParent(_startParent);
        _droppedZone?.OnDrop(eventData);
    }
}