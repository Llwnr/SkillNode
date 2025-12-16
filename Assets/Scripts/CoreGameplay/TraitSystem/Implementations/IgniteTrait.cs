using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IgniteTrait", menuName = "Traits/IgniteTrait")]
public class IgniteTrait : Trait {
    [SerializeField]private StatusEffect burnEffectToApply;
    [SerializeField]private StatusEffectData effectData;
    
    public override void ApplyTo(TraitInstance instance) {
        UnitInstance owner = instance.Owner;

        Action<DamagePacket> inflictBurnToTarget = (packet) => {
            var runtimeEffectData = new StatusEffectData(
                effectData.Duration,
                instance.TraitData.StackCount,
                effectData.Magnitude
            );
            packet.Target?.ApplyStatusEffect(burnEffectToApply, runtimeEffectData);
        };
        
        owner.Events.OnDamageDealt += inflictBurnToTarget;
        
        instance.CleanupActions.Add(() => {
            owner.Events.OnDamageDealt -= inflictBurnToTarget;
        });
    }

    public override void OnRemoval(TraitInstance instance) {
        //Generally don't need to do much. Let the CleanupAction of TraitInstance handle removal.
    }
}