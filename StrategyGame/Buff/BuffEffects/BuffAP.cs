using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Buffs/Effects/BuffAbilityPower")]
public class BuffAP : BuffEffect
{
    [SerializeField]
    private int _base;

    public override void Apply()
    {
        _buff.Target.Data.ChangeAbilityPower(_base + (int)(_buff.CasterAP * _buff.Multiplier));
    }

    public override void Debuff()
    {
        _buff.Target.Data.ChangeAbilityPower(-(_base + (int)(_buff.CasterAP * _buff.Multiplier)));
    }

    public override Dictionary<string, string> LocalizationArguments(Buff buff, Creature caster)
    {
        var args = new Dictionary<string, string>();
        args.Add($"{name}_Value", $"{_base + (int)(caster.Data.AbilityPower * buff.Multiplier)}");
        return args;
    }
}
