using UnityEngine;

public abstract class StatusEffect : ScriptableObject {
    public new string name;
    [TextArea(3,10)]
    public string description;

    public bool isDurationBased = true;
    public float tickInterval = 1.0f;
    
    
    public abstract void ApplyTo(StatusEffectInstance instance);
    public abstract void OnTick(StatusEffectInstance instance);
    public virtual void OnRemoval(StatusEffectInstance instance){}
}