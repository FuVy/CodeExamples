using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureAbility<T> : Ability where T : Creature
{
    [SerializeField]
    protected DamageType _damageType;
    [SerializeField]
    protected float _multiplier = 1f;

    protected T _creature;
    public T Creature => _creature;
    protected Cell[,] _cells;
    protected float _castTime;
    protected Coroutine _ability;

    public override void Init(MonoBehaviour mono)
    {
        if (Field.Instance != null)
        {
            _cells = Field.Instance.Cells;
        }
        _creature = (T)mono;
    }

    protected abstract void Action();

    protected virtual void SetTurnTime() => SetTurnTime(_creature);

    protected void SetTurnTime(Creature creature)
    {
        var clips = creature.Animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Cast")
            {
                _castTime = clip.length;
                break;
            }
        }
        _creature.SetTurnTime(_castTime);
    }

    protected virtual bool Castable()
    {
        return true;
    }

    protected virtual void TryCast()
    {
        _creature.SetCastingStatus(false);
        if (Castable())
        {
            Cast();
        }
        else
        {
            AlternativeAction();
        }
    }

    protected virtual void Cast()
    {
        _creature.TriggerCast();
        _creature.Play(SoundType.Cast);
        _creature.Play("Cast");
        var key = $"USED_ABILITY_{name.RemoveCloneInString()}";
        if (GameSession.Save.FightStats.TryGetValue(key, out var value))
        {
            GameSession.Save.FightStats[key] = (int)value + 1;
        }
        else
        {
            GameSession.Save.FightStats.Add(key, 1);
        }
        SetTurnTime();
        Action();
    }

    protected virtual void AlternativeAction()
    {
        _creature.AttackType?.Action();
    }

    public override void Sub()
    {
        Unsub();
        _creature.OnCast += TryCast;
    }

    public override void Unsub()
    {
        if (!_creature) { return; }
        _creature.OnCast -= TryCast;
        if (_ability != null)
        {
            _creature?.StopCoroutine(_ability);
            _ability = null;
        }
    }
}

public abstract class CreatureAbility : CreatureAbility<Creature>
{

}
