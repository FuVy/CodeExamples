using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(menuName = "Abilities/Buffs/Effects/Heal")]
public class BuffHeal : BuffEffect
{
    [SerializeField]
    private int _base;

    [SerializeField]
    private bool _ignoreCasterAP = false;

    public override void Apply()
    {
        if (_buff.Duration > 1)
        {
            _buff.Target.OnTurnEnd += Heal;
        }
        else if (!_buff.Applied)
        {
            Heal();
        }
    }

    public override void Debuff()
    {
        _buff.Target.OnTurnEnd -= Heal;
    }

    public override Dictionary<string, string> LocalizationArguments(Buff buff, Creature caster)
    {
        var args = new Dictionary<string, string>();
        int healValue;
        if (_ignoreCasterAP)
        {
            healValue = Mathf.CeilToInt(_base * buff.Multiplier);
        }
        else
        {
            healValue = _base + Mathf.CeilToInt(buff.CasterAP * caster.Data.AbilityPower);
        }
        args.Add($"{name}_HealValue", healValue.ToString());
        return args;
    }

    private void Heal()
    {
        if (_ignoreCasterAP)
        {
            _buff.Target.Health.Change(Mathf.CeilToInt(_base * _buff.Multiplier));
        }
        else
        {
            _buff.Target.Health.Change(_base + Mathf.CeilToInt(_buff.CasterAP * _buff.Multiplier));
        }
    }
}
