using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState<T> where T : Passive
{
    public BossState(T boss) => _bossLogic = boss;
    protected T _bossLogic;
    public abstract void Enter();
    public abstract void Exit();
    public abstract void OnTurn();
    public virtual void OnCreatureTurnEnd() { }
    public virtual void OnTurnEnd() { }
}
