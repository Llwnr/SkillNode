using System.Collections.Generic;
using UnityEngine;

public class UI_SkillAreaDisplay : MonoBehaviour {
    [SerializeField] private Transform baseTilePrefab;
    private List<GameObject> _createdTiles = new List<GameObject>();

    public static UI_SkillAreaDisplay Instance;
    public bool TilesDisplayable = false;
    public int RangeCache = 0;

    private void Awake() {
        if (Instance == null) Instance = this;
    }

    public void Update() {
        if (TilesDisplayable) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DisplayTiles(mouseWorldPos, RangeCache);
        }
        else {
            DestroyTiles();
        }
    }

    void DisplayTiles(Vector3 position, int range) {
        // Use GridManager directly. true = ignore occupants (for skill targeting range)
        List<Vector2Int> areasOfEffect = GridManager.Instance.GetTilesInRange(position, range, ignoreOccupants: true);

        DestroyTiles();

        // Create center tile
        CreateTile(GridManager.Instance.GetGridPosition(position));
        
        // Create range tiles
        foreach (var areaAxes in areasOfEffect) {
            CreateTile(areaAxes);
        }
    }

    private void DestroyTiles() {
        foreach (var tile in _createdTiles) Destroy(tile);
        _createdTiles.Clear();
    }

    private void CreateTile(Vector2Int areaAxes) {
        // Use the wrapper method
        float cellSize = GridManager.Instance.Grid.CellSize; 
        Vector3 pos = GridManager.Instance.GetWorldPosition(areaAxes) + (Vector3.one * cellSize * 0.5f);

        Transform tile = Instantiate(baseTilePrefab, pos, Quaternion.identity, transform);
        tile.localScale *= cellSize;
        _createdTiles.Add(tile.gameObject);
    }
}