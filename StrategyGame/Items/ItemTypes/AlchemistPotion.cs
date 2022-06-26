using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/AlchemistPotion")]
public class AlchemistPotion : Potion
{
    protected override void Save()
    {
        var alchemistPotion = new Potion.SaveData();
        alchemistPotion.PotionName = name.RemoveCloneInString();
        alchemistPotion.Targets = new string[1] { "Alchemist" };
        alchemistPotion.RemoveLocationIndex = GameSession.LocationID + _duration;
        TurnSaver.Instance.Potions.Add(alchemistPotion);
    }
    private void OnValidate()
    {
        _alchemistPotion = null;
    }
}
