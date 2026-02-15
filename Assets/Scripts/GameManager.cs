using System;
using UnityEngine;

public class GameManager : MonoBehaviour{
    public TimePhase CurrentPhase { get; private set; }

    public static GameManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null) {
            Debug.LogError("[GameManager] GameManager already exists");
            return;
        }

        Instance = this;
    }
}

public enum TimePhase {
    Day,
    Night
}

