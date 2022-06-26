using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Treasure")]
public class Treasure : ItemData
{
    public override bool CheckItemTypeCondition()
    {
        return WinHandler.Instance.WinState;
    }
}
