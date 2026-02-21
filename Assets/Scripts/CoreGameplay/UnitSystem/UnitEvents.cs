using System;
using Cysharp.Threading.Tasks;

public class UnitEvents {
    //Why: To control data before damage is dealt/received. 
    public Func<DamagePacket, UniTask> OnBeforeDamageDealt;
    public Func<DamagePacket, UniTask> OnBeforeDamageReceived; //For ex: Cancel damage entirely
    
    public Func<DamagePacket, UniTask> OnDamageDealt;
    public Func<DamagePacket, UniTask> OnDamageReceived;
    
    //Turn based events
    public Func<UniTask> OnBattleStart;
    public Func<UniTask> OnTurnStart;
    public Func<UniTask> OnTurnEnd;
    
    public Func<UniTask> OnDeath;
}