using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitInstance : MonoBehaviour {
    [SerializeField] private UnitBaseConfig unitConfig;
    private UnitStats _stats; //The runtime changeable and latest stats of the unit.
    public UnitStats Stats => _stats;
    
    public List<TraitInstance> _traits = new List<TraitInstance>();
    public List<StatusEffectInstance> _statusEffects = new List<StatusEffectInstance>();

    public UnitEvents Events { get; private set; } = new UnitEvents();
    public HealthComponent HealthComponent { get; private set; }

    private void Awake() {
        _stats = new UnitStats(unitConfig);
        HealthComponent = GetComponent<HealthComponent>();
    }

    private void Update() {
        UpdateStatusEffectsTimer(Time.deltaTime);
    }

    #region Status Effect Management

    public void ApplyStatusEffect(StatusEffect effect, StatusEffectData effectData) {
        StatusEffectInstance existingEffect = _statusEffects.FirstOrDefault(eff => eff.StatusEffect.effectId == effect.effectId);
        if (existingEffect != null) {
            existingEffect.AddStacks(effectData.StackCount);
            existingEffect.Refresh();
            return;
        }

        StatusEffectInstance effectInstance = new StatusEffectInstance(effect, this, effectData);
        effectInstance.Apply();
        _statusEffects.Add(effectInstance);
    }

    public void RemoveStatusEffect(StatusEffect effect) {
        StatusEffectInstance existingEffect = _statusEffects.FirstOrDefault(eff => eff.StatusEffect.effectId == effect.effectId);
        if (existingEffect != null) {
            existingEffect.Remove();
            _statusEffects.Remove(existingEffect);
        }
    }

    void UpdateStatusEffectsTimer(float deltaTime) {
        for (int i = _statusEffects.Count - 1; i >= 0; i--) {
            _statusEffects[i].Update(deltaTime);
        }
    }
    #endregion
    
    #region Trait Management
    
    public void ApplyTrait(Trait trait, TraitData traitData) {
        TraitInstance existingTrait =  _traits.FirstOrDefault(t => t.TraitType == trait.GetType());
        //Check if trait is unique
        if (existingTrait != null){
            existingTrait.AddStacks(traitData.StackCount);
            return;
        }
        //If trait is unique and hasn't been added
        TraitInstance traitInstance = new TraitInstance(trait, this, traitData);
        traitInstance.Apply();
        _traits.Add(traitInstance);
    }

    public void RemoveTrait(Trait trait) {
        TraitInstance traitInstance = _traits.FirstOrDefault(t => t.TraitType == trait.GetType());
        if (traitInstance != null) {
            traitInstance.Cleanup();
            _traits.Remove(traitInstance);
        }
    }

    //How to handle trait removal when the object is destroyed..?
    void HandleTraitRemoval() {
        foreach (var traitInstance in _traits) {
            traitInstance.Cleanup();
        }
        _traits.Clear();
    }
    #endregion

    private void OnDestroy() {
        HandleTraitRemoval();
    }
}