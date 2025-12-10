using UnityEngine;

public class GridInputHandler : MonoBehaviour {
    [SerializeField] private Character _activeCharacter;
    private GridMovement _gridMovement;

    private void Start() {
        // Wait for GridManager to init
        if (GridManager.Instance != null) {
            _gridMovement = new GridMovement(GridManager.Instance.Grid);
            
            // Snap character to nearest grid cell on start
            Vector2Int startPos = GridManager.Instance.Grid.GetGridPosition(_activeCharacter.transform.position);
            _gridMovement.MoveCharacter(_activeCharacter, startPos);
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int targetGridPos = GridManager.Instance.Grid.GetGridPosition(mouseWorldPos);

            // Move
            _gridMovement.MoveCharacter(_activeCharacter, targetGridPos);
        }
    }
}