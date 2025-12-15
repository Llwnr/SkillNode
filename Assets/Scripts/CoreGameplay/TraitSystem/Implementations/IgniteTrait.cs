using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IgniteTrait", menuName = "Traits/IgniteTrait")]
public class IgniteTrait : Trait {
    [SerializeField]private BurnEffect burnEffectToApply;
    [SerializeField]private StatusEffectData runtimeEffectData;
    public override void ApplyTo(TraitInstance instance) {
        UnitInstance owner = instance.Owner;

        UnitEvents.DamageDealtEventHandler inflictBurnOnHit = (attacker, target, damage) => {
            Debug.Log($"{target.name} has been inflicted with burn");
            runtimeEffectData.StackCount = instance.TraitData.StackCount; //Strong ignite traits inflict stronger burn stacks
            target.ApplyStatusEffect(burnEffectToApply, runtimeEffectData);
        };
        
        owner.Events.OnDamageDealt += inflictBurnOnHit;
        
        instance.CleanupActions.Add(() => {
            owner.Events.OnDamageDealt -= inflictBurnOnHit;
        });
    }

    public override void OnRemoval(TraitInstance instance) {
        //Generally don't need to do much. Let the CleanupAction of TraitInstance handle removal.
    }
}