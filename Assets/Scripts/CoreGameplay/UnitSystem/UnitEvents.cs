using System;

public class UnitEvents {
    public Action<DamagePacket> OnDamageDealing;
    public Action<DamagePacket> OnDamageReceiving;
    
    public Action<DamagePacket> OnDamageDealt;
    public Action<DamagePacket> OnDamageReceived;
    
    public Action OnDeath;
}