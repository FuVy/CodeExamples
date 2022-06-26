using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Buffs/Effects/ChangeEffectDuration")]
public class BuffEffectDuration : BuffEffect
{
    [SerializeField]
    private bool _overTime = true;
    [SerializeField]
    private StatusEffect _statusEffect;
    [SerializeField]
    private int _duration;

    public override void Apply()
    {
        if (!_buff.Applied)
        {
            if (_overTime)
            {
                _buff.Target.ChangeEffectOverTime(_statusEffect, _duration);
            }
            else
            {
                _buff.Target.ChangeHitEffect(_statusEffect, _duration);
            }
        }
    }

    public override void Debuff() { }

    public override Dictionary<string, string> LocalizationArguments(Buff buff, Creature caster)
    {
        var dictionary = new Dictionary<string, string>();
        dictionary.Add($"{name}_Duration", _duration.ToString());
        return dictionary;
    }
}
