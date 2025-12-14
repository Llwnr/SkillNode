using UnityEngine;

[CreateAssetMenu(fileName = "SkillNode", menuName = "SkillNode/")]
public abstract class UnitBase : ScriptableObject {
    public string unitName;
    public Sprite sprite;
    public UnitStats unitStats;
}