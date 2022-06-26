using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobAbility : CreatureAbility<Monster>
{
    protected override void Cast()
    {
        base.Cast();
        ResetCastTime();
    }

    public void ResetCastTime()
    {
        _creature.SetCastingStatus(false);
        ((MonsterData)_creature.Data).SetTurnAfterCast(-1);
    }
}
