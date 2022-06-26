using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/TargetType/EveryHero")]
public class ItemTargetEveryHero : ItemTargetType
{
    public override bool CanBeUsed()
    {
        return true;
    }

    public override Creature[] GetCreatureTargets()
    {
        return HeroGenerator.Instance.CreatedCreatures.ToArray();
    }

    public override Cell[] GetCellTargets()
    {
        var creatures = GetCreatureTargets();
        var cells = new List<Cell>();
        foreach (Creature creature in creatures)
        {
            cells.Add(creature.CurrentCell);
        }
        return cells.ToArray();
    }
}
