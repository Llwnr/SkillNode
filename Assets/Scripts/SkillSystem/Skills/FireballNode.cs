using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "SkillNode/Fireball")]
public class FireballNode : SkillNode{
    public override void Activate() {
        Console.WriteLine($"Fireball activated. {Data}");
    }
}