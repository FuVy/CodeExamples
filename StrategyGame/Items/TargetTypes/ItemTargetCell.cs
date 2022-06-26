using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/TargetType/Cell")]
public class ItemTargetCell : ItemTargetType
{
    [SerializeField]
    private bool _requireEmptyCell = true;
    public override bool CanBeUsed()
    {
        var cell = CellSelector.Instance.SelectedCell;
        if (cell == null)
        {
            return false;
        }
        else if (_requireEmptyCell && cell.ContainedCreature != null)
        {
            return false;
        }
        return true;
    }

    public override Cell[] GetCellTargets()
    {
        var cell = new Cell[1] { CellSelector.Instance.SelectedCell };
        return cell;
    }

    public override Creature[] GetCreatureTargets()
    {
        return null;
    }
}
