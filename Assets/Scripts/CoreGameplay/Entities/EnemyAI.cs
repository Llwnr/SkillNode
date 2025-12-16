using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    [SerializeField] private Vector2 targetEndpoint;
    [SerializeField] private UnitInstance unitInstance;
    
    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, targetEndpoint, unitInstance.Stats.MoveSpeed.Value);
    }
}