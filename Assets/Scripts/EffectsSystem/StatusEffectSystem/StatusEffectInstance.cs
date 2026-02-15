using System;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectInstance {
    public StatusEffect StatusEffect { get; private set; }
    public StatusEffectData EffectData { get; private set; }
    public UnitInstance Owner { get; private set; }

    public List<Action> CleanupActions = new List<Action>();

    
    public StatusEffectInstance(StatusEffect statusEffect, UnitInstance owner, StatusEffectData effectData) {
        StatusEffect = statusEffect;
        Owner = owner;

        EffectData = new StatusEffectData(
            effectData.StackCount,
            effectData.Magnitude
        );
    }

    public void AddStacks(int stackAmt) {
        EffectData.StackCount += stackAmt;
    }

    public void Apply() {
        StatusEffect.ApplyTo(this);
    }

    public void Remove() {
        foreach (var cleanupAction in CleanupActions) {
            cleanupAction.Invoke();
        }
        CleanupActions.Clear();
        StatusEffect.OnRemoval(this);
    }
    
}