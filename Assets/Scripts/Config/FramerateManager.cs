using System;
using UnityEngine;

public class FramerateManager : MonoBehaviour {
    public int frameRate = 30;
    private void Start() {
        Application.targetFrameRate = frameRate;
    }
}