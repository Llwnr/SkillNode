using UnityEngine;
using System;
using System.Collections.Generic;

public class GridSystem<TGridObject> {
    public int Width { get; private set; }
    public int Height { get; private set; }
    public float CellSize { get; private set; }
    
    private Vector3 _originPos;
    private TGridObject[,] _gridArray;

    // Constructor
    public GridSystem(int width, int height, float cellSize, Vector3 originPos, Func<GridSystem<TGridObject>, Vector2Int, TGridObject> createGridObject) {
        Width = width;
        Height = height;
        CellSize = cellSize;
        _originPos = originPos;

        _gridArray = new TGridObject[width, height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Vector2Int coords = new Vector2Int(x, y);
                _gridArray[x, y] = createGridObject(this, coords);
            }
        }
    }

    public Vector3 GetWorldPosition(Vector2Int gridPos) {
        return new Vector3(gridPos.x, gridPos.y) * CellSize + _originPos;
    }
    
    // Overload for individual ints if needed
    public Vector3 GetWorldPosition(int x, int y) => GetWorldPosition(new Vector2Int(x, y));

    public Vector2Int GetGridPosition(Vector3 worldPos) {
        Vector3 relativePos = worldPos - _originPos;
        int x = Mathf.FloorToInt(relativePos.x / CellSize);
        int y = Mathf.FloorToInt(relativePos.y / CellSize);
        return new Vector2Int(x, y);
    }

    public TGridObject GetGridObject(Vector2Int gridPos) {
        if (!IsValidGridPosition(gridPos)) return default;
        return _gridArray[gridPos.x, gridPos.y];
    }

    public TGridObject GetGridObject(Vector3 worldPos) {
        return GetGridObject(GetGridPosition(worldPos));
    }

    public bool IsValidGridPosition(Vector2Int gridPos) {
        return gridPos.x >= 0 && gridPos.y >= 0 && gridPos.x < Width && gridPos.y < Height;
    }
}