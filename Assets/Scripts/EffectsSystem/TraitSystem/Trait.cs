using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Trait : ScriptableObject {
    public new string name;
    [TextArea(3,10)]
    public string description;

    /*
     * Trait = passive behavior modifier attached to a unit.
     *
     * Use traits to react to gameplay events (damage, turns, etc.)
     * and apply stat changes or status effects.
     *
     * Rules:
     * - Event-driven only (no Update/timers/deltaTime)
     * - Bind all listeners through instance.Subscriptions
     * - Cleanup is automatic on removal
     *
     * ApplyTo()  → subscribe/apply effects/[IMPORTANT] also implement unsubscribing methods
     * OnRemoval() → optional manual cleanup (rare)
     */
    public abstract void ApplyTo(TraitInstance instance);
    // public virtual void OnRemoval(TraitInstance instance){}
}