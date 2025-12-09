using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "SkillNode/Fireball")]
public class FireballNode : SkillNode{
    public override void Execute(SkillExecutionContext context, SkillData skillData) {
        context.Target.ReceiveDamage(skillData.Power);
    }
}