using System.Collections.Generic;

public enum DamageType {
    Physical,
    Fire,
    Cold,
    Poison,
} 

public class DamagePacket {
    public float DamageAmount;
    public UnitInstance Attacker;
    public UnitInstance Target;
    public bool IsCancelled;
    public bool IsIndirect;
    public DamageType DamageType;

    //Visual stuff to be implemented later
    public List<string> Popups;

    public DamagePacket(float amt, UnitInstance attacker, UnitInstance target, DamageType damageType = DamageType.Physical) {
        DamageAmount = amt;
        Attacker = attacker;
        Target = target;
    }

    /* RULE ON INDIRECT:
     *  Has no attacker. Meaning, cannot pin point the source applier of the damage
     *  Ignores all defenses
     */
    public DamagePacket SetIndirect() {
        IsIndirect = true;
        return this;
    }

    public DamagePacket Clone() {
        return (DamagePacket)MemberwiseClone();
    }
}