using UnityEngine;

public class Character : MonoBehaviour {
    [SerializeField]private float healthPoints;
    public float HealthPoints => healthPoints;
    
    public void ReceiveDamage(float damage) {
        healthPoints -= damage;
    }

}