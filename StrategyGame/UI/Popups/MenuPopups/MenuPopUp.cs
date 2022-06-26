using UnityEngine;
using Utilities;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Pop-ups/MenuPopup/Popup")]
public class MenuPopUp : PopUpBase<MenuPopupAction>, IRequest<SingleMenuPopup>
{
    [HideIf("HasOkButton")]
    [field: SerializeField]
    private float _selfDestroyTimer = 1f;
    public float SelfDestroyTimer => _selfDestroyTimer;
    
    [field: SerializeField]
    public int Priority { get; private set; } = 0;
    public SingleMenuPopup Parent { get; private set; }

    public override void Apply()
    {
        foreach (PopUpAction action in _actions)
        {
            action.Apply();
        }
    }

    public void InitActions()
    {
        for (int i = 0; i < _actions.Length; i++)
        {
            _actions[i] = Instantiate(_actions[i]);
            if (_actions[i] is IRequest<MenuPopUp> requestor)
            {
                requestor.SatisfyRequest(this);
            }
        }
        for (int i = 0; i < _actionsOnButton.Length; i++)
        {
            _actionsOnButton[i] = Instantiate(_actionsOnButton[i]);
            if (_actionsOnButton[i] is IRequest<MenuPopUp> requestor)
            {
                requestor.SatisfyRequest(this);
            }
        }

    }

    public void SatisfyRequest(SingleMenuPopup requested)
    {
        Parent = requested;
    }

    [System.Serializable]
    public struct ScenePopupPair
    {
        public string Scene;
        public string PopupName;
    }

    public void Destroy()
    {
        for (int i = 0; i < _actions.Length; i++)
        {
            Destroy(_actions[i]);
        }
        for (int i = 0; i < _actionsOnButton.Length; i++)
        {
            Destroy(_actionsOnButton[i]);
        }
    }
}