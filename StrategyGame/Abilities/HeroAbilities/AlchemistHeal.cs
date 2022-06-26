using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Abilities/Specific/Alchemist/Heal")]
public class AlchemistHeal : HeroAbility, IUpgradeableAbility
{
    [Header("SelfHeal")]
    [SerializeReference]
    private AlchemistHealBuff _buff;
    
    [ReadOnly]
    [SerializeField]
    [Header("Duration:<value>")]
    private int _additionalDuration = 0;
    [ReadOnly]
    [SerializeField]
    [Header("HealValue:<value>")]
    private int _healValue = 0;
    
    
    private bool _;
    private List<string> _upgrades = new List<string>();

    public void EnableUpgrade(string name)
    {
        string[] upgrades = name.Split('/');
        foreach (string upgrade in upgrades)
        {
            if (upgrade == "SelfHeal")
            {
                _upgrades.Add(upgrade);
            }
            else if (upgrade == "Buff")
            {
                _upgrades.Add(upgrade);
            }
            else
            {
                string[] subs = upgrade.Split(':');
                if (subs[0] == "Duration")
                {
                    _additionalDuration += int.Parse(subs[1]);
                }
                else if (subs[0] == "HealValue")
                {
                    _healValue += int.Parse(subs[1]);
                }
            }
        }
    }

    public override void UpdateLocalizationArguments(Creature creature)
    {
        LocalizationArgs = new Dictionary<string, string>();
        LocalizationArgs.Add("HealValue", _buff.OverallHeal.ToString());
    }

    protected override void Action()
    {
        if (_upgrades.Contains("SelfHeal"))
        {
            Apply(_creature);
        }
        var heroes = HeroGenerator.Instance.CreatedCreatures.ToArray();
        Creature lowestHealthCreature = null;
        for (int i = 0; i < heroes.Length; i++)
        {
            if ((heroes[i].Health.Current < heroes[i].Health.Maximum) && (((lowestHealthCreature == null) || heroes[i].Health.Current < lowestHealthCreature?.Health.Current)))
            {
                lowestHealthCreature = heroes[i];
            }
        }

        if (lowestHealthCreature != null)
        {
            Apply(lowestHealthCreature);
        }
        else
        {
            for (int i = 0; i < heroes.Length; i++)
            {
                if (lowestHealthCreature == null || heroes[i].Health.Current < lowestHealthCreature?.Health.Current)
                {
                    lowestHealthCreature = heroes[i];
                }
            }
            Apply(lowestHealthCreature);
        }
    }

    private void Apply(Creature target)
    {
        target.Health.Change(Mathf.CeilToint(_creature.Data.AbilityPower * _multiplier), true);
        if (_upgrades.Contains("Buff"))
        {
            var buff = target.ApplyBuff(_buff, _creature);
            buff.ChangeDuration(_additionalDuration);
            buff.ChangeAdditionalHeal(_healValue);
        }
    }
}
