using UnityEngine;

[CreateAssetMenu(menuName = ("Items/Actions/GiveGold"))]
public class GiveGold : ItemAction
{
    [SerializeField]
    private int _value;

    public override void Apply(ItemTargetType.Targets targets)
    {
        GameSession.ChangeGold(_value);
    }
}
