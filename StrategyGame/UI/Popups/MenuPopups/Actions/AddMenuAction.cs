using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pop-ups/MenuPopup/Actions/AddMenuPopup")]
public class AddMenuAction : MenuPopupAction
{
    [SerializeField]
    private MenuPopUp.ScenePopupPair[] _pairs;
    public override void Apply()
    {
        foreach (var pair in _pairs)
        {
            GameSession.Save.MenuPopupsToShow.Add(pair);
        }
        SaveLoader.Save(GameSession.Save);
    }
}
