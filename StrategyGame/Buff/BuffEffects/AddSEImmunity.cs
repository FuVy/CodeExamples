using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Buffs/Effects/AddSEImmunity")]
public class AddSEImmunity : BuffEffect
{
    [SerializeField]
    private StatusEffect[] _statusEffects;
    public override void Apply()
    {
        foreach (var effect in _statusEffects)
        {
            _buff.Target.Data.AdditionalStatusEffectImmunities.Add(effect);
        }
    }

    public override void Debuff()
    {
        foreach (var effect in _statusEffects)
        {
            _buff.Target.Data.AdditionalStatusEffectImmunities.Remove(effect);
        }
    }

    public override Dictionary<string, string> LocalizationArguments(Buff buff, Creature caster)
    {
        return null;
    }
}
