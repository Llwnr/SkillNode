using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Trait : ScriptableObject {
    public new string name;
    [TextArea(3,10)]
    public string description;

    //Trait logic goes here. Subscribe to gameplay events to activate trait effects essentially
    public virtual void RegisterHooks(UnitInstance unit) { }
    public virtual void DeregisterHooks(UnitInstance unit) { }
}