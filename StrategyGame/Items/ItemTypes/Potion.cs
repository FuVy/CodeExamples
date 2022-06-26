using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Items/Potion")]
public class Potion : ItemData
{
    [SerializeField]
    protected int _duration = 1;
    [SerializeReference]
    protected AlchemistPotion _alchemistPotion;

    public override bool CheckItemTypeCondition()
    {
        return MobGenerator.Instance.Turn < 2;
    }

    public override void Use()
    {
        var targets = TargetType.GetCreatureTargets();
        var alchemist = targets.ToList().Find(x => x.Data.ClonelessName() == "Alchemist");
        PresetTargets preset = CreateInstance<PresetTargets>();
        if (alchemist != null 
            && _alchemistPotion != null
            && ItemBar.Instance.Upgrades.Contains(ItemBar.UpgradeTypes.AlchemistPotions))
        {
            var updatedList = targets.ToList();
            updatedList.Remove(alchemist);
            targets = updatedList.ToArray();
            var alchemistTarget = new Creature[1] { alchemist };
            preset.Set(alchemistTarget, new Cell[0]);
            var newPotion = Instantiate(_alchemistPotion);
            newPotion.SetTargetType(preset);
            newPotion.TryInit();
            newPotion.Save();
            newPotion.Apply();
        }
        preset.Set(targets, TargetType.GetCellTargets());
        Save();
        Use(preset);
    }
    public void Apply()
    {
        base.Use();
    }
}