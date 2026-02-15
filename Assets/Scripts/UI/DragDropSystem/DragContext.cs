using UnityEngine;

public class DragContext {
    public Sprite Icon;
    public GameObject ReferencedObject;

    public DragContext(Sprite icon, GameObject referencedObject) {
        Icon = icon;
        ReferencedObject = referencedObject;
    }
}