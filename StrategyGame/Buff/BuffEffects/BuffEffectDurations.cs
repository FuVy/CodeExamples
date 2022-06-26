using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Buffs/Effects/ChangeEffectDurations")]
public class BuffEffectDurations : BuffEffect
{
    [SerializeField]
    private EffectInfo[] _statusEffectsInfo;

    public override void Apply()
    {
        if (!_buff.Applied)
        {
            foreach (EffectInfo effectInfo in _statusEffectsInfo)
            {
                if (effectInfo.OverTime)
                {
                    _buff.Target.ChangeEffectsOverTime(effectInfo.Duration, effectInfo.Effects);
                }
                else
                {
                    _buff.Target.ChangeEffectsOnHit(effectInfo.Duration, effectInfo.Effects);
                }
            }
        }
    }

    private struct EffectInfo
    {
        public StatusEffect[] Effects;
        public bool OverTime;
        public int Duration;
    }

    public override void Debuff() { }

    public override Dictionary<string, string> LocalizationArguments(Buff buff, Creature caster)
    {
        var args = new Dictionary<string, string>();
        foreach (var pair in _statusEffectsInfo)
        {
            foreach (var effect in pair.Effects)
            {
                if (pair.OverTime)
                {
                    args.Add($"{name}_{effect}_Duration", pair.Duration.ToString());
                }
                else
                {
                    args.Add($"{name}_{effect}_Hits", pair.Duration.ToString());
                }
            }
        }
        return args;
    }
}
