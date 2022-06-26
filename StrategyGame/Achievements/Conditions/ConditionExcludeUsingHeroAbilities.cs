using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Conditions/ExcludeHeroAbilities")]
public class ConditionExcludeUsingHeroAbilities : AchievementCondition
{
    [SerializeField]
    private HeroAbility[] _abilities;
    public override bool ConditionSatisfied()
    {
        foreach (var ability in _abilities)
        {
            if (GameSession.Save.FightStats.ContainsKey($"USED_ABILITY_{ability.name}"))
            {
                return false;
            }
        }
        return true;
    }
}
