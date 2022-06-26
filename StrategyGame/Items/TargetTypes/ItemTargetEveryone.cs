using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/TargetType/Everyone")]
public class ItemTargetEveryone : ItemTargetType
{
    public override bool CanBeUsed()
    {
        return true;
    }

    public override Creature[] GetCreatureTargets()
    {
        List<Creature> creatures = new List<Creature>();
        creatures.Add(HeroGenerator.Instance.CreatedCreatures.ToArray());
        creatures.Add(MobGenerator.Instance.CreatedCreatures.ToArray());
        var boss = Field.Instance.Boss;
        if (boss != null)
        {
            creatures.Add(boss);
        }
        return creatures.ToArray();
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
