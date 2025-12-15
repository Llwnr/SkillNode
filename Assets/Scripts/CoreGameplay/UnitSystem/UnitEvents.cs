using System;

public class UnitEvents {
    public delegate void DamageDealtEventHandler(
        UnitInstance attacker,
        UnitInstance target,
        float dmgDealtAmt
    );
    public DamageDealtEventHandler OnDamageDealt;

    public delegate void DamageReceivedEventHandler(
        UnitInstance dmgReceiver, 
        UnitInstance dmgDealer,
        float dmgReceivedAmt
    );
    public DamageReceivedEventHandler OnDamageReceived;
    
    public Action OnDeath;
}