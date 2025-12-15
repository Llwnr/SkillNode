using UnityEngine;

[CreateAssetMenu(fileName = "BurnEffect", menuName = "StatusEffects/BurnEffect")]
public class BurnEffect : StatusEffect{
    public override void ApplyTo(StatusEffectInstance instance) {
        
    }

    public override void OnRemoval(StatusEffectInstance instance) {
        
    }

    public override void OnTick(StatusEffectInstance instance) {
        StatusEffectData data = instance.EffectData;
        Debug.Log($"{instance.Owner.name} received {data.StackCount*data.Magnitude} damage");
    }
}