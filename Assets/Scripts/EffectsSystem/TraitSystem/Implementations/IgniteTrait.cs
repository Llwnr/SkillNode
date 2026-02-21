using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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
        Func<DamagePacket, UniTask> inflictBurnToTarget = async (packet) =>
        {
            var runtimeEffectData = new StatusEffectData(
                instance.TraitData.StackCount, // Note: This trait uses trait stacks to determine burn power
                effectData.Magnitude
            );
            packet.Target?.ApplyStatusEffect(burnEffectToApply, runtimeEffectData);

            Debug.Log("Ignite trait activated. Playing animation");
            await UniTask.Delay(1500);
            Debug.Log("Animation has ended");
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
        Func<DamagePacket, UniTask> immuneToFireDamage = async (packet) =>
        {
            if (packet.DamageType == DamageType.Fire) packet.IsCancelled = true;
            await Task.Delay(1);
        };

        instance.Subscriptions.Bind(
            immuneToFireDamage,
            handler => owner.Events.OnBeforeDamageReceived += handler,
            handler => owner.Events.OnBeforeDamageReceived -= handler
        );
    }
}