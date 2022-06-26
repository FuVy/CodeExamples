using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Specific/DemonHunter/LaneAoe")]
public class HuntLaneAoe : HeroDecreasingAoe, IUpgradeableAbility
{
    [Header("Buffs")]
    [SerializeReference]
    private Buff[] _buffs;

    private List<string> _enabledUpgrades = new List<string>();
    
    public override void UpdateLocalizationArguments(Creature creature)
    {
        LocalizationArgs = new Dictionary<string, string>();
        LocalizationArgs.Add("DamageValue", Mathf.CeilToInt(_multiplier * _creature.Data.AbilityPower).ToString());
    }

    protected override void ApplyDamage(int damage, Creature target)
    {
        base.ApplyDamage(damage, target);
        if (_enabledUpgrades.Contains("Buffs"))
        {
            for (int i = 0; i < _buffs.Length; i++)
            {
                foreach (Buff effect in _buffs)
                {
                    var buff = Instantiate(effect);
                    buff.name = Extensions.RemoveCloneInName(buff.name);
                    buff.SetCasterAP(_creature.Data.AbilityPower);
                    buff.Init(target);
                }
            }
        }
    }
    public void EnableUpgrade(string name)
    {
        if (!_enabledUpgrades.Contains(name))
        {
            _enabledUpgrades.Add(name);
        }
    }
}
