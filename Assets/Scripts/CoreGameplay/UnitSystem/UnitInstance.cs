using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitInstance : MonoBehaviour
{
    [SerializeField] private UnitBaseConfig unitConfig;
    [SerializeField] private UnitStats _stats; //The runtime changeable and latest stats of the unit.
    public UnitStats Stats => _stats;

    // Lists to track runtime logic attached to this unit.
    public List<TraitInstance> _traits = new List<TraitInstance>();
    public List<StatusEffectInstance> _statusEffects = new List<StatusEffectInstance>();

    // Central event bus for this specific unit.
    public UnitEvents Events { get; private set; } = new UnitEvents();
    public HealthComponent HealthComponent { get; private set; }
    public UnitController Controller { get; private set; }

    private void Awake()
    {
        _stats = new UnitStats(unitConfig); // Initialize runtime stats from config.
        Controller = GetComponent<UnitController>();
        HealthComponent = GetComponent<HealthComponent>();
    }

    #region Status Effect Management

    public void ApplyStatusEffect(StatusEffect effect, StatusEffectData effectData)
    {
        // Check for existing effect to stack it instead of duplicating.
        StatusEffectInstance existingEffect =
            _statusEffects.FirstOrDefault(eff => eff.StatusEffect.effectId == effect.effectId);
        if (existingEffect != null)
        {
            existingEffect.AddStacks(effectData.StackCount);
            return;
        }

        // Create new runtime instance of the effect.
        StatusEffectInstance effectInstance = new StatusEffectInstance(effect, this, effectData);
        effectInstance.Apply();
        _statusEffects.Add(effectInstance);
    }

    public void RemoveStatusEffect(StatusEffect effect)
    {
        StatusEffectInstance existingEffect =
            _statusEffects.FirstOrDefault(eff => eff.StatusEffect.effectId == effect.effectId);
        if (existingEffect != null)
        {
            existingEffect.Remove();
            _statusEffects.Remove(existingEffect);
        }
    }

    // [IMPORTANT] -> Replace with forwarding Turn Manager's turn changes.
    // void UpdateStatusEffectsTimer(float deltaTime) {
    //     for (int i = _statusEffects.Count - 1; i >= 0; i--) {
    //         _statusEffects[i].Update(deltaTime);
    //     }
    // }

    #endregion

    #region Trait Management

    // Logic mirrors Status Effects: Check for existing -> Stack or Create New.
    public void ApplyTrait(Trait trait, TraitData traitData)
    {
        TraitInstance existingTrait = _traits.FirstOrDefault(t => t.TraitType == trait.GetType());
        //Check if trait is unique
        if (existingTrait != null)
        {
            existingTrait.AddStacks(traitData.StackCount);
            return;
        }

        //If trait is unique and hasn't been added
        TraitInstance traitInstance = new TraitInstance(trait, this, traitData);
        traitInstance.Apply();
        _traits.Add(traitInstance);
    }

    public void RemoveTrait(Trait trait)
    {
        TraitInstance traitInstance = _traits.FirstOrDefault(t => t.TraitType == trait.GetType());
        if (traitInstance != null)
        {
            traitInstance.Cleanup();
            _traits.Remove(traitInstance);
        }
    }

    //How to handle trait removal when the object is destroyed..?
    void HandleTraitRemoval()
    {
        foreach (var traitInstance in _traits)
        {
            traitInstance.Cleanup();
        }

        _traits.Clear();
    }

    #endregion

    private void OnDestroy()
    {
        HandleTraitRemoval();
    }
}