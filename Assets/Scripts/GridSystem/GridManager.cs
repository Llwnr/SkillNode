using UnityEngine;

public class GridManager : MonoBehaviour {
    public static GridManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private float cellSize = 2f;
    [SerializeField] private Vector3 originPosition;
    [SerializeField] private bool showDebugGizmos = true;

    public GridSystem<GridObject> Grid { get; private set; }

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        // Initialize the grid
        Grid = new GridSystem<GridObject>(width, height, cellSize, originPosition, 
            (GridSystem<GridObject> g, Vector2Int coords) => new GridObject(g, coords));
    }

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