using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

[CreateAssetMenu(menuName = "Abilities/Buffs/Effects/ChangeResistance")]
public class BuffResistance : BuffEffect
{
    [SerializeField]
    private DamageTypeValuePair[] _pairs;

    public override void Apply()
    {
        for (int i = 0; i < _pairs.Length; i++)
        {
            _buff.Target.Data.ChangeResistance(_pairs[i].Type, _pairs[i].Value);
        }
    }

    public override void Debuff()
    {
        for (int i = 0; i < _pairs.Length; i++)
        {
            _buff.Target.Data.ChangeResistance(_pairs[i].Type, -_pairs[i].Value);
        }
    }

    public override Dictionary<string, string> LocalizationArguments(Buff buff, Creature caster)
    {
        var args = new Dictionary<string, string>();
        foreach (var pair in _pairs)
        {
            args.Add($"{name}_{pair.Type}", $"{pair.Value}");
        }
        return args;
    }
}
