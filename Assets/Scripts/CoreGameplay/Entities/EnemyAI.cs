using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    [SerializeField] private Vector2 targetEndpoint;
    [SerializeField] private UnitInstance unitInstance;
    
    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, targetEndpoint, unitInstance.Stats.MoveSpeed.Value);
    }
    
    private UnitInstance self;
    public Trait trait;
    public TraitData traitData;

    private void Start() {
        self = GetComponent<UnitInstance>();
        self.ApplyTrait(trait, traitData);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        StartCoroutine(Attack(other.GetComponent<UnitInstance>()));
    }

    IEnumerator Attack(UnitInstance enemyUnit) {
        if (enemyUnit == null || enemyUnit.HealthComponent.CurrentHp <= 0) yield break;
        
        DamagePacket packet = new DamagePacket(self.Stats.AttackDamage.Value, self, enemyUnit);
        self.Events.OnDamageDealing?.Invoke(packet);

        if (packet.IsCancelled) yield break;
        
        self.Events.OnDamageDealt?.Invoke(packet);
        
        enemyUnit.HealthComponent.TakeDamage(packet);
        
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Attack(enemyUnit));
    }
}