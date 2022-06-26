using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Specific/Alchemist/HealBuff")]
public class AlchemistHealBuff : Buff
{
    [SerializeField]
    private int _baseHeal = 1;

    private int _additionalHeal = 0;
    private int _additionalDuration = 0;

    public int OverallHeal => _baseHeal + _additionalHeal;

    public override void Init(Creature target, SaveData save)
    {
        base.Init(target, save);
        if (save.EffectsSettings.TryGetValue("HealSettings", out var settings))
        {
            settings.Read("AdditionalHeal", ref _additionalHeal);
            settings.Read("AdditionalDuration", ref _additionalDuration);
            _duration += _additionalDuration;
        }
    }
    protected override void CheckDuration()
    {
        if (Removable())
        {
            Remove();
        }
        else
        {
            Target.Health.Change(_baseHeal + _additionalHeal);
            TriggerDurationUpdate();
        }
    }

    public void ChangeAdditionalHeal(int value) => _additionalHeal += value;

    protected override void StickToCreature()
    {
        Target.OnTurnStart += HandleTurns;
        Target.Buffs.Add(this);
        Applied = true;
        Target.TriggerBuffApplied(this);
    }

    public override void Remove()
    {
        Target.OnTurnStart -= HandleTurns;
        OnRemoval?.Invoke();
        Target.Buffs.Remove(this);
        for (int i = _effects.Length - 1; i >= 0; i--)
        {
            _effects[i].Debuff();
            _effects[i].Destroy();
        }
        Destroy(this);
    }
    public override SaveData Save()
    {
        var save = base.Save();
        var settings = new Dictionary<string, object>();
        settings.Write("AdditionalHeal", _additionalHeal);
        settings.Write("AdditionalDuration", _additionalDuration);
        save.EffectsSettings.Add("HealSettings", settings);
        return save;
    }
}
