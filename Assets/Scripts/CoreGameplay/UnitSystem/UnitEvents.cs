using System;

public class UnitEvents {
    //Why: To control data before damage is dealt/received. 
    public Action<DamagePacket> OnBeforeDamageDealt;
    public Action<DamagePacket> OnBeforeDamageReceived; //For ex: Cancel damage entirely
    
    public Action<DamagePacket> OnDamageDealt;
    public Action<DamagePacket> OnDamageReceived;
    
    //Turn based events
    public Action OnTurnStart;
    public Action OnTurnEnd;
    
    public Action OnDeath;
}