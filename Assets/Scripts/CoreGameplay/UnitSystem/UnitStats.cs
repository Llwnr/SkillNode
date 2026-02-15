using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitStats {
    public Stat MaxHp;
    
    public Stat AttackDamage;
    public Stat AttackSpeed;
    
    public Stat MoveSpeed;
    public Stat Defense;

    public UnitStats(UnitBaseConfig config) {
        MaxHp = new Stat(config.baseMaxHp);
        AttackDamage = new Stat(config.baseAttackDamage);
        AttackSpeed = new Stat(config.baseAttackSpeed);
        MoveSpeed = new Stat(config.baseMoveSpeed);
        Defense = new Stat(config.baseDefense);
    }
}