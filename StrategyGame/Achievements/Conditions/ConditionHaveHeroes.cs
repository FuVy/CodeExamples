using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Conditions/HaveHeroes")]
public class ConditionHaveHeroes : AchievementCondition
{
    [SerializeField]
    private HeroData[] _heroes;
    public override bool ConditionSatisfied()
    {
        foreach (var hero in _heroes)
        {
            if (!GameSession.Save.FightStats.ContainsKey($"USED_HERO_{hero.name}"))
            {
                return false;
            }
        }
        return true;
    }
}
