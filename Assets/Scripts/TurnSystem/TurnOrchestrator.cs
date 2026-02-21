using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TurnOrchestrator : MonoBehaviour
{
    private static TurnOrchestrator _instance;
    public static TurnOrchestrator Instance => _instance;

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

    private HashSet<ITurnBasedUnit> _turnBasedUnitsRef = new HashSet<ITurnBasedUnit>(); //The original list from which _aliveUnits List is populated
    private Queue<ITurnBasedUnit> _aliveUnits = new Queue<ITurnBasedUnit>(); //Where turn management happens. Thus its a queue

    public void AddReference(ITurnBasedUnit unit){_turnBasedUnitsRef.Add(unit);}
    public void Remove(ITurnBasedUnit unit) { _turnBasedUnitsRef.Remove(unit); }

    private async void Start()
    {
        await Task.Delay(10); // Wait for ITurnBasedUnits to add to this script
        await BattleStart();
        
        while (_aliveUnits.Count > 0)
        {
            await Task.Delay(100); //So the game doesn't crashes!!!
            await TurnStart();
        }
    }

    async UniTask BattleStart()
    {
        //Ready the queue from reference
        foreach (var unit in _turnBasedUnitsRef)
        {
            _aliveUnits.Enqueue(unit);
        }
        
        List<UniTask> initialBattleTasks = new List<UniTask>();
        foreach (var unit in _aliveUnits)
        {
            initialBattleTasks.Add(unit.BattleStart());
        }

        await UniTask.WhenAll(initialBattleTasks);
    }

    async UniTask TurnStart()
    {
        ITurnBasedUnit unit = _aliveUnits.Dequeue();
        if (IsInvalid(unit)) return;
        await unit.TurnStart();
        //In case the unit died from certain effects on turn start
        if (IsInvalid(unit)) return;
        await unit.TurnEnd();
        //After their turn ends, wait in line again for their next turn
        if(!IsInvalid(unit)) _aliveUnits.Enqueue(unit);
        
        //Note: We check for IsInvalid after every state in case the unit died during those states
    }

    bool IsInvalid(ITurnBasedUnit unit)
    {
        // 1. Check C# null
        if (unit == null) return true;
    
        // 2. Check Unity Object null (Handle destroyed GameObjects)
        // Note: This casts the interface to a boolean which triggers Unity's overload.
        if (unit.Equals(null)) return true; 

        // 3. Check Logic State (Is it actually in the fight?)
        if (!_turnBasedUnitsRef.Contains(unit)) return true;

        return false;
    }
}