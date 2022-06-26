using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossActivatedState : SecondBossState
{
    public SecondBossActivatedState(SecondBoss boss) : base(boss) { }

    public override void Enter()
    {
        var creature = _bossLogic.Creature;
        creature.Animator.Play("Spawn");

        var mobs = MobGenerator.Instance.CreatedCreatures;
        for (int i = mobs.Count - 1; i >= 0; i--)
        {
            if (mobs[i].Data != creature.Data)
            {
                mobs[i].CurrentCell?.SetContainedCreature(null, false);
                Object.Destroy(mobs[i].gameObject);
                MobGenerator.Instance.CreatedCreatures.RemoveAt(i);
            }
        }

        var cells = Field.Instance.Cells;
        var spawnX = _bossLogic.SpawnX;
        var bossCells = new Cell[]
            {
                cells[0, spawnX],
                cells[1, spawnX],
                cells[2, spawnX]
            };
        bossCells.Shuffle();
        creature.ChangeCell(bossCells[0], false);

        var bossMinion = _bossLogic.BossMinion;
        var firstMinion = MobGenerator.Instance.GenerateCreature(bossMinion, true);
        var secondMinion = MobGenerator.Instance.GenerateCreature(bossMinion, true);
        firstMinion.ChangeCell(bossCells[1], false);
        firstMinion.Init();
        secondMinion.ChangeCell(bossCells[2], false);
        secondMinion.Init();
        
        _bossLogic.SkipSpawn();
        _bossLogic.ChangeState(new SecondBossRangedAttack(_bossLogic));
    }

    public override void OnTurn() { }

    public override void Exit() { }

    public override void OnChange() { }
    protected override void CalculateState() { }
}
