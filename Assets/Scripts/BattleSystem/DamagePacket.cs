using System.Collections.Generic;

public enum DamageType
{
    Physical,
    Fire,
    Cold,
    Poison,
}

// DTO (Data Transfer Object) pattern. 
// Used to pass context around without coupling the Attacker and Victim directly.
public class DamagePacket
{
    public float DamageAmount;
    public UnitInstance Attacker;
    public UnitInstance Target;
    public bool IsCancelled; // Allows traits/abilities to completely nullify damage (e.g. Immunity).
    public bool IsIndirect; // Flag for DoT (Damage over Time) or environmental damage.
    public DamageType DamageType;

    //Visual stuff to be implemented later
    public List<string> Popups;

    public DamagePacket(float amt, UnitInstance attacker, UnitInstance target,
        DamageType damageType = DamageType.Physical)
    {
        DamageAmount = amt;
        Attacker = attacker;
        Target = target;
    }

    /* RULE ON INDIRECT:
     *  Has no attacker. Meaning, cannot pin-point the source applier of the damage
     *  Ignores all defenses
     */
    public DamagePacket SetIndirect()
    {
        IsIndirect = true;
        return this;
    }

    // Prototype Pattern.
    // Necessary because C# passes objects by reference. 
    // Events need to modify a specific instance of a packet without affecting previous logic steps.
    public DamagePacket Clone()
    {
        return (DamagePacket)MemberwiseClone();
    }
}