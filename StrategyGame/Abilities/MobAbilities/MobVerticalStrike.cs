using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Mob/VerticalStrike")]
public class MobVerticalStrike : MobAbility
{
    [SerializeField]
    private int _distance = 3;
    [SerializeField]
    private Buff[] _buffs;

    protected override bool Castable()
    {
        return _creature.CurrentCell.CellIndexes.x - _distance <= 1;
    }
    protected override void Action()
    {
        var cells = Field.Instance.Cells;
        List<Creature> targets = new List<Creature>();
        targets.Add(cells[0, 1].ContainedCreature);
        targets.Add(cells[1, 1].ContainedCreature);
        targets.Add(cells[2, 1].ContainedCreature);
        var damage = Mathf.CeilToInt(_multiplier * _creature.Data.AbilityPower);
        foreach (var target in targets)
        {
            target.DealDamage(damage, _damageType, DamageSource.Spell);
            if (_buffs != null)
            {
                foreach (var buff in _buffs)
                {
                    target.ApplyBuff(buff, _creature);
                }
            }
        }
    }

    public override void UpdateLocalizationArguments(Creature creature)
    {
        LocalizationArgs = new Dictionary<string, string>();
        LocalizationArgs.Add("Damage", $"{Mathf.CeilToInt(_multiplier * _creature.Data.AbilityPower)}");
        LocalizationArgs.Add("Distance", $"{_distance}");
        foreach (var buff in _buffs)
        {
            LocalizationArgs.Add(buff.LocalizationArgs(_creature));
        }
    }
}
