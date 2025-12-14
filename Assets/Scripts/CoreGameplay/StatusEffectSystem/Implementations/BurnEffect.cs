public class BurnEffect : StatusEffect{
    public override void RegisterHooks(UnitInstance unit) {
        unit.OnDamageReceived += BurnDmgOnTick;
    }

    public override void DeregisterHooks(UnitInstance unit) {
        unit.OnDamageReceived -= BurnDmgOnTick;
    }

    void BurnDmgOnTick(UnitInstance unit, float dmgAmt) {
        unit.Stats.currentHp -= 5;
    }
}