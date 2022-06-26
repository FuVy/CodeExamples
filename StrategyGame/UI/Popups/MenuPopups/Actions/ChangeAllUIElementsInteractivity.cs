using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Pop-ups/MenuPopup/Actions/ChangeAllUIElementInteractivity")]
public class ChangeAllUIElementsInteractivity : MenuPopupAction
{
    [SerializeField]
    private bool _interactable = false;
    public override async void Apply()
    {
        await UniTask.Delay(10);
        var elements = UIElementsHolder.Instance.UIElements;
        foreach (var element in elements)
        {
            element.ChangeState(_interactable);
        }
    }
}
