using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour {
    [SerializeField] private UnitInstance _unitSelf;
    [SerializeField]private float _currentHp;
    public float CurrentHp => _currentHp;
    
    private void Start() {
        _currentHp = _unitSelf.Stats.MaxHp.Value;
    }

    public void TakeDamage(DamagePacket dmgPacket) {
        DamagePacket packet = dmgPacket.Clone();
        _unitSelf.Events.OnDamageReceiving?.Invoke(packet);
        
        //Once packet is manipulated.
        if (packet.IsCancelled) return;
        
        if (!packet.IsIndirect) {
            packet.DamageAmount *= 1 / _unitSelf.Stats.Defense.Value;
        }

        _currentHp -= packet.DamageAmount;
        
        if(!packet.IsIndirect) Debug.Log($"{name} received {packet.DamageAmount} damage");
        else Debug.Log($"{name} received {packet.DamageAmount} damage. INDIRECT");
        
        _unitSelf.Events.OnDamageReceived?.Invoke(packet);
        
        if(_currentHp <= 0) Destroy(gameObject);
    }
}