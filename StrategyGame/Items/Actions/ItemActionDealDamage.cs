using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Items/Actions/DealDamage"))]
public class ItemActionDealDamage : ItemAction
{
    [SerializeField]
    private DamageTypeValuePair[] _pairs;

    public override void Apply(ItemTargetType.Targets targets)
    {
        foreach (Creature target in targets.Creatures)
        {
            foreach (DamageTypeValuePair pair in _pairs)
            {
                target.DealDamage(pair.Value, pair.Type, DamageSource.Item);
            }
            target.GetHit(null, DamageSource.Item);
        }
    }
}
