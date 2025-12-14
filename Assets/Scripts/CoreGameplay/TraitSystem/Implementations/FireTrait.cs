using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireTrait", menuName = "Traits/FireTrait")]
public class FireTrait : Trait {
    public override void RegisterHooks(UnitInstance unit) {
        BurnEffect burn = new BurnEffect();
        unit.AddStatusEffect(burn);
    }
}