using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossMeleeCast : SecondBossState
{
    private float _castTime = 1f, _castEndTime = 0f;
    private BaseEffectHolder _visualEffect;

    public SecondBossMeleeCast(SecondBoss boss) : base(boss) { }

    public override void Enter()
    {
        _bossLogic.ChangeLocalizationID("SecondBossMelee");
        _bossLogic.Creature.SetAnimator(_bossLogic.MeleeCastAnimator);
        CreateVisualEffect();
        UpdateVisualEffect();
        CalculateCastTime();
    }

    private void CreateVisualEffect()
    {
        _visualEffect = _bossLogic.Creature.EffectsHolder.Create();
        _visualEffect.SetColor(_bossLogic.CastDamageColor);
        _bossLogic.Creature.Health.OnChange += UpdateVisualEffect;
        var abilityDamage = Mathf.CeilToInt(_bossLogic.Creature.Data.AbilityPower * _bossLogic.Multiplier);
        _visualEffect.SetText($"<sprite=\"OtherSymbols\" name=\"{_bossLogic.CastDamageSymbolID}\">{abilityDamage}");
    }

    private async void UpdateVisualEffect()
    {
        await System.Threading.Tasks.Task.Delay(10);
        var abilityDamage = Mathf.CeilToInt(_bossLogic.Creature.Data.AbilityPower * _bossLogic.Multiplier);
        var healthDifference = _bossLogic.HealthOnCastStart - _bossLogic.Creature.Health.Current;
        var predictedDamage = Mathf.Clamp(abilityDamage - healthDifference, 0, abilityDamage);
        _visualEffect.SetText($"<sprite=\"OtherSymbols\" name=\"{_bossLogic.CastDamageSymbolID}\">{predictedDamage}");
    }

    public override void Exit()
    {
        _bossLogic.Creature.Health.OnChange -= UpdateVisualEffect;
        _bossLogic.SetCastTime(0);
        _bossLogic.SetCastedRecently(true);
    }

    private void CalculateCastTime()
    {
        var clips = _bossLogic.Creature.Animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Cast")
            {
                _castTime = clip.length;
            }
            else if (clip.name == "CastEnd")
            {
                _castEndTime = clip.length;
            }
        }
    }

    private IEnumerator Cast()
    {
        _bossLogic.Creature.SetTurnTime(_castTime + _castEndTime + 0.1f);
        _bossLogic.Creature.Animator.Play("Cast");
        yield return new WaitForSeconds(_castTime);
        var cellIndexes = _bossLogic.Creature.CurrentCell.CellIndexes;
        var cells = Field.Instance.Cells;
        var target = cells[cellIndexes.y, cellIndexes.x - 1].ContainedCreature;
        if (target != null)
        {
            var abilityDamage = Mathf.CeilToInt(_bossLogic.Creature.Data.AbilityPower * _bossLogic.Multiplier);
            var healthDifference = _bossLogic.HealthOnCastStart - _bossLogic.Creature.Health.Current;
            target.DealDamage(Mathf.Clamp(abilityDamage, 0, abilityDamage - healthDifference),
                _bossLogic.DamageType, _bossLogic.DamageSource);
            Debug.Log($"ability damage {abilityDamage} healthDiff {healthDifference}");
        }
        _bossLogic.ChangeState(new SecondBossMeleeAttack(_bossLogic));
        _visualEffect.Destroy();
    }

    public override void OnTurn()
    {
        if (_bossLogic.Creature.Data.Disabled || _bossLogic.Creature.AffectedByAny(StatusEffect.Stun)) { return; }

        _bossLogic.ChangeCastTime(+1);
        if (_bossLogic.CurrentCast >= _bossLogic.CastTime)
        {
            _bossLogic.Creature.StartCoroutine(Cast());
        }
    }

    protected override void CalculateState() { }

    public override void OnChange() 
    {
        _bossLogic.SetHealthOnCastStart(_bossLogic.Creature.Health.Current);
    }
}
