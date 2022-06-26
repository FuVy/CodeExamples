using Guards.Abilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(menuName = "Abilities/Buffs/Effects/AddAbility")]
public class BuffNewAbility : BuffEffect
{
    [SerializeReference]
    private Ability _ability;

    private bool _destroyed = false;
    
    public override void Apply()
    {
        _ability = Instantiate(_ability);
        _ability.RemoveCloneInName();
        _ability.Init(_buff.Target);
        _ability.Sub();
        _buff.Target.AdditionalAbilities.Add(_ability);
        _destroyed = false;
    }

    public override void Debuff()
    {
        _buff.Target.AdditionalAbilities.Remove(_ability);
        _ability?.Unsub();
        _ability?.Destroy();
        _destroyed = true;
    }

    public override Dictionary<string, object> Settings()
    {
        var settings = new Dictionary<string, object>();
        settings.Write("Destroyed", _destroyed);
        if (!_destroyed)
        {
            settings.Write("Settings", _ability.Settings());
        }
        return settings;
    }

    public override void SetParams(Dictionary<string, object> settings)
    {
        settings.Read("Destroyed", ref _destroyed);
        if (!_destroyed)
        {
            if (settings.TryGetValue("Destroyed", out var abilitySettings))
            {
                _ability.Set((AbilitySettings)abilitySettings);
            }
        }
        else
        {
            Debuff();
        }
    }

    public override Dictionary<string, string> LocalizationArguments(Buff buff, Creature caster)
    {
        var args = new Dictionary<string, string>();
        args.Add($"{name}_AbilityName", _ability.name);
        return args;
    }
}
