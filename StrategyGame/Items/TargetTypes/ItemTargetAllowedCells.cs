using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/TargetType/AllowedCells")]
public class ItemTargetAllowedCells : ItemTargetType
{
    [SerializeField]
    private Vector2Int[] _allowedCells;

    [SerializeField]
    private bool _requireEmptyMainCell = false;
    public override bool CanBeUsed()
    {
        var cell = CellSelector.Instance.SelectedCell;
        if (cell == null)
        {
            return false;
        }
        if (!_allowedCells.Contains(cell.CellIndexes))
        {
            return false;
        }    
        if (_requireEmptyMainCell && cell.ContainedCreature != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override Creature[] GetCreatureTargets()
    {
        return null;
    }

    public override Cell[] GetCellTargets()
    {
        if (CanBeUsed())
        {
            var cells = new Cell[1];
            cells[0] = CellSelector.Instance.SelectedCell;
            return cells;
        }
        return null;
    }
}
