using UnityEngine;

[CreateAssetMenu(fileName = "BurnEffect", menuName = "StatusEffects/BurnEffect")]
public class BurnEffect : StatusEffect{
    public override void OnTick(StatusEffectInstance instance) {
        StatusEffectData data = instance.EffectData;
        float dmgValue = data.StackCount * data.Magnitude;
        DamagePacket dmgPacket = new DamagePacket(dmgValue, null, instance.Owner);
        dmgPacket.IsIndirect = true;
        dmgPacket.DamageType = DamageType.Fire;
        instance.Owner.HealthComponent.TakeDamage(dmgPacket);
    }
}