using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SecondBossMeleeAttack : SecondBossState
{
    public SecondBossMeleeAttack(SecondBoss boss) : base(boss) { }

    public override void Enter()
    {
        _bossLogic.Creature.StartCoroutine(ChangeAnimator());
        _bossLogic.Creature.Data.SetBaseRange(1);
        _bossLogic.Creature.SetAttackType(new MoveOrAttack());
        _bossLogic.ChangeLocalizationID("SecondBossMelee");
        _bossLogic.Creature.Data.SetDamage(_bossLogic.MeleeBaseDamage);
    }

    private IEnumerator ChangeAnimator()
    {
        yield return new WaitForSeconds(1f);
        _bossLogic.Creature.SetAnimator(_bossLogic.MeleeAnimator);
    }

    public override void Exit()
    {
        _bossLogic.Creature.SetAttackType(null);
    }

    public override void OnChange() { }

    public override void OnTurn() { }

    protected override void CalculateState()
    {
        var currentHealth = _bossLogic.Creature.Health.Current;
        if (_bossLogic.Creature.CurrentCell.CellIndexes.x == 2 && !_bossLogic.CastedRecently)
        {
            _bossLogic.ChangeState(new SecondBossMeleeCast(_bossLogic));
        }
        else if (currentHealth.Between(26, 50))
        {
            _bossLogic.ChangeState(new SecondBossFlyAwayState(_bossLogic));
        }
    }

    public override void GenerateMobs()
    {
        var mobsCount = MobGenerator.Instance.CreatedCreatures.FindAll(x => x.ClonelessName() != _bossLogic.BossMinion.name).Count;
        int maxGenerateCount = 0;
        var stateMobs = _bossLogic.StateMobs.Find(x => x.StateName == _bossLogic.State.ToString());
        maxGenerateCount = stateMobs.MaxSpawnQuantity;
        if (mobsCount < maxGenerateCount)
        {
            if (stateMobs.Mobs.Length < 0 || stateMobs.SpawnQuantity == 0) { return; }
            var spawnQuantity = 1;
            if (stateMobs.StateName != null)
            {
                spawnQuantity = stateMobs.SpawnQuantity;
            }
            List<int> yIndexes = new List<int>() { 0, 1, 2 };
            yIndexes.Shuffle();
            var cells = Field.Instance.Cells;
            var boss = _bossLogic.Creature;
            var bossY = boss.CurrentCell.CellIndexes.y;

            spawnQuantity = Mathf.Clamp(spawnQuantity, 0, maxGenerateCount - mobsCount);
            for (int i = 0; i < spawnQuantity; i++)
            {
                List<KeyValuePair<int, int>> pairs = new List<KeyValuePair<int, int>>();
                var mobPair = stateMobs.Mobs.Random();
                for (int y = 0; y < 3; y++)
                {
                    if (cells[y, mobPair.X].ContainedCreature != null) { continue; }
                    int quantity = 0;
                    for (int x = 2; x <= 4; x++)
                    {
                        var existingMob = cells[y, x].ContainedCreature;
                        if (existingMob != null && existingMob != boss)
                        {
                            quantity++;
                        }
                    }
                    pairs.Add(new KeyValuePair<int, int>(y, quantity));
                }
                if (pairs.Count > 0)
                {
                    if (cells[bossY, 2].ContainedCreature == null && cells[bossY, 3].ContainedCreature == null && cells[bossY, 4].ContainedCreature == null)
                    {
                        MobGenerator.Instance.Generate(mobPair.Mob, cells[bossY, mobPair.X]);
                        continue;
                    }
                    pairs.Shuffle();
                    var ordered = pairs.OrderBy(x => x.Value).ToArray();
                    MobGenerator.Instance.Generate(mobPair.Mob, cells[ordered[0].Key, mobPair.X]);
                }
            }
        }
    }
}
