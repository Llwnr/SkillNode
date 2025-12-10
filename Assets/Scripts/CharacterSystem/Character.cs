using System;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    [SerializeField]private float healthPoints;
    public int moveRange = 3;
    public float HealthPoints => healthPoints;
    public Action<float> OnDamageReceived;
    
    public StringBoolDictionary statusEffects = new StringBoolDictionary();
    
    public void ReceiveDamage(float damage) {
        healthPoints -= damage;
        OnDamageReceived?.Invoke(healthPoints);
    }

    public void ApplyEffect(string key) {
        statusEffects[key] = true;
    }

}