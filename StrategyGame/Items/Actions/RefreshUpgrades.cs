using System;
using UnityEngine;

[CreateAssetMenu(menuName = ("Items/Actions/RefreshUpgrades"))]
public class RefreshUpgrades : ItemAction
{
    public override void Apply(ItemTargetType.Targets targets)
    {
        UpgradeShop.Instance.GenerateSlots();
    }
}
