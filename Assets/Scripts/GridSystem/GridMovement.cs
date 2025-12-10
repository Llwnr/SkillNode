using System.Collections.Generic;
using UnityEngine;

public class GridMovement {
    private GridSystem<GridObject> _grid;

    public GridMovement(GridSystem<GridObject> grid) {
        _grid = grid;
    }

    // 1. Move Character to a specific coordinate
    public void MoveCharacter(Character character, Vector2Int targetCoords) {
        if (!_grid.IsValidGridPosition(targetCoords)) return;

        // A. Find where the character currently is
        Vector2Int startCoords = _grid.GetGridPosition(character.transform.position);
        GridObject startCell = _grid.GetGridObject(startCoords);
        Vector2Int nearestCoordToTarget 
            = GetNearestAxesToTarget(startCoords, targetCoords, character.moveRange);
        GridObject targetCell = _grid.GetGridObject(nearestCoordToTarget);

        // B. Logic Checks (Is target empty?)
        if (targetCell.IsOccupied()) {
            Debug.LogWarning($"Cell {nearestCoordToTarget} is already occupied by {targetCell.CharacterOnTile.name}");
            return;
        }

        // C. Update Data
        if (startCell.CharacterOnTile == character) {
            startCell.ClearCharacter();
        }
        targetCell.SetCharacter(character);

        // D. Update Visuals
        Vector3 worldPos = _grid.GetWorldPosition(nearestCoordToTarget) + (Vector3.one * _grid.CellSize * 0.5f);
        character.transform.position = worldPos;
        
        Debug.Log($"{character.name} moved to {nearestCoordToTarget}");
    }

    Vector2Int GetNearestAxesToTarget(Vector2Int startPos, Vector2Int targetPos, int moveRange) {
        List<Vector2Int> axesInRange = GetValidMovesInRange(startPos, moveRange);
        float distance = Mathf.Infinity;
        Vector2Int nearestPossibleAxesToTarget = new Vector2Int(0,0);
        foreach (Vector2Int axes in axesInRange) {
            float tempDist = Vector2.Distance(axes, targetPos);
            if (tempDist < distance) {
                distance = tempDist;
                nearestPossibleAxesToTarget = axes;
            }
        }

        return nearestPossibleAxesToTarget;
    }

    // 2. BFS Algorithm to find valid tiles in range
    public List<Vector2Int> GetValidMovesInRange(Vector2Int startPos, int range, bool ignoreOccupied = false) {
        List<Vector2Int> validCells = new List<Vector2Int>();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, int> distances = new Dictionary<Vector2Int, int>();

        queue.Enqueue(startPos);
        distances[startPos] = 0;

        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        while (queue.Count > 0) {
            Vector2Int current = queue.Dequeue();
            int currentDist = distances[current];

            if (currentDist >= range) continue;

            foreach (Vector2Int dir in directions) {
                Vector2Int neighbor = current + dir;
                bool isOccupied = _grid.GetGridObject(neighbor).IsOccupied() && !ignoreOccupied;
                // Check bounds and if we already visited
                if (_grid.IsValidGridPosition(neighbor) && !isOccupied && !distances.ContainsKey(neighbor)) {
                    // Check if walkable (Optional: Add logic here to check if tile is blocked)
                    
                    distances[neighbor] = currentDist + 1;
                    queue.Enqueue(neighbor);
                    validCells.Add(neighbor);
                }
            }
        }
        return validCells;
    }

    public bool IsInRange(Vector2Int startPos, Vector2Int targetPos, int range) {
        return GetValidMovesInRange(startPos, range, true).Contains(targetPos);
    }
}