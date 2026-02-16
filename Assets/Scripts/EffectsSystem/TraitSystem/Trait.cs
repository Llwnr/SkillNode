using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Trait : ScriptableObject
{
    public new string name;
    [TextArea(3, 10)] public string description;

    // Entry point. Logic here should hook into UnitEvents using instance.Subscriptions.
    public abstract void ApplyTo(TraitInstance instance);
    // public virtual void OnRemoval(TraitInstance instance){}
}