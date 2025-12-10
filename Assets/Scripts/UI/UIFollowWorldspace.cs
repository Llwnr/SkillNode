using System;
using UnityEngine;

public class UIFollowWorldspace : MonoBehaviour {
    [SerializeField] private GameObject worldspaceTarget;
    [SerializeField] private Vector2 offset;

    private void Start() {
        FollowTarget();
    }

    private void FixedUpdate() {
        FollowTarget();
    }

    void FollowTarget() {
        if (worldspaceTarget != null) {
            transform.position = Camera.main.WorldToScreenPoint(worldspaceTarget.transform.position + (Vector3)offset);
        }
    }
}