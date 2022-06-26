using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Items/TargetType/SelectedCells")]
public class ItemTargetCells : ItemTargetType
{
    [SerializeField]
    private bool _presetCells = false;

    [SerializeField]
    private Vector2Int[] _cells = { Vector2Int.zero };

    [HideIf("_presetCells")]
    [SerializeField]
    private bool _requireEmptyMainCell = false;

    [SerializeField]
    private bool _targetMonsters = true;

    [SerializeField]
    private bool _targetHeroes = true;

    public override bool CanBeUsed()
    {
        var cell = CellSelector.Instance.SelectedCell;
        if (cell == null)
        {
            return false;
        }
        else if (_requireEmptyMainCell && cell.ContainedCreature != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override Cell[] GetCellTargets()
    {
        var cells = Field.Instance.Cells;
        var list = new List<Cell>();
        Vector2Int vector;
        Vector2Int selectedCellOffset = Vector2Int.zero;
        if (!_presetCells)
        {
            var selectedCell = CellSelector.Instance.SelectedCell;
            if (_requireEmptyMainCell && selectedCell.ContainedCreature != null)
            {
                return null;
            }
            selectedCellOffset = selectedCell.CellIndexes;
        }
        for (int i = 0; i < _cells.Length; i++)
        {
            vector = _cells[i] + selectedCellOffset;
            if (vector.x >= 0 && vector.x < cells.GetLength(1) && vector.y >= 0 && vector.y < cells.GetLength(0))
            {
                list.Add(cells[vector.y, vector.x]);
            }
        }
        return list.ToArray();
    }

    public override Creature[] GetCreatureTargets()
    {
        List<Creature> targetList = new List<Creature>();
        var cellTargets = GetCellTargets();
        for (int i = 0; i < cellTargets.Length; i++)
        {
            var containedCreature = cellTargets[i].ContainedCreature;
            if (containedCreature != null)
            {
                if ((containedCreature is Monster && _targetMonsters) || (containedCreature is Hero && _targetHeroes))
                {
                    targetList.Add(containedCreature);
                }
            }
        }
        return targetList.ToArray();
    }

    private void OnValidate()
    {
        if (_cells == null || _cells.Length < 1)
        {
            System.Array.Resize(ref _cells, 1);
        }
        if (_presetCells)
        {
            _requireEmptyMainCell = false;
        }
    }
}