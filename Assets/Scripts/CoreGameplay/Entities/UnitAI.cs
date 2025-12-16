using System;
using System.Collections;
using UnityEngine;

public class UnitAI : MonoBehaviour {
    private UnitInstance self;
    public Trait trait;
    public TraitData traitData;

    private void Start() {
        self = GetComponent<UnitInstance>();
        self.ApplyTrait(trait, traitData);
    }

    private void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        StartCoroutine(Attack(other.GetComponent<UnitInstance>()));
    }

    IEnumerator Attack(UnitInstance enemyUnit) {
        DamagePacket packet = new DamagePacket(self.Stats.AttackDamage.Value, self, enemyUnit);
        self.Events.OnDamageDealing?.Invoke(packet);

        if (packet.IsCancelled) yield break;
        
        self.Events.OnDamageDealt?.Invoke(packet);
        
        enemyUnit.HealthComponent.TakeDamage(packet);
        
        yield return new WaitForSeconds(3);
        StartCoroutine(Attack(enemyUnit));
    }
}