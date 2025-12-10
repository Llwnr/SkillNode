using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

public class GridSystem<TGridObject>{
    private int _width, _height;
    private float _cellSize;
    private Vector3 _originPos;

    private TGridObject[,] _gridArray;
    private int TotalWidth => _gridArray.GetLength(0);
    private int TotalHeight => _gridArray.GetLength(1);

    public GridSystem(int width, int height, float cellSize, Vector3 originPos, Func<TGridObject> createGridObject) {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPos = originPos;

        _gridArray = new TGridObject[width, height];

        for (int x = 0; x < TotalWidth; x++) {
            for (int y = 0; y < TotalHeight; y++) {
                _gridArray[x, y] = createGridObject();
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x,y+1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x+1,y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0,height), GetWorldPosition(width,height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width,height), GetWorldPosition(width,0), Color.white, 100f);
    }

    public Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * _cellSize + _originPos;
    }

    public void GetGridPosition(Vector3 worldPos, out int x, out int y) {
        Vector3 relativePos = worldPos - _originPos;
        x = Mathf.FloorToInt(relativePos.x / _cellSize);
        y = Mathf.FloorToInt(relativePos.y / _cellSize);
    }

    bool IsInGridRange(int x, int y) {
        return x >= 0 && x < _width && y >= 0 && y < _height;
    }

    public void SetValue(int x, int y, TGridObject value) {
        if (!IsInGridRange(x,y)) return;
        _gridArray[x, y] = value;
    }

    public void SetValue(Vector3 worldPosition, TGridObject value) {
        int x, y;
        GetGridPosition(worldPosition, out x, out y);
        
        SetValue(x, y, value);
    }
    
    public TGridObject GetValue(int x, int y) {
        if (!IsInGridRange(x, y)) return default;
        return _gridArray[x, y];
    }

    public TGridObject GetValue(Vector3 worldPos) {
        int x, y;
        GetGridPosition(worldPos, out x, out y);

        return GetValue(x,y);
    }

    public List<Vector2Int> GetRange(Vector3 worldPos, int range) {
        int x, y;
        GetGridPosition(worldPos, out x, out y);
        Vector2Int gridStartPos = new Vector2Int(x, y);
        return GetRange(gridStartPos, range);
    }

    public List<Vector2Int> GetRange(Vector2Int startPos, int range) {
        List<Vector2Int> inRangeCells = new List<Vector2Int>();
        Queue<PathNode>  queue = new Queue<PathNode>();
        HashSet<Vector2Int> visitedCells = new HashSet<Vector2Int>();
        
        queue.Enqueue(new PathNode(startPos.x, startPos.y, 0));
        visitedCells.Add(startPos);

        while (queue.Count > 0) {
            PathNode currentCell = queue.Dequeue();

            if (currentCell.Distance <= range) {
                inRangeCells.Add(currentCell.GetCoordinates());

                List<Vector2Int> neighbours = GetNeighbours(currentCell.X, currentCell.Y);
                foreach (var neighbour in neighbours) {
                    if (visitedCells.Contains(neighbour)) continue;
                    queue.Enqueue(new PathNode(neighbour.x, neighbour.y, currentCell.Distance + 1));
                    visitedCells.Add(neighbour);
                }
            }

        }
        return inRangeCells;
    }

    public List<Vector2Int> GetNeighbours(int x, int y) {
        List<Vector2Int> validNeighbours = new List<Vector2Int>();

        int[] dx = {-1, 1, 0, 0};
        int[] dy = {0, 0, 1, -1};

        for (int i = 0; i < dx.Length; i++) {
            int newX = x + dx[i];
            int newY = y + dy[i];
            if (IsInGridRange(newX, newY)) {
                validNeighbours.Add(new Vector2Int(newX, newY));
            }
        }

        return validNeighbours;
    }
}

public struct PathNode {
    public int X,Y;
    public int Distance;

    public Vector2Int GetCoordinates() {
        return new Vector2Int(X, Y);
    }

    public PathNode(int x, int y, int distance) {
        X = x;
        Y = y;
        Distance = distance;
    }
}