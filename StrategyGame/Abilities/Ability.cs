using UnityEngine;
using System.Collections.Generic;
using Guards.Abilities;
public abstract class Ability : ScriptableObject
{
    #if UNITY_EDITOR
    [TextArea(3, 8), SerializeField]
    protected string _description;
    #endif

    [SerializeField]
    private bool _showInUnitInspector = false;
    public bool ShowInInspector => _showInUnitInspector;
    [field: SerializeField]
    public string LocalizationID { get; protected set; }
    [SerializeField]
    private Sprite _sprite;
    public Sprite Sprite => _sprite;

    public Dictionary<string, string> LocalizationArgs = new Dictionary<string, string>();
    public virtual void UpdateLocalizationArguments(Creature creature) { }

    public abstract void Init(MonoBehaviour mono);
    public abstract void Sub();
    public abstract void Unsub();
    public void Destroy()
    {
        Unsub();
        Destroy(this);
    }
    private void OnDestroy()
    {
        Unsub();
    }

    public virtual void Set(AbilitySettings settings) { }
    public virtual AbilitySettings Settings() { return null;  }
    public void ChangeLocalizationID(string id) => LocalizationID = id;
}
