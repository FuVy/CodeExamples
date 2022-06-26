using UnityEngine;

[CreateAssetMenu(menuName = "Items/Scroll")]
public class Scroll : ItemData
{
    public override bool CheckItemTypeCondition()
    {
        return true;
    }
}
