using UnityEngine;

[CreateAssetMenu(menuName = "Pop-ups/MenuPopup/Actions/Destroy")]
public class HideMenuPopup : MenuPopupAction, IRequest<MenuPopUp>
{
    private MenuPopUp _parent;
    public override void Apply()
    {
        _parent.Parent.Destroy();
    }

    public void SatisfyRequest(MenuPopUp requested)
    {
        _parent = requested;
    }
}
