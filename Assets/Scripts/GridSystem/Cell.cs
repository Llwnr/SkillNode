using UnityEngine;

public class Cell {
    private GridSystem<Cell> _gridSystem;
    private Vector2Int _gridCoords;
    
    public UnitInstance CharacterOnTile { get; private set; }

    public Cell(GridSystem<Cell> gridSystem, Vector2Int coords) {
        _gridSystem = gridSystem;
        _gridCoords = coords;
    }

    public void SetCharacter(UnitInstance character) {
        CharacterOnTile = character;
    }

    public void ClearCharacter() {
        CharacterOnTile = default;
    }

    public bool IsOccupied() {
        return CharacterOnTile != null;
    }

    public override string ToString() {
        return $"[{_gridCoords.x}, {_gridCoords.y}] \n {(CharacterOnTile ? CharacterOnTile.name : "Empty")}";
    }
}