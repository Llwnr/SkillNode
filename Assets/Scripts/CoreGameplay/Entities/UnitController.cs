using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using PrimeTween;

public class UnitController : MonoBehaviour {
    private UnitInstance _unit;
    public Trait trait;
    public TraitData traitData;

    private bool _canAttack = true;

    private void Start() {
        _unit = GetComponent<UnitInstance>();
        _unit.ApplyTrait(trait, traitData);
    }

    public async Task Chase(UnitInstance targetUnit) {//Chase the enemy target. Parameters set by room.
        Vector2 distance = targetUnit.transform.position - transform.position;
        transform.position = targetUnit.transform.position - new Vector3(1,0,0);

        await Attack(targetUnit);
    }

    public async Task Attack(UnitInstance targetUnit) {
        while (_canAttack == false) {
            await Task.Yield();
        }

        Debug.Log($"{name} is attacking {targetUnit.name}");
        await Reload();
        await Attack(targetUnit);
    }

    async Task Reload() {
        _canAttack = false;
        float waitDuration = 1/_unit.Stats.AttackSpeed.Value;
        while (_canAttack == false) {
            waitDuration -= Time.deltaTime;
            if (waitDuration <= 0) {
                _canAttack = true;
                break;
            }
            await Task.Yield();
        }
    }

    public void Behaviour() {
        // If in Range -> Attack -> Chase and reload/attack delay -> Repeat
    }
}