using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private DragContext _dragContext;
    
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Vector3 _startPosition;
    private Transform _startParent;

    public Action<PointerEventData> OnDragStart;
    
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
        
        GameObject draggedObject = eventData.pointerDrag;
        _dragContext = new DragContext(draggedObject.GetComponent<Sprite>(), draggedObject);
        
        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
        transform.SetParent(transform.root);

        OnDragStart?.Invoke(eventData);
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
    }

    private void Reset(PointerEventData eventData) {
        transform.position = _startPosition;
        transform.SetParent(_startParent);
    }
}