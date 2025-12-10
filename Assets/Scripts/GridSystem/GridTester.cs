using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridTester : MonoBehaviour {
    [SerializeField] private int width, height;
    [SerializeField] private float cellSize;
    [SerializeField] private Vector3 originPos;

    [SerializeField] private int range;
    [SerializeField] private Transform square;
    
    private GridSystem<int> _grid;
    private void Start() {
        _grid = new GridSystem<int>(width, height, cellSize, originPos, () => 0);
    }

    void DisplayRangeVisual() {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        List<Vector2Int> ranges = _grid.GetRange(worldPos, range);
        foreach (var validRange in ranges) {
            Vector3 pos = _grid.GetWorldPosition(validRange.x, validRange.y);
            Transform rangeBlock = Instantiate(square, pos + Vector3.one * cellSize * 0.5f, Quaternion.identity);
            rangeBlock.localScale *= cellSize;
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            DisplayRangeVisual();
        }
    }
}