using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
    public static GridManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private float cellSize = 2f;
    [SerializeField] private Vector3 originPosition;
    [SerializeField] private bool showDebugGizmos = true;

    private Vector3 _offset;

    public GridSystem<GridObject> Grid { get; private set; }

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        // Initialize the grid
        Grid = new GridSystem<GridObject>(width, height, cellSize, originPosition, 
            (GridSystem<GridObject> g, Vector2Int coords) => new GridObject(g, coords));
    }

    private void Start() {
        _offset = Vector3.one * cellSize * 0.5f;
    }

    #region Helpers 
    //Shortcuts so you don't have to get the GridSystem itself.
    public Vector3 GetWorldPosition(Vector2Int gridPos) => Grid.GetWorldPosition(gridPos);
    public Vector2Int GetGridPosition(Vector3 worldPos) => Grid.GetGridPosition(worldPos);
    public GridObject GetGridObject(Vector2Int gridPos) => Grid.GetGridObject(gridPos);
    public bool IsValidGridPosition(Vector2Int gridPos) => Grid.IsValidGridPosition(gridPos);
    #endregion
    
    #region Actions, Movement
    public void MoveCharacter(UnitInstance character, Vector2Int targetCoords) {
        if (!IsValidGridPosition(targetCoords)) return;
        
        Vector2Int startCoords = GetGridPosition(character.transform.position);
        //If character wants to move at where it is currently, then do nothing
        if (startCoords == targetCoords) {
            // Debug.LogWarning($"Cel {startCoords} is already occupied by oneself i.e. {character.name}");
            return;
        }

        if (!IsTileInRange(startCoords, targetCoords, 3, false)) return;
        
        GridObject startCell = GetGridObject(startCoords);
        GridObject targetCell = GetGridObject(targetCoords);
        
        if (targetCell.IsOccupied()) {
            // Debug.LogWarning($"Cell {nearestCoordToTarget} is already occupied by {targetCell.CharacterOnTile.name}");
            return;
        }
        
        if (startCell.CharacterOnTile == character) {
            startCell.ClearCharacter();
        }
        targetCell.SetCharacter(character);

        // Update Visuals
        Vector3 worldPos = GetWorldPosition(targetCoords) + _offset;
        character.transform.position = worldPos;
        
        // Debug.Log($"{character.name} moved to {nearestCoordToTarget}");
    }

    public void SnapToGrid(UnitInstance character) {
        Vector2Int pos = GetGridPosition(character.transform.position);
        if (IsValidGridPosition(pos)) {
            GetGridObject(pos).SetCharacter(character);
            character.transform.position = GetWorldPosition(pos) + _offset;
        }
    }
    #endregion
    
    #region Tile queries, finding range etc

    public List<Vector2Int> GetTilesInRange(Vector2Int startPos, int range, bool ignoreOccupants = false) {
        List<Vector2Int> validCells = new List<Vector2Int>();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, int> distances = new Dictionary<Vector2Int, int>();

        queue.Enqueue(startPos);
        distances[startPos] = 0;
        // Optional: Add startPos to validCells if you want the center included
        validCells.Add(startPos); 

        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        while (queue.Count > 0) {
            Vector2Int current = queue.Dequeue();
            int currentDist = distances[current];

            if (currentDist >= range) continue;

            foreach (Vector2Int dir in directions) {
                Vector2Int neighbor = current + dir;

                if (!Grid.IsValidGridPosition(neighbor)) continue;
                if (distances.ContainsKey(neighbor)) continue;

                // Movement Logic: If we can't walk through it, don't add to queue
                bool isOccupied = Grid.GetGridObject(neighbor).IsOccupied();
                if (!ignoreOccupants && isOccupied) continue;

                distances[neighbor] = currentDist + 1;
                queue.Enqueue(neighbor);
                validCells.Add(neighbor);
            }
        }
        return validCells;
    }
    
    public List<Vector2Int> GetTilesInRange(Vector3 worldPos, int range, bool ignoreOccupants = false) {
        return GetTilesInRange(GetGridPosition(worldPos), range, ignoreOccupants);
    }

    public bool IsTileInRange(Vector2Int start, Vector2Int end, int range, bool ignoreOccupants) {
        int pureDist = Mathf.Abs(start.x - end.x) + Mathf.Abs(start.y - end.y);
        if (pureDist > range) return false;

        return GetTilesInRange(start, range, ignoreOccupants).Contains(end);
    }
    #endregion

    // Built-in Unity method for drawing debug visuals in Scene view
    private void OnDrawGizmos() {
        if (!showDebugGizmos || Grid == null) return;

        for (int x = 0; x < Grid.Width; x++) {
            for (int y = 0; y < Grid.Height; y++) {
                // Draw Cell Borders
                Vector3 worldPos = Grid.GetWorldPosition(x, y);
                Gizmos.color = Color.white;
                Gizmos.DrawLine(worldPos, worldPos + Vector3.up * Grid.CellSize);
                Gizmos.DrawLine(worldPos, worldPos + Vector3.right * Grid.CellSize);
                
                // Optional: Draw Center
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(worldPos + new Vector3(Grid.CellSize, Grid.CellSize) * 0.5f, 0.1f);
            }
        }
        
        // Draw outer borders
        Gizmos.color = Color.white;
        Gizmos.DrawLine(Grid.GetWorldPosition(0, Grid.Height), Grid.GetWorldPosition(Grid.Width, Grid.Height));
        Gizmos.DrawLine(Grid.GetWorldPosition(Grid.Width, Grid.Height), Grid.GetWorldPosition(Grid.Width, 0));
    }
}