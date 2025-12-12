using System;
using UnityEngine;

public class GridInputHandler : MonoBehaviour {
    [SerializeField] private Character _activeCharacter;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int targetGridPos = GridManager.Instance.GetGridPosition(mouseWorldPos);
            
            GridManager.Instance.MoveCharacter(_activeCharacter, targetGridPos);
        }
    }
}