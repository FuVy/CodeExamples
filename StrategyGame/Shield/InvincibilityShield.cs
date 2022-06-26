using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shields/InvincibilityShield")]
public class InvincibilityShield : Shield
{
    [SerializeField]
    private int _duration = 1;

    private int _turn = 0;
    private bool _applied = false;

    public override int CalculateAppliedDamage(int damage)
    {
        if (Health.Current + damage < 1)
        {
            if (!_applied)
            {
                TurnHandler.Instance.OnTurn += HandleTurns;
            }
            _applied = true;
            _priority = -1000;
            return 0;
        }
        if (_applied)
        {
            return 0;
        }
        else
        {
            return damage;
        }
    }

    public override int PredictedDamage(int damage)
    {
        return 0;
    }

    public override void Init()
    {
        TriggerStateUpdate();
    }

    public override void TriggerStateUpdate()
    {
        OnStateChange?.Invoke($"{_turn - _duration}");
    }

    private void Disable()
    {
        TurnHandler.Instance.OnTurn -= HandleTurns;
        Destroy();
    }

    private void HandleTurns()
    {
        _turn++;
        if (_turn >= _duration)
        {
            Disable();
        }
        TriggerStateUpdate();
    }

    public override void Set(Dictionary<string, object> settings)
    {
        settings.Read("Duration", ref _duration);
        settings.Read("Turn", ref _turn);
        settings.Read("Applied", ref _applied);
        if (_applied)
        {
            _priority = -1000;
        }
        TriggerStateUpdate();
    }

    public override Dictionary<string, object> Settings()
    {
        var settings = new Dictionary<string, object>();
        settings.Add("Duration", _duration);
        settings.Add("Turn", _turn);
        settings.Add("Applied", _applied);
        return settings;
    }

    public override Dictionary<string, string> LocalizationParams(Creature caster)
    {
        return null;
    }
}
