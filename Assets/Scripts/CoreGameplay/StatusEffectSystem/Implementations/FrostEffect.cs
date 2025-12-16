using UnityEngine;

[CreateAssetMenu(fileName = "FrostEffect", menuName = "StatusEffects/FrostEffect")]
public class FrostEffect : StatusEffect {
    public float slowDebuff;
    public override void ApplyTo(StatusEffectInstance instance) {
        instance.Owner.Stats.MoveSpeed.AddModifier(-slowDebuff, true);
        Debug.Log($"Applied frost to {instance.Owner.name}");
    }

    public override void OnRemoval(StatusEffectInstance instance) {
        instance.Owner.Stats.MoveSpeed.RemoveModifier(-slowDebuff, true);
    }
}