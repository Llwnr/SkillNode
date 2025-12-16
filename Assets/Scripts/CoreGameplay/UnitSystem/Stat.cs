using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {
    [SerializeField] private float _baseValue;
    
    private List<float> _flatModifiers = new List<float>();
    private List<float> _multModifiers = new List<float>();

    private float _cachedValue;
    private bool _isDirty = true;

    public float Value{
        get {
            if(_isDirty) Recalculate();
            return _cachedValue;
        }
    }

    public Stat(float baseValue) {
        _baseValue = baseValue;
    }

    public void AddModifier(float amount, bool isMultiplier) {
        if (isMultiplier) {
            _multModifiers.Add(amount);
        }
        else {
            _flatModifiers.Add(amount);
        }
        _isDirty = true;
    }

    public void RemoveModifier(float amount, bool isMultiplier) {
        if (isMultiplier) {
            _multModifiers.Remove(amount);
        }
        else {
            _flatModifiers.Remove(amount);
        }
        _isDirty = true;
    }

    void Recalculate() {
        float finalValue = _baseValue;

        foreach (var flatMod in _flatModifiers) {
            finalValue += flatMod;
        }

        float sumOfMult = GetSumOfMultiplierMods();

        finalValue *= (1f + sumOfMult);

        _cachedValue = Mathf.Max(0,finalValue);
        _isDirty = false;
    }

    private float GetSumOfMultiplierMods() {
        float sumOfMult = 0f;
        foreach (var multMod in _multModifiers) {
            sumOfMult += multMod;
        }

        return sumOfMult;
    }
}