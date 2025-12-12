using TMPro;
using UnityEngine;

public class ModifierItemUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI nameBox;
    [SerializeField] private ModifierNodeInstance associatedModifierNode;

    public void UpdateVisuals() {
        ModifierNode modNode = associatedModifierNode.ModifierNode;

        nameBox.text = modNode.name;
    }
}