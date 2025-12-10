using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {
    private Rigidbody2D _rb;
    [SerializeField] private float moveSpeed;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        ApplyDrag();
        
        float hDir = Input.GetAxis("Horizontal");
        float vDir = Input.GetAxis("Vertical");
        _rb.AddForce(new Vector2(hDir * moveSpeed, vDir * moveSpeed));
    }

    void ApplyDrag() {
        _rb.velocity *= 0.8f;
    }
}