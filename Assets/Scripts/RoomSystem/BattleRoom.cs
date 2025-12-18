using UnityEngine;

public class BattleRoom : Room {
    [SerializeField] private float atkBuff;
    public override void OnUnitPlaced(UnitInstance unit) {
        unit.Stats.AttackDamage.AddModifier(atkBuff, false);
    }
}