using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Shields/AbsorbingXShield")]
public class AbsorbingXShield : Shield
{
    [SerializeField]
    private bool _capacityDefinedByAP = false;
    [SerializeField, ShowIf("_capacityDefinedByAP")]
    private float _multiplier = 1f;
    [SerializeField, HideIf("_capacityDefinedByAP")]
    private int _capacity = 5;
    public int Capacity => _capacity;

    public override int CalculateAppliedDamage(int damage)
    {
        _capacity += damage;
        if (_capacity > 0)
        {
            TriggerStateUpdate();
            return 0;
        }
        else
        {
            Destroy();
            return _capacity;
        }
    }
    public override int PredictedDamage(int damage)
    {
        var capacity = _capacity;
        capacity += damage;
        if (capacity >= 0)
        {
            return 0;
        }
        else
        {
            Debug.Log($"predicted damage after damaging shield {capacity}");
            return capacity;
        }
    }

    public override void Init()
    {
        if (_capacityDefinedByAP)
        {
            _capacity = Mathf.CeilToInt(Health.Creature.Data.AbilityPower * _multiplier);
        }
        TriggerStateUpdate();
    }

    public void SetCapacity(int capacity)
    {
        _capacity = capacity;
        TriggerStateUpdate();
    }

    public override void TriggerStateUpdate()
    {
        OnStateChange?.Invoke($"{_capacity}");
    }

    public override void Set(Dictionary<string, object> settings)
    {
        settings.Read("Capacity", ref _capacity);
        TriggerStateUpdate();
    }

    public override Dictionary<string, object> Settings()
    {
        var settings = new Dictionary<string, object>();
        settings.Write("Capacity", _capacity);
        return settings;
    }

    public override Dictionary<string, string> LocalizationParams(Creature caster)
    {
        var args = new Dictionary<string, string>();
        int capacity = _capacity;
        if (_capacityDefinedByAP)
        {
            capacity = Mathf.CeilToInt(caster.Data.AbilityPower * _multiplier);
        }
        args.Add("AbsorbingXShield_Capacity", $"{capacity}");
        return args;
    }
}
