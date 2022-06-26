using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Buffs/Effects/DealDamage")]
public class BuffDealDamage : BuffEffect
{
    [SerializeField]
    private int _base;
    [SerializeField]
    private DamageType _damageType;
    [SerializeField]
    private DamageSource _source = DamageSource.Dot;

    public override void Apply()
    {
        if (_buff.Duration > 1)
        {
            _buff.Target.OnTurnEnd += Damage;
        }
        else
        {
            Damage();
        }
    }

    public override void Debuff()
    {
        if (_buff.Duration > 1)
        {
            _buff.Target.OnTurnEnd -= Damage;
        }
    }

    public override Dictionary<string, string> LocalizationArguments(Buff buff, Creature caster)
    {
        var args = new Dictionary<string, string>();
        args.Add($"{name}_Damage", $"{_base + Mathf.CeilToInt(caster.Data.AbilityPower * buff.Multiplier)}");
        return args;
    }

    private void Damage()
    {
        _buff.Target.DealDamage(_base + (Mathf.CeilToInt(_buff.CasterAP * _buff.Multiplier)), _damageType, _source);
        _buff.Target.GetHit(null, _source);
    }
}
