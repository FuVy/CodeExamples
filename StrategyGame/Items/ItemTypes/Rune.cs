using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Rune")]
public class Rune : ItemData
{
    public override bool CheckItemTypeCondition()
    {
        bool interactable = ActionConditionsSatisfied() 
            && RunesHolder.Instance.gameObject.activeInHierarchy;
        return interactable;
    }
}
