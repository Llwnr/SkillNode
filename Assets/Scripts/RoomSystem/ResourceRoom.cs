using System;
using UnityEngine;

public class ResourceRoom : Room {
    public float resourceTick = 1f;
    public float timer = 0f;

    private float harvestStrength = 1f;
    public override void OnUnitPlaced(UnitInstance unit) {
        harvestStrength += unit.Stats.AttackDamage.Value;
    }

    public override void OnUnitRemoved(UnitInstance unit) {
        
    }

    private void Update() {
        if (MyUnits.Count > 0) {
            timer += Time.deltaTime;
            if (timer > resourceTick) {
                timer -= resourceTick;
                Debug.Log($"Harvest {harvestStrength} resources every second");
            }
        }
    }
}