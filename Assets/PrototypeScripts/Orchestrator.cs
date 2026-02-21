using System;
using UnityEngine;

public class Orchestrator : MonoBehaviour
{
    public Vector2Int GridSize;

    private GridSystem<int> _grid; 
    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        _grid = new GridSystem<int>(GridSize.x, GridSize.y, 1, Vector3.zero, (grid, i) => 0);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < GridSize.x; i++)
        {
            for (int j = 0; j < GridSize.y; j++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(GetVector2(i, j), GetVector2(i + 1, j));
                Gizmos.DrawLine(GetVector2(i, j), GetVector2(i, j + 1));
                Gizmos.DrawLine(GetVector2(i + 1, j), GetVector2(i + 1, j + 1));
                Gizmos.DrawLine(GetVector2(i, j + 1), GetVector2(i + 1, j + 1));
            }
        }

        Vector2 GetVector2(int x, int y)
        {
            float offset = -0.5f;//(Should actually be CellSize*0.5f;
            return new Vector2(x+offset, y+offset);
        }
    }
}