using UnityEngine;

public class SkillInstantiator : MonoBehaviour {
    [SerializeField]private SkillInventory userSkillInventory;
    [SerializeField]private SkillInstance skillInstancePrefab;
    [SerializeField] private Transform container;
    [SerializeField] private Character caster;
    private void Start() {
        foreach (var actionSkill in userSkillInventory.mySkills) {
            SkillInstance skillInstance = Instantiate(skillInstancePrefab, container);
            skillInstance.Init(actionSkill);
            skillInstance.GetComponent<SkillExecutor>().Init(caster);
        }
    }
}