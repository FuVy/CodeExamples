using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pop-ups/MenuPopup/Actions/CreateNew")]
public class InitMenuPopup : MenuPopupAction
{
    [SerializeField]
    private MenuPopUp[] _popups;
    public override void Apply()
    {
        for (int i = 0; i < _popups.Length; i++)
        {
            MenuPopupHolder.Instance.InitPopup(_popups[i]);
        }
    }
}
