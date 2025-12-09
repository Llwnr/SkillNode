using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillNode", menuName = "SkillNode/")]
public abstract class SkillNode : ScriptableObject {
    public new string name;
    [SerializeField]
    private SkillData baseData;
    public SkillData BaseData => baseData.Clone();

    public abstract void Execute(SkillExecutionContext context, SkillData skillData);
}