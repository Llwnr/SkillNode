using System;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    [SerializeField] private Character player, enemy;
    private void Start() {
        SetupCharactersInGrid();
    }

    void SetupCharactersInGrid() {
        GridSystem<GridObject> grid = GridManager.Instance.Grid;
        grid.GetGridObject(player.transform.position).SetCharacter(player);
        grid.GetGridObject(enemy.transform.position).SetCharacter(enemy);
    }
}