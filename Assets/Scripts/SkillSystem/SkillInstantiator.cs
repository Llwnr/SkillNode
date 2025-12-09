using UnityEngine;

public class SkillInstantiator : MonoBehaviour {
    [SerializeField]private SkillInventory userSkillInventory;
    [SerializeField]private SkillInstance skillInstancePrefab;
    [SerializeField] private Transform container;
    private void Start() {
        foreach (var actionSkill in userSkillInventory.mySkills) {
            SkillInstance skillInstance = Instantiate(skillInstancePrefab, container);
            skillInstance.Init(actionSkill);
        }
    }
}