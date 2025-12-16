using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour {
    [SerializeField] private UnitInstance _unitSelf;
    public float _currentHp;
    
    private void Start() {
        _currentHp = _unitSelf.Stats.MaxHp.Value;
    }

    public void TakeDamage(DamagePacket dmgPacket) {
        DamagePacket packet = dmgPacket.Clone();
        _unitSelf.Events.OnDamageReceiving?.Invoke(packet);
        
        //Once packet is manipulated.
        if (packet.IsCancelled) return;

        _currentHp -= packet.DamageAmount;
        _unitSelf.Events.OnDamageReceived?.Invoke(packet);
    }
}