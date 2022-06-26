using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Buffs/Effects/ApplyShield")]
public class BuffApplyShield : BuffEffect
{
    [SerializeField]
    private Shield _shield;
    [SerializeField]
    private bool _removeShieldOnDebuff = false;

    private Shield _appliedShield;

    public override void Apply()
    {
        _appliedShield = Instantiate(_shield);
        _appliedShield.Init(_buff.Target.Health);
        _appliedShield.name = _appliedShield.name + "/fromBuff";
        _buff.Target.Health.Shields.Add(_appliedShield);
    }

    public override void Debuff()
    {
        if (_removeShieldOnDebuff)
        {
            _appliedShield?.Destroy();
        }
    }

    public override Dictionary<string, string> LocalizationArguments(Buff buff, Creature caster)
    {
        return null;
    }

    public override void SetParams(Dictionary<string, object> settings)
    {
        bool applied = false;
        settings.Read("Applied", ref applied);
        if (applied)
        {
            if (settings.TryGetValue("Settings", out var shieldSettings))
            {
                _appliedShield.Set((Dictionary<string, object>)shieldSettings);
            }
        }
        else
        {
            _appliedShield.Destroy();
        }
    }

    public override Dictionary<string, object> Settings()
    {
        Dictionary<string, object> settings = new Dictionary<string, object>();
        settings.Write("Applied", (_appliedShield != null));
        if (_appliedShield != null)
        {
            settings.Write("Settings", _appliedShield.Settings());
        }
        return settings;
    }
}
