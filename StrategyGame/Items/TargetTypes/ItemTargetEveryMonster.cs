using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/TargetType/EveryMonster")]
public class ItemTargetEveryMonster : ItemTargetType
{
    [SerializeField]
    private MonsterData.MonsterType[] _excludedTypes;

    public override bool CanBeUsed()
    {
        return true;
    }

    public override Creature[] GetCreatureTargets()
    {
        List<Monster> monsters = MobGenerator.Instance.CreatedCreatures;
        for (int i = monsters.Count - 1; i >= 0; i--)
        {
            if (_excludedTypes.Contains(((MonsterData)monsters[i].Data).Type))
            {
                monsters.RemoveAt(i);
            }
        }
        return monsters.ToArray();
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
