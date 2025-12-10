using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillInventory", menuName = "SkillInventory", order = 0)]
public class SkillInventory : ScriptableObject {
    public List<ActionSkill> mySkills = new  List<ActionSkill>();
}