using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillNode", menuName = "SkillNode/")]
public abstract class SkillNode : ScriptableObject {
    [SerializeField]
    private SkillData _defaultData;
    public SkillData DefaultData => _defaultData;
    
    public abstract void Execute(SkillData skillData);
}