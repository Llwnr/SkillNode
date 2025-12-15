using System;
using UnityEngine;

public class TraitTester : MonoBehaviour {
    public UnitInstance unit;
    public UnitInstance enemy;
    public Trait igntieTrait;

    public void GiveIgniteTrait() {
        unit.ApplyTrait(igntieTrait, new TraitData{StackCount = 5});
    }

    public void DealDamageToUnit() {
        unit.Events.OnDamageDealt?.Invoke(unit, enemy, 9999);
    }

    public void RemoveTrait() {
        unit.RemoveTrait(igntieTrait);
    }
}