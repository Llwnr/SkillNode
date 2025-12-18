using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour {
    protected List<UnitInstance> Units = new List<UnitInstance>();

    private void Start() {
        foreach (var unitInstance in GetComponentsInChildren<UnitInstance>()) {
            TryPlaceUnit(unitInstance);
        }
    }

    public abstract void OnUnitPlaced(UnitInstance unit);

    public bool TryPlaceUnit(UnitInstance unit) {
        if (unit == null) return false;
        
        Units.Add(unit);
        
        OnUnitPlaced(unit);
        return true;
    }
}