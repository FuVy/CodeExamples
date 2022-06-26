using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Shield : ScriptableObject
{
    [SerializeField]
    protected int _priority = 8;
    public int Priority => _priority;
    [field: SerializeField]
    public bool Saveable { get; private set; } = true;
    [field: SerializeField]
    public ShieldGroup Group { get; private set; }
    [field: SerializeField]
    public ShieldsEffectApply ApplyType { get; protected set; }

    public Health Health { get; protected set; }

    public UnityAction OnRemoval;
    public UnityAction<string> OnStateChange;

    public void Init(Health health)
    {
        Health = health;
        ApplyType?.Apply(this);
        Init();
    }
    public abstract void Init();
    public abstract int CalculateAppliedDamage(int damage);
    public abstract int PredictedDamage(int damage);
    public abstract Dictionary<string, string> LocalizationParams(Creature caster);
    protected virtual void Remove()
    {
        OnRemoval?.Invoke();
    }

    protected void Clear() => Health?.Shields.Remove(this);

    public virtual void TriggerStateUpdate()
    {
        OnStateChange?.Invoke("");
    }

    public virtual void Set(Dictionary<string, object> settings) { }
    public virtual Dictionary<string, object> Settings() { return null;  }

    public void SetPriority(int priority) => _priority = priority;

    public void Destroy()
    {
        Remove();
        Clear();
        Destroy(this);
    }
    private void OnDestroy()
    {
        Clear();
    }

    [System.Serializable]
    public struct SaveData
    {
        public string Name;
        public string Type;
        public Dictionary<string, object> Settings;
    }
}