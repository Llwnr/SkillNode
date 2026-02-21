using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using PrimeTween;

[RequireComponent(typeof(UnitInstance))]
public class UnitController : MonoBehaviour, ITurnBasedUnit {
    private UnitInstance _unit;
    public Trait trait;
    public TraitData traitData;

    // private bool isMyTurn = false;

    private void Start() {
        _unit = GetComponent<UnitInstance>();
        _unit.ApplyTrait(trait, traitData);
        TurnOrchestrator.Instance.AddReference(this); 
    }

    public async UniTask Attack()
    {
        await _unit.Events.OnDamageDealt.Invoke(new DamagePacket(5, _unit, null));
        Debug.Log("Attack finished");
    }

    private void OnDisable()
    {
        TurnOrchestrator.Instance.Remove(this);
    }
    
    #region Turn Management
    public async UniTask BattleStart()
    {
        await WaitForEvent(_unit.Events.OnBattleStart);
    }

    public async UniTask TurnStart()
    {
        //Do turn start tasks
        await WaitForEvent(_unit.Events.OnTurnStart);
    }

    public async UniTask TurnEnd()
    {
        //Do turn end tasks
        await WaitForEvent(_unit.Events.OnTurnEnd);
    }

    private async UniTask WaitForEvent(Func<UniTask> unitEvent)
    {
        if (unitEvent == null) return;
        // Use GetInvocationList IF your animations/status effects are to be applied sequentially.
        // Otherwise, use UniTask.WhenALl
        var delegates = unitEvent.GetInvocationList();
        UniTask[] tasks = new UniTask[delegates.Length];
        for (int i = 0; i < delegates.Length; i++)
        {
            tasks[i] = ((Func<UniTask>)delegates[i]).Invoke();
        }

        await UniTask.WhenAll(tasks);
    }
    #endregion
}