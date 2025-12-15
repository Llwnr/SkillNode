using System;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectInstance {
    public StatusEffect StatusEffect { get; private set; }
    public StatusEffectData EffectData { get; private set; }
    public UnitInstance Owner { get; private set; }
    public Type EffectType => StatusEffect.GetType();

    public List<Action> CleanupActions = new List<Action>();

    private float _tickTimer = 0f;
    private float _durationTimer = 0f;
    
    public StatusEffectInstance(StatusEffect statusEffect, UnitInstance owner, StatusEffectData effectData) {
        StatusEffect = statusEffect;
        Owner = owner;
        _durationTimer = effectData.Duration;

        EffectData = new StatusEffectData(
            effectData.Duration,
            effectData.StackCount,
            effectData.Magnitude
        );
    }

    public void Update(float deltaTime) {
        if (!StatusEffect.isDurationBased) return;

        _durationTimer -= deltaTime;
        if (_durationTimer <= 0) {
            Owner.RemoveStatusEffect(StatusEffect);
            return;
        }

        if (StatusEffect.tickInterval > 0f) {
            _tickTimer += deltaTime;
            if (_tickTimer >= StatusEffect.tickInterval) {
                _tickTimer -= StatusEffect.tickInterval;
                StatusEffect.OnTick(this);
            }
        }
    }

    public void AddStacks(int stackAmt) {
        EffectData.StackCount += stackAmt;
        Debug.Log("Adding stacks:" + stackAmt + " new amt: " + EffectData.StackCount);
    }

    public void Refresh() {
        if (StatusEffect.isDurationBased) {
            _durationTimer = EffectData.Duration;
        }
        
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