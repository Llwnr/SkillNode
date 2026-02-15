using UnityEngine;

public class BattleRoom : Room {
    [SerializeField] private float atkBuff, maxHpBuff, movespeedBuff, defenseBuff;
    public override void OnUnitPlaced(UnitInstance unit) {
        unit.Stats.AttackDamage.AddModifier(atkBuff, false);
        unit.Stats.MaxHp.AddModifier(maxHpBuff, false);
        unit.Stats.Defense.AddModifier(defenseBuff, false);
        unit.Stats.MoveSpeed.AddModifier(movespeedBuff, false);
    }

    public override void OnUnitRemoved(UnitInstance unit) {
        Debug.Log($"OnUnitRemoved {unit}");
        unit.Stats.AttackDamage.RemoveModifier(atkBuff, false);
        unit.Stats.MaxHp.RemoveModifier(maxHpBuff, false);
        unit.Stats.Defense.RemoveModifier(defenseBuff, false);
        unit.Stats.MoveSpeed.RemoveModifier(movespeedBuff, false);
    }
}