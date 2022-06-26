using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Items/Actions/ApplyEffects"))]
public class ItemActionApplyEffects : ItemAction
{
    [SerializeField]
    private StatusEffectTypeValuePair[] _pairs;
    public override void Apply(ItemTargetType.Targets targets)
    {
        foreach (Creature target in targets.Creatures)
        {
            foreach (StatusEffectTypeValuePair pair in _pairs)
            {
                target.ChangeEffectOverTime(pair.Type, pair.Value);
            }
        }
    }
}