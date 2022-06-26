using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pop-ups/MenuPopup/Actions/ChangeUIElementInteractivity")]
public class ChangeUIElementInteractivity : MenuPopupAction
{
    [SerializeField]
    private bool _interactable = false;
    [SerializeField]
    private string _name;
    public override async void Apply()
    {
        await UniTask.Delay(10);
        var element = UIElementsHolder.Instance.UIElements.Find(x => x.Name == _name);
        if (element != null)
        {
            element.ChangeState(_interactable);
        }
    }
}
