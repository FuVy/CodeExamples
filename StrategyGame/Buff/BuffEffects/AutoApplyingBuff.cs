using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Buffs/AutoBuff")]
public class AutoApplyingBuff : Buff
{
    protected override void HandleTurns()
    {
        DebuffEffects();
        ApplyEffects();
        base.HandleTurns();
    }
}
