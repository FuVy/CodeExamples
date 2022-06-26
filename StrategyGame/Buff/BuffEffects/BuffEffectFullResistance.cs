using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Buffs/Effects/FullResistance")]
public class BuffEffectFullResistance : BuffEffect
{
    [SerializeField]
    private DamageType _damageType;
    [SerializeField]
    private bool _state = true;
    public override void Apply()
    {
        _buff.Target.Data.SetFullResistance(_damageType, _state);
    }

    public override void Debuff()
    {
        _buff.Target.Data.SetFullResistance(_damageType, !_state);
    }

    public override Dictionary<string, string> LocalizationArguments(Buff buff, Creature caster)
    {
        return null;
    }
}
