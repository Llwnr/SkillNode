using UnityEngine;

public class GridObject {
    private GridSystem<GridObject> _gridSystem;
    private Vector2Int _gridCoords;
    
    public Character CharacterOnTile { get; private set; }

    public GridObject(GridSystem<GridObject> gridSystem, Vector2Int coords) {
        _gridSystem = gridSystem;
        _gridCoords = coords;
    }

    public void SetCharacter(Character character) {
        CharacterOnTile = character;
    }

    public void ClearCharacter() {
        CharacterOnTile = null;
    }

    public bool IsOccupied() {
        return CharacterOnTile != null;
    }

    public override string ToString() {
        return $"[{_gridCoords.x}, {_gridCoords.y}] \n {(CharacterOnTile ? CharacterOnTile.name : "Empty")}";
    }
}