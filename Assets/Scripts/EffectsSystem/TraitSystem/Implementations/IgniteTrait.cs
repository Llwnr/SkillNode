using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IgniteTrait", menuName = "Traits/IgniteTrait")]
public class IgniteTrait : Trait {
    [SerializeField]private StatusEffect burnEffectToApply;
    [SerializeField]private StatusEffectData effectData;
    
    public override void ApplyTo(TraitInstance instance) {
        SetupBurnOnAttack(instance);
        SetupBurnImmunity(instance);
    }

    //Will give the trait holder the ability to inflict burn to its targets when attacking
    void SetupBurnOnAttack(TraitInstance instance) {
        UnitInstance owner = instance.Owner;
        Action<DamagePacket> inflictBurnToTarget = (packet) => {
            var runtimeEffectData = new StatusEffectData(
                instance.TraitData.StackCount,
                effectData.Magnitude
            );
            packet.Target?.ApplyStatusEffect(burnEffectToApply, runtimeEffectData);
        };
        
        instance.Subscriptions.Bind(
            handler: inflictBurnToTarget,
            subscribe: handler => owner.Events.OnDamageDealt += handler,
            unsubscribe: handler => owner.Events.OnDamageDealt -= handler
            );
    }

    //Makes the owner immune to burn/fire damage
    void SetupBurnImmunity(TraitInstance instance) {
        UnitInstance owner = instance.Owner;
        Action<DamagePacket> immuneToFireDamage = (packet) => {
            if (packet.DamageType == DamageType.Fire) packet.IsCancelled = true;
        };

        instance.Subscriptions.Bind(
            handler: immuneToFireDamage,
            subscribe: handler => owner.Events.OnDamageReceiving += handler,
            unsubscribe: handler => owner.Events.OnDamageReceiving -= handler
        );
    }
}