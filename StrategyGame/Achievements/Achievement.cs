using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Achievement")]
public class Achievement : ScriptableObject
{
    [field: SerializeField]
    public Sprite Sprite { get; private set; }
    [field: SerializeField]
    public AchievementCondition[] Conditions { get; private set; }

    public bool ConditionsSatisfied()
    {
        foreach (var condition in Conditions)
        {
            if (!condition.ConditionSatisfied())
            {
                return false;
            }
        }
        return true;
    }
}
