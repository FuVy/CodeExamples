using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Conditions/ExcludeHeroes")]
public class ConditionExcludeHeroes : AchievementCondition
{
    [SerializeField]
    private HeroData[] _heroes;
    public override bool ConditionSatisfied()
    {
        foreach (var hero in _heroes)
        {
            if (GameSession.Save.FightStats.ContainsKey($"USED_HERO_{hero.name}"))
            {
                return false;
            }
        }
        return true;
    }
}
