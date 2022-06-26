using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Conditions/CompleteBeforeXTurns")]
public class ConditionTurns : AchievementCondition
{
    [SerializeField]
    private int _value;
    public override bool ConditionSatisfied()
    {
        if (GameSession.Save.FightStats.TryGetValue("SPENT_TURNS", out var value))
        {
            if ((int)value < _value)
            {
                return true;
            }
        }
        return false;
    }
}
