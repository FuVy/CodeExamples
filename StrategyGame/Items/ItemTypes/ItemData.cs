using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;
using System;

public abstract class ItemData : ScriptableObject
{
    [SerializeField]
    private int _cost;
    public int Cost => _cost;

    [SerializeField]
    [ShowAssetPreview]
    private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeReference]
    private ItemAction[] _actions;
    public ItemAction[] Actions => _actions;

    [SerializeField]
    private ItemTargetType _targetType;
    public ItemTargetType TargetType => _targetType;

    [field: SerializeField]
    public string NameID { get; private set; } = "Test";
    [field: SerializeField]
    public string DescriptionID { get; private set; } = "Test";

    public virtual void TryInit()
    {
        for (int i = 0; i < _actions.Length; i++)
        {
            Array.Resize(ref _actions, _actions.Length);
            _actions[i] = Instantiate(_actions[i]);
            _actions[i].RemoveCloneInName();
            if (_actions[i] is IInitializableItemAction initializable)
            {
                initializable.Init();
            }
            if (_actions[i] is IRequest<ItemData> ir)
            {
                ir.SatisfyRequest(this);
            }
        }
    }

    public void CheckItem(ItemHolder holder)
    {
        holder.Button.interactable = CheckItemTypeCondition() && ItemBar.Instance.ItemsUsable;
    }

    public abstract bool CheckItemTypeCondition();

    protected void Use(ItemTargetType targetType)
    {
        Use(targetType, _actions);
    }

    public virtual void Use()
    {
        Use(_targetType);
    }

    protected void Use(ItemTargetType targetType, ItemAction[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Apply(targetType.GetTargets());
        }
    }

    protected bool ActionConditionsSatisfied()
    {
        for (int i = 0; i < _actions.Length; i++)
        {
            if (_actions[i] is IHaveAdditionalCondition actionWithCondition)
            {
                if (!actionWithCondition.AdditionalConditionSatisfied())
                {
                    return false;
                }
            }
        }
        return true;
    }

    public virtual bool CanBeUsed()
    {
        if (ItemBar.Instance.ItemsUsable == false)
        {
            return false;
        }
        return _targetType.CanBeUsed();
    }

    public void SetTargetType(ItemTargetType type) => _targetType = type;

    private void OnDestroy()
    {
        ClearActions();
    }

    protected void ClearActions()
    {
        for (int i = 0; i < _actions.Length; i++)
        {
            Destroy(_actions[i]);
        }
    }

    public enum Target
    { 
        None,
        Creature,
        Monster,
        Hero,
        Cell
    }
}