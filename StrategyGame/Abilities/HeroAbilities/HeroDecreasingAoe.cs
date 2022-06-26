using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Hero/DecreasingAoe")]
public class HeroDecreasingAoe : HeroAbility
{
    [SerializeField]
    protected int _distance = 3;
    [SerializeField]
    private int _decreaseStep = 5;
    protected override void Action()
    {
        _ability = _creature.StartCoroutine(HandleAoe());
    }

    protected virtual int Distance => _distance;
    protected virtual int DecreaseStep => _decreaseStep;

    private IEnumerator HandleAoe()
    {
        yield return new WaitForSeconds(_castTime);
        int rightMost = _creature.CurrentCell.CellIndexes.x + Distance + 1;
        int damage = _creature.Data.AbilityPower;
        var y = _creature.CurrentCell.CellIndexes.y;
        for (int x = _creature.CurrentCell.CellIndexes.x + 1; x < rightMost; x++)
        {
            Creature target = _cells[y, x].ContainedCreature;
            if (target != null && ((MonsterData)target.Data).Type != MonsterData.MonsterType.HeroSummon)
            {
                ApplyDamage(damage, target);
            }
            damage -= DecreaseStep;
            if (damage < 0)
            {
                damage = 0;
            }
        }
        _ability = null;
    }

    protected virtual void ApplyDamage(int damage, Creature target)
    {
        target.DealDamage(Mathf.CeilToInt(damage * _multiplier), _damageType, DamageSource.Spell);
        target.Animator.SetTrigger("Hit");
        target.GetHit(_creature, DamageSource.Spell);
    }
}
