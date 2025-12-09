using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillData {
    public string Name;
    public float Power;
    public string Element;
    public float Cost;

    [SerializeField]
    private StringFloatDictionary stats =  new StringFloatDictionary();
    [SerializeField]
    private StringListStringDictionary listStats =  new StringListStringDictionary();

    public void SetStats(string key, float value) {
        if (stats.ContainsKey(key)) {
            stats[key] = value;
            return;
        }
        stats.Add(key, value);
    }

    public float GetFloat(string key) {
        if (stats.TryGetValue(key, out var value)) {
            return value;
        }
        return 0;
    }

    public SkillData Clone() {
        return new SkillData{
            Name = this.Name,
            Power = this.Power,
            Element = this.Element,
            Cost = this.Cost,
            stats = stats,
            listStats = listStats
        };
    }

    public override string ToString() {
        return $"{Name}, Power: {Power}, Cost: {Cost}, Element: [{Element}]";
    }
}