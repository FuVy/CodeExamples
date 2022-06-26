using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shields/OracleShield")]
public class OracleShield : Shield
{
    [SerializeField]
    private int _duration = 1;

    private int _turn = 0;
    private bool _applied = false;
    private int _damageToRemove;

    public override int CalculateAppliedDamage(int damage)
    {
        Debug.Log(Health.Current + damage);
        if (Health.Current + damage < 1)
        {
            if (!_applied)
            {
                TurnHandler.Instance.OnTurn += HandleTurns;
                _damageToRemove = -Health.Current;
            }
            _applied = true;

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
        OnStateChange?.Invoke("");
    }

    private void Disable()
    {
        TurnHandler.Instance.OnTurn -= HandleTurns;
        Destroy();
        Health.Change(_damageToRemove);
    }

    private void HandleTurns()
    {
        _turn++;
        if (_turn >= _duration)
        {
            Disable();
            return;
        }
        TriggerStateUpdate();
    }

    public override void Set(Dictionary<string, object> settings)
    {
        settings.Read("Duration", ref _duration);
        settings.Read("Turn", ref _turn);
        settings.Read("Applied", ref _applied);
        settings.Read("DamageToRemove", ref _damageToRemove);
        TriggerStateUpdate();
    }

    public override void TriggerStateUpdate()
    {
        OnStateChange?.Invoke($"{_duration - _turn}");
    }

    public override Dictionary<string, object> Settings()
    {
        var settings = new Dictionary<string, object>();
        settings.Write("Duration", _duration);
        settings.Write("Turn", _turn);
        settings.Write("Applied", _applied);
        settings.Write("DamageToRemove", _damageToRemove);
        return settings;
    }

    public override Dictionary<string, string> LocalizationParams(Creature caster)
    {
        return null;
    }
}