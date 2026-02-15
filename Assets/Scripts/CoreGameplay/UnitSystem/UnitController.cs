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
}