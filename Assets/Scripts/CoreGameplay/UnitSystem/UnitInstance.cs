using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitInstance : MonoBehaviour {
    private UnitStats _stats; //The runtime changeable and latest stats of the unit.
    public UnitStats Stats => _stats;
    
    private HashSet<Trait> _activeTraits = new HashSet<Trait>();
    private HashSet<StatusEffect> _effects = new HashSet<StatusEffect>();
    
    public Action<UnitInstance> OnAttack;
    public Action<UnitInstance, float> OnDamageReceived;
    public Action OnDeath;

    public void AddStatusEffect(StatusEffect statusEffect) {
        _effects.Add(statusEffect);
    }
    
    #region TraitManagement
    public void AddTrait(Trait trait, int stacks) {
        Trait existingTrait = _activeTraits.FirstOrDefault(t => t.GetType() == trait.GetType());
        if (existingTrait == null) {
            Trait clone = Instantiate(trait);
            clone.name = trait.name;
            
            _activeTraits.Add(clone);
            clone.RegisterHooks(this);
        }
    }

    public void RemoveTrait(Trait trait) {
        Trait existingTrait = _activeTraits.First(t => t.GetType() == trait.GetType());
        if (existingTrait != null) {
            _activeTraits.Remove(trait);
            trait.DeregisterHooks(this);
        }
    }
    #endregion
}