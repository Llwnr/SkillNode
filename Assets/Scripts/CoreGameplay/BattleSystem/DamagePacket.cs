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

    //Visual stuff
    public List<string> Popups;

    public DamagePacket(float amt, UnitInstance attacker, UnitInstance target) {
        DamageAmount = amt;
        Attacker = attacker;
        Target = target;
    }

    public DamagePacket Clone() {
        return (DamagePacket)MemberwiseClone();
    }
}