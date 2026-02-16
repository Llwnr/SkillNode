using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTurnManager : MonoBehaviour
{
    private static GlobalTurnManager _instance;
    public static GlobalTurnManager Instance => _instance;

    private void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError($"{GetType().Name} already exists, destroying.");
            Destroy(this);
        }
    }

    public enum TurnState
    {
        BattleStart,
        TurnStart,
        TurnEnd
    }

    private HashSet<ITurnBasedUnit> _turnBasedUnits = new HashSet<ITurnBasedUnit>();

    public void Subscribe(ITurnBasedUnit unit){ _turnBasedUnits.Add(unit); }
    public void UnSubscribe(ITurnBasedUnit unit) { _turnBasedUnits.Remove(unit); }

    void TurnEnd(UnitController unit)
    {
        //Handle unit's turn end.
        //unit.TurnEnd();
    }
    
    void Manage()
    {
        void OnBattleStart()
        {
            foreach (var unit in _turnBasedUnits)
            {
                unit.BattleStart();
            }
        }
        void OnTurnStart()
        {
            foreach (var unit in _turnBasedUnits)
            {
                unit.TurnStart(TurnEnd);
            }
        }
        void OnTurnEnd()
        {
            foreach (var unit in _turnBasedUnits)
            {
                unit.TurnEnd();
            }
        }
    }
}