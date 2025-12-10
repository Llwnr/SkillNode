using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    [SerializeField] private Character player;
    [SerializeField] private Character enemy;

    [SerializeField]private Character _currActiveCharacter = null;

    public Character CurrActiveCharacter => _currActiveCharacter;

    public int roundNum = 0;
    
    private static TurnManager _instance;
    public static TurnManager Instance => _instance;

    private void Awake() {
        if (_instance != null) {
            throw new Exception("TurnManager already exists");
        }

        _instance = this;
    }

    private void Start() {
        BeginPlayerTurn();
    }

    void BeginPlayerTurn() {
        _currActiveCharacter = player;
    }

    public void EndTurn(Character caster) {
        if (caster != _currActiveCharacter) return;
        if (caster == player) {
            _currActiveCharacter = enemy;
        }
        else {
            roundNum++;
            _currActiveCharacter = player;
        }
    }
}