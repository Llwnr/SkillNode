using UnityEngine;

[CreateAssetMenu(fileName = "BurnEffect", menuName = "StatusEffects/BurnEffect")]
public class BurnEffect : StatusEffect{
    public override void OnTurnStart(StatusEffectInstance instance) {
        StatusEffectData data = instance.EffectData;
        float dmgValue = data.StackCount * data.Magnitude;

        DamagePacket dmgPacket = 
            new DamagePacket(dmgValue, null, instance.Owner)
            .SetIndirect();
        
        instance.Owner.HealthComponent.TakeDamage(dmgPacket);
    }
}