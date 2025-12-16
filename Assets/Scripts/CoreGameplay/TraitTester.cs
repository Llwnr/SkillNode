using System;
using UnityEngine;

public class TraitTester : MonoBehaviour {
    public UnitInstance unit;
    public UnitInstance enemy;
    public Trait igniteTrait;

    public void GiveIgniteTrait() {
        unit.ApplyTrait(igniteTrait, new TraitData{StackCount = 5});
    }

    public void DealDamageToUnit() {
        unit.Events.OnDamageDealt?.Invoke(new DamagePacket(999, unit, enemy));
    }

    public void RemoveTrait() {
        unit.RemoveTrait(igniteTrait);
    }
}