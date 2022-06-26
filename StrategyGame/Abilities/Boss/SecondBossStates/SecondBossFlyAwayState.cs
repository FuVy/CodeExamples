using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossFlyAwayState : SecondBossState
{
    public SecondBossFlyAwayState(SecondBoss boss) : base(boss) { }

    public override void Enter() { }

    public override void Exit() { }

    public override void OnChange() 
    {
        var cells = Field.Instance.Cells;
        var spawnX = _bossLogic.SpawnX;
        List<int> yIndexes = new List<int>() { 0, 1, 2 };
        var oldY = _bossLogic.Creature.CurrentCell.CellIndexes.y;
        yIndexes.Remove(oldY);
        var newY = yIndexes.Random();
        _bossLogic.Creature.ChangeCell(cells[newY, spawnX], true);
        yIndexes.Remove(newY);
        yIndexes.Add(oldY);
        _bossLogic.SetTurnsSinceLastCopySummon(0);
        var bossMinion = _bossLogic.BossMinion;
        for (int i = 0; i < yIndexes.Count; i++)
        {
            var cell = cells[yIndexes[i], spawnX];
            if (cell.ContainedCreature == null)
            {
                var minion = MobGenerator.Instance.GenerateCreature(bossMinion, true);
                minion.ChangeCell(cell, false);
                minion.Init();
            }
        }

        _bossLogic.ChangeState(new SecondBossRangedAttack(_bossLogic));
    }

    public override void OnTurn() { }

    protected override void CalculateState() { }
}
