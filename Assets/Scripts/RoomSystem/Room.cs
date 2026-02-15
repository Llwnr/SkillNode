using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Room : MonoBehaviour {
    protected List<UnitInstance> MyUnits = new List<UnitInstance>();
    protected List<UnitInstance> EnemyUnits = new List<UnitInstance>();

    private void Start() {
        foreach (var unitInstance in GetComponentsInChildren<UnitInstance>()) {
            TryPlaceUnit(unitInstance);
        }
    }

    public abstract void OnUnitPlaced(UnitInstance unit);
    public abstract void OnUnitRemoved(UnitInstance unit);

    private async void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.TryGetComponent(out UnitInstance unit)) {
            if (MyUnits.Contains(unit)) {
                return;
            }
            await EnemyUnitEnter(unit);
        }
    }

    public async Task EnemyUnitEnter(UnitInstance enemyUnit){
        EnemyUnits.Add(enemyUnit);
        foreach (var unit in MyUnits) {
            await unit.Controller.Chase(enemyUnit);
        }
    }

    public bool TryPlaceUnit(UnitInstance unit) {
        if (unit == null) return false;
        
        MyUnits.Add(unit);
        
        OnUnitPlaced(unit);
        return true;
    }
}