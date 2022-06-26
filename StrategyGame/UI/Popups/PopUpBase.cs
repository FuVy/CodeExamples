using UnityEngine;

public abstract class PopUpBase<T> : ScriptableObject where T : PopUpAction
{
    [SerializeField]
    private string _descriptionKey;
    public string DescriptionKey => _descriptionKey;

    [SerializeReference]
    protected T[] _actions;
    public T[] Actions => _actions;

    [SerializeReference]
    protected T[] _actionsOnButton;
    public T[] ActionsOnButton => _actionsOnButton;

    [SerializeReference]
    private Sprite _icon;
    public Sprite Icon => _icon;

    [SerializeField]
    private Vector2Int _position;
    public Vector2Int Position => _position;

    [SerializeField]
    private Vector2Int _size = new Vector2Int(600, 550);
    public Vector2Int Size => _size;

    [SerializeField]
    protected bool _hasOKButton = true;
    public bool HasOkButton => _hasOKButton;

    public abstract void Apply();

    public void ApplyButton()
    {
        foreach (PopUpAction action in _actionsOnButton)
        {
            action.Apply();
        }
    }
}
