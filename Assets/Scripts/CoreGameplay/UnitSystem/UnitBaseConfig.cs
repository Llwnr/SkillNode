using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfig", menuName = "Unit/UnitBaseConfig")]
public class UnitBaseConfig : ScriptableObject {
    public float baseMaxHp;
    public float baseAttack;
    public float baseDefense;
    public float baseMoveSpeed;
}