using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/TargetType/SavedTarget")]
public class ItemTargetSaved : ItemTargetType
{
    [SerializeReference]
    private string[] _creatures;

    public override bool CanBeUsed()
    {
        return true;
    }

    public override Creature[] GetCreatureTargets()
    {
        List<Creature> targets = new List<Creature>();
        for (int i = 0; i < _creatures.Length; i++)
        {
            var hero = HeroGenerator.Instance.CreatedCreatures.Find(x => x.ClonelessName() == _creatures[i]);
            if (hero != null)
            {
                targets.Add(hero);
            }
        }
        return targets.ToArray();
    }

    public override Cell[] GetCellTargets()
    {
        var targets = GetCreatureTargets();
        List<Cell> cells = new List<Cell>();
        foreach (Creature target in targets)
        {
            cells.Add(target.CurrentCell);
        }
        return cells.ToArray();
    }

    public void SetCreatures(string[] creatures) => _creatures = creatures;
}
