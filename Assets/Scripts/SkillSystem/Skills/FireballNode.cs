using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "SkillNode/Fireball")]
public class FireballNode : SkillNode{
    public override void Execute(SkillData skillData) {
        Debug.Log($"Fireball activated. {skillData}");
    }
}