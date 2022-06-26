using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/TargetType/SelectedCreature")]
public class ItemTargetSelectedCreature : ItemTargetType
{
    [SerializeField]
    private bool _targetMonsters = true, _targetHeroes = true;

    public override bool CanBeUsed()
    {
        var targets = GetCreatureTargets();
        return (targets != null && targets.Length > 0);
    }

    public override Creature[] GetCreatureTargets()
    {
        var selectedCreature = CreatureSelector.Instance.SelectedCreature;
        if ((selectedCreature != null) &&
            ((selectedCreature is Monster && _targetMonsters == true) ||
            (selectedCreature is Hero && _targetHeroes == true)))
        {
            return new Creature[1] { selectedCreature };
        }
        else
        {
            return null;
        }
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