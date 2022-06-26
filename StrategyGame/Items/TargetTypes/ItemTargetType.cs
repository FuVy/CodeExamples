using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemTargetType : ScriptableObject
{
    [SerializeField]
    private bool _requireTarget = true;
    public bool RequireTarget => _requireTarget;

    [SerializeField]
    private bool _overridable = true;
    public bool Overridable => _overridable;

    public Targets GetTargets()
    {
        var targets = new Targets()
        {
            Creatures = GetCreatureTargets(),
            Cells = GetCellTargets()
        };
        return targets;
    }

    public abstract Creature[] GetCreatureTargets();

    public virtual Cell[] GetCellTargets() => null;

    public abstract bool CanBeUsed();

    public struct Targets
    {
        public Creature[] Creatures;
        public Cell[] Cells;
    }
}

public class PresetTargets : ItemTargetType
{
    private Creature[] _targets;
    private Cell[] _cells;
    public override bool CanBeUsed()
    {
        return true;
    }

    public override Creature[] GetCreatureTargets()
    {
        return _targets;
    }

    public override Cell[] GetCellTargets()
    {
        return _cells;
    }

    public void Set(Creature[] targets, Cell[] cells)
    {
        _targets = targets;
        _cells = cells;
    }
}