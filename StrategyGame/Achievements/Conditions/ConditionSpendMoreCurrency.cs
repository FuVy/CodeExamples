using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Conditions/SpendMoreCurrency")]
public class ConditionSpendMoreCurrency : AchievementCondition
{
    [SerializeField]
    private int _value;

    public override bool ConditionSatisfied()
    {
        if (GameSession.Save.FightStats.TryGetValue("SPENT_CURRENCY", out var value))
        {
            if ((int)value > _value)
            {
                return true;
            }
        }
        return false;
    }
}
