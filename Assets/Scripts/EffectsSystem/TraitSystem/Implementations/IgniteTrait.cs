using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IgniteTrait", menuName = "Traits/IgniteTrait")]
public class IgniteTrait : Trait
{
    [SerializeField] private StatusEffect burnEffectToApply;
    [SerializeField] private StatusEffectData effectData;

    public override void ApplyTo(TraitInstance instance)
    {
        AddBurnOnAttackTrait(instance);
        AddBurnImmunity(instance);
    }

    // Logic: When owner deals damage -> Apply Burn Effect to target.
    private void AddBurnOnAttackTrait(TraitInstance instance)
    {
        var owner = instance.Owner;

        // Define the behavior
        Action<DamagePacket> inflictBurnToTarget = (packet) =>
        {
            var runtimeEffectData = new StatusEffectData(
                instance.TraitData.StackCount, // Note: This trait uses trait stacks to determine burn power
                effectData.Magnitude
            );
            packet.Target?.ApplyStatusEffect(burnEffectToApply, runtimeEffectData);
        };

        // Bind safely using SubscriptionManager
        instance.Subscriptions.Bind(
            inflictBurnToTarget,
            method => owner.Events.OnDamageDealt += method,
            method => owner.Events.OnDamageDealt -= method
        );
    }

    // Logic: When owner receives Fire damage -> Cancel it.
    private void AddBurnImmunity(TraitInstance instance)
    {
        var owner = instance.Owner;
        Action<DamagePacket> immuneToFireDamage = (packet) =>
        {
            if (packet.DamageType == DamageType.Fire) packet.IsCancelled = true;
        };

        instance.Subscriptions.Bind(
            immuneToFireDamage,
            handler => owner.Events.OnBeforeDamageReceived += handler,
            handler => owner.Events.OnBeforeDamageReceived -= handler
        );
    }
}