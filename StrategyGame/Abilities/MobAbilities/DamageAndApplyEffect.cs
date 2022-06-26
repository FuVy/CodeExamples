using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Mob/DamageAndApplyEffect")]
public class DamageAndApplyEffect : MobAbility
{
    [SerializeField]
    private int _distance = 3;
    [SerializeField]
    private ResistancePair[] _effects;
    [SerializeField]
    private Buff[] _buffs;

    private Creature _enemy;

    protected override bool Castable()
    {
        return EnemiesInRange();
    }

    protected override void Action()
    {
        SetTurnTime();
        _creature.StartCoroutine(HandleCast());
    }

    private IEnumerator HandleCast()
    {
        yield return new WaitForSeconds(_castTime);
        var damage = _creature.Data.AbilityPower ;
        _enemy.DealDamage(Mathf.CeilToInt(damage * _multiplier), _damageType, DamageSource.Spell);
        _enemy.GetHit(_creature);
        foreach (ResistancePair pair in _effects)
        {
            _enemy.ChangeEffectOverTime(pair.Effect, pair.Value);
        }
        foreach (var buff in _buffs)
        {
            _enemy.ApplyBuff(buff, _creature);
        }
    }

    private bool EnemiesInRange()
    {
        var currentCell = _creature.CurrentCell.CellIndexes;
        var furthestCellIndex = Mathf.Clamp(currentCell.x - _distance, 0, currentCell.x);
        for (int i = currentCell.x; i >= furthestCellIndex; i--)
        {
            var creature = _cells[currentCell.y, i].ContainedCreature;
            if (creature != null)
            {
                if (creature.GetType() == typeof(Hero))
                {
                    _enemy = creature;
                    return true;
                }
                else
                {
                    if (((MonsterData)creature.Data).Type == MonsterData.MonsterType.HeroSummon)
                    {
                        _enemy = creature;
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public override void UpdateLocalizationArguments(Creature creature)
    {
        LocalizationArgs = new Dictionary<string, string>();
        foreach (var buff in _buffs)
        {
            LocalizationArgs.Add(buff.LocalizationArgs(_creature));
        }
    }

    [System.Serializable]
    private struct ResistancePair
    {
        public StatusEffect Effect;
        public int Value;
    }
}
