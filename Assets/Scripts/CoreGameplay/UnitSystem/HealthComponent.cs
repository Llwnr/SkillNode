using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private UnitInstance _unitSelf;
    [SerializeField] private float _currentHp;
    public float CurrentHp => _currentHp;

    private void Start()
    {
        _currentHp = _unitSelf.Stats.MaxHp.Value;
    }

    public void TakeDamage(DamagePacket dmgPacket)
    {
        // Clone packet to ensure modifications (like armor reduction) don't affect the original sender's reference.
        DamagePacket packet = dmgPacket.Clone();
        // 1. Pre-calculation Event: Allow traits to modify/cancel damage (e.g., Shields, Immunity).
        _unitSelf.Events.OnBeforeDamageReceived?.Invoke(packet);

        if (packet.IsCancelled) return;

        // 2. Defense Calculation (Only for direct damage).
        // Formula: Damage = Raw / Defense. (Diminishing returns logic).
        if (!packet.IsIndirect)
        {
            packet.DamageAmount *= 1 / _unitSelf.Stats.Defense.Value;
        }

        _currentHp -= packet.DamageAmount;

        if (!packet.IsIndirect) Debug.Log($"{name} received {packet.DamageAmount} damage");
        else Debug.Log($"{name} received {packet.DamageAmount} INDIRECT damage.");

        // 3. Post-calculation Event: Trigger reactions.
        _unitSelf.Events.OnDamageReceived?.Invoke(packet);

        if (_currentHp <= 0) Destroy(gameObject);
    }
}