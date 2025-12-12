using System;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    [SerializeField] private Character player, enemy;
    private void Start() {
        SetupCharactersInGrid();
    }

    void SetupCharactersInGrid() {
        GridManager.Instance.SnapToGrid(player);
        GridManager.Instance.SnapToGrid(enemy);
    }
}