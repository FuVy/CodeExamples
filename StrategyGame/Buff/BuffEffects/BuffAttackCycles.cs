using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(menuName = "Abilities/Buffs/Effects/ChangeAttackCycles")]
public class BuffAttackCycles : BuffEffect
{
    [SerializeField]
    private int _quantity = 1;

    public override void Apply()
    {
        _buff.Target.Data.ChangeAdditionalAttackCycles(_quantity);
    }

    public override void Debuff()
    {
        _buff.Target.Data.ChangeAdditionalAttackCycles(-_quantity);
    }

    public override Dictionary<string, string> LocalizationArguments(Buff buff, Creature caster)
    {
        var args = new Dictionary<string, string>();
        args.Add($"{name}_Quantity", $"{_quantity}");
        return args;
    }
}
