using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitInstance : MonoBehaviour {
    private UnitStats _stats; //The runtime changeable and latest stats of the unit.
    public UnitStats Stats => _stats;
    
    public List<TraitInstance> _ownedTraits = new List<TraitInstance>();
    public List<StatusEffectInstance> _inflictedStatusEffects = new List<StatusEffectInstance>();

    public UnitEvents Events { get; private set; } = new UnitEvents();

    private void Update() {
        PassUpdateToStatusEffects(Time.deltaTime);
    }

    #region Status Effect Management

    public void ApplyStatusEffect(StatusEffect effect, StatusEffectData effectData) {
        StatusEffectInstance existingEffect = _inflictedStatusEffects.FirstOrDefault(eff => eff.EffectType == effect.GetType());
        if (existingEffect != null) {
            existingEffect.AddStacks(effectData.StackCount);
            existingEffect.Refresh();
            return;
        }

        Debug.Log("Didn't found the effect. Adding new one");
        StatusEffectInstance effectInstance = new StatusEffectInstance(effect, this, effectData);
        effectInstance.Apply();
        _inflictedStatusEffects.Add(effectInstance);
    }

    public void RemoveStatusEffect(StatusEffect effect) {
        StatusEffectInstance existingEffect = _inflictedStatusEffects.FirstOrDefault(eff => eff.EffectType == effect.GetType());
        if (existingEffect != null) {
            existingEffect.Remove();
            _inflictedStatusEffects.Remove(existingEffect);
        }
    }

    void PassUpdateToStatusEffects(float deltaTime) {
        for (int i = _inflictedStatusEffects.Count - 1; i >= 0; i--) {
            _inflictedStatusEffects[i].Update(deltaTime);
        }
    }
    #endregion
    
    #region Trait Management
    
    public void ApplyTrait(Trait trait, TraitData traitData) {
        TraitInstance existingTrait =  _ownedTraits.FirstOrDefault(t => t.TraitType == trait.GetType());
        //Check if trait is unique
        if (existingTrait != null){
            existingTrait.AddStacks(traitData.StackCount);
            return;
        }
        //If trait is unique and hasn't been added
        TraitInstance traitInstance = new TraitInstance(trait, this, traitData);
        traitInstance.Apply();
        _ownedTraits.Add(traitInstance);
    }

    public void RemoveTrait(Trait trait) {
        TraitInstance traitInstance = _ownedTraits.FirstOrDefault(t => t.TraitType == trait.GetType());
        if (traitInstance != null) {
            traitInstance.Remove();
            _ownedTraits.Remove(traitInstance);
        }
    }

    //How to handle trait removal when the object is destroyed..?
    void HandleTraitRemoval() {
        foreach (var traitInstance in _ownedTraits) {
            traitInstance.Remove();
        }
        _ownedTraits.Clear();
    }
    #endregion

    private void OnDestroy() {
        HandleTraitRemoval();
    }
}