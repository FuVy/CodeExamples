using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public abstract class SecondBossState : BossState<SecondBoss>
{
    public SecondBossState(SecondBoss boss) : base(boss) { }

    public abstract void OnChange();
    public override void OnTurnEnd()
    {
        CalculateState();
    }
    public virtual void GenerateMobs()
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
                    pairs.Shuffle();
                    var ordered = pairs.OrderBy(x => x.Value).ToArray();
                    MobGenerator.Instance.Generate(mobPair.Mob, cells[ordered[0].Key, mobPair.X]);
                }
            }
        }
    }

    protected abstract void CalculateState();
}
