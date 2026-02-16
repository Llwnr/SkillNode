using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    public string effectId;
    public new string name;
    [TextArea(3, 10)] public string description;

    public bool isDurationBased = true;
    public float tickInterval = 1.0f;


    public virtual void ApplyTo(StatusEffectInstance instance)
    {
    }

    public virtual void OnTurnStart(StatusEffectInstance instance)
    {
    }

    public virtual void OnTurnEnd(StatusEffectInstance instance)
    {
    }

    public virtual void OnRemoval(StatusEffectInstance instance)
    {
    }
}