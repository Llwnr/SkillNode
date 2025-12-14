using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitStats {
    public new string name;
    public float maxHp, currentHp;
    public float power;
    public float attackSpeed, moveSpeed;
    public float range;

    private List<Trait> _traits = new List<Trait>();
    
    public UnitStats Clone() {
        return (UnitStats)this.MemberwiseClone();
    }
}