public abstract class StatusEffect {
    public abstract void RegisterHooks(UnitInstance unit);
    public abstract void DeregisterHooks(UnitInstance unit);

    public override string ToString() {
        return $"{GetType().Name}";
    }
}