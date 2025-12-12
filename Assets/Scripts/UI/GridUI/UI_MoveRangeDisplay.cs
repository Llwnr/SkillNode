using System.Collections.Generic;
using UnityEngine;

public class UI_MoveRangeDisplay : MonoBehaviour {
    [SerializeField] private Transform baseTilePrefab;
    [SerializeField] private Character myCharacter;
    private List<GameObject> _createdTiles = new List<GameObject>();

    public void Update() {
        DestroyTiles();
        DisplayTiles(myCharacter.transform.position, myCharacter.moveRange);
    }
    
    void DisplayTiles(Vector3 position, int range) {
        // Use GridManager directly. true = ignore occupants (for skill targeting range)
        List<Vector2Int> areasOfEffect = GridManager.Instance.GetTilesInRange(position, range, ignoreOccupants: false);

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