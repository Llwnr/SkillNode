using UnityEngine;

[CreateAssetMenu(fileName = "Rupture", menuName = "SkillNode/Rupture")]
public class RuptureNode : SkillNode{
    public override void Execute(SkillExecutionContext context, SkillData skillData) {
        context.Target.ReceiveDamage(skillData.Power);
        foreach (var kv in skillData.StatusEffects) {
            context.Target.ApplyEffect(kv.Key);
        }
    }
}