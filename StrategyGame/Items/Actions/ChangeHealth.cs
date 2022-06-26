using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Items/Actions/ChangeHealth"))]
public class ChangeHealth : ItemAction
{
    [SerializeField]
    private bool _changeMaxHealth = false;

    [SerializeField]
    private int _value = 5;

    [SerializeField]
    private ItemTargetType _targetType;

    public override void Apply(ItemTargetType.Targets targets)
    {
        foreach (Creature creature in targets.Creatures)
        {
            if (_changeMaxHealth)
            {
                creature.Health.Change(_value, _value);
            }
            else
            {
                creature.Health.Change(_value);
            }
        }
    }
}
