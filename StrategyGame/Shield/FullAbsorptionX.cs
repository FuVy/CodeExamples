using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shields/FullAbsorptionXTimes")]
public class FullAbsorptionX : Shield
{
    [SerializeField]
    private int _absorptions = 3;

    private int _currentAbsorptions = 0;
    public override int CalculateAppliedDamage(int damage)
    {
        _currentAbsorptions++;
        TriggerStateUpdate();
        if (_currentAbsorptions >= _absorptions)
        {
            Destroy();
        }
        return 0;
    }

    public override int PredictedDamage(int damage)
    {
        return 0;
    }

    public override void TriggerStateUpdate()
    {
        OnStateChange?.Invoke($"{_absorptions}");
    }

    public override void Init()
    {
        TriggerStateUpdate();
    }

    public override void Set(Dictionary<string, object> settings)
    {
        settings.Read("CurrentAbsorptions", ref _absorptions);
        TriggerStateUpdate();
    }

    public override Dictionary<string, object> Settings()
    {
        Dictionary<string, object> settings = new Dictionary<string, object>();
        settings.Write("CurrentAbsorptions", _currentAbsorptions);
        return settings;
    }

    public override Dictionary<string, string> LocalizationParams(Creature caster)
    {
        var args = new Dictionary<string, string>();
        args.Add("FullAbsorptionX_Absorptions", $"{_absorptions}");
        return args;
    }
}
