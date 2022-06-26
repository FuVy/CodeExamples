using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public static class CreatureExtensions
{
    public static int SharedEffectDuration(this Creature creature, StatusEffect effect) => creature.Effects[effect] + creature.TurnEffects[effect];

    public static int PredictDamage(this Creature creature, int value, DamageType damageType, float multiplier = 1f)
    {
        if (creature.AffectedBy(StatusEffect.Invincible) || creature.Data.FullyResistant(damageType))
        {
            return 0;
        }
        int damage = (int)((value - creature.Resistance(damageType)) * multiplier);
        if (damage < 0)
        {
            damage = 0;
        }
        damage = -creature.Health.PredictDamageAfterShields(-damage);
        return damage;
    }

    public static int PredictDamage(this Creature creature, int value, DamageType damageType, DamageSource damageSource)
    {
        if (damageSource == DamageSource.Attack)
        {
            if (creature.AffectedBy(StatusEffect.Evasion))
            {
                return 0;
            }
        }
        return creature.PredictDamage(value, damageType);
    }

    public static void SetEffectOverTime(this Creature creature, StatusEffect effect, int value)
    {
        creature.TurnEffects[effect] = value;
        creature.TriggerStatusEffectsChanged();
    }

    public static bool ImmuneTo(this Creature creature, StatusEffect effect)
    {
        return (creature.Data.BaseStatusEffectImmunities.Contains(effect) 
            || creature.Data.AdditionalStatusEffectImmunities.Contains(effect));
    }

    public static void ChangeEffectOverTime(this Creature creature, StatusEffect effect, int value)
    {
        ChangeEffectDuration(creature, creature.TurnEffects, effect, value);
    }

    public static void ChangeHitEffect(this Creature creature, StatusEffect effect, int value)
    {
        ChangeEffectDuration(creature, creature.Effects, effect, value);
    }

    private static void ChangeEffectDuration(Creature creature, Dictionary<StatusEffect, int> dictionary, StatusEffect effect, int value)
    {
        if (creature.ImmuneTo(effect)) { return; }
        if (creature is Monster)
        {
            if (((MonsterData)creature.Data).Type == MonsterData.MonsterType.Boss) { return; }
        }
        dictionary[effect] += value;
        creature.TriggerStatusEffectsChanged();
    }

    public static void SetHitEffect(this Creature creature, StatusEffect effect, int value)
    {
        creature.Effects[effect] = value;
        creature.TriggerStatusEffectsChanged();
    }

    public static int DealDamage(this Creature creature, int value, DamageType damageType, DamageSource damageSource)
    {
        if (creature.Data.FullyResistant(damageType))
        {
            creature.Health.Change(0, false);
            creature.Health.ShowHealthChange(LocalizationSettings.StringDatabase.GetLocalizedString("Battle", "FullResistance"), Color.yellow);
            return 0;
        }
        int damage = value - creature.Resistance(damageType);
        if (damage < 0)
        {
            damage = 0;
        }
        if ((damageSource == DamageSource.Dot) || (damageSource == DamageSource.Aura))
        {
            damage = Mathf.CeilToInt(Mathf.Clamp(damage, value * 0.5f, value * 1.5f));
        }
        return -creature.Health.Change(-damage);
    }

    public static bool AffectedByAny(this Creature creature, params StatusEffect[] effects)
    {
        foreach (StatusEffect effect in effects)
        {
            if (creature.Effects[effect] > 0 || creature.TurnEffects[effect] > 0)
            {
                return true;
            }
        }
        return false;
    }

    public static bool AffectedByAll(this Creature creature, params StatusEffect[] effects)
    {
        foreach (StatusEffect effect in effects)
        {
            if (creature.Effects[effect] < 0 && creature.TurnEffects[effect] < 0)
            {
                return false;
            }
        }
        return true;
    }

    public static T ApplyBuff<T>(this Creature creature, T buff, Creature caster) where T : Buff
    {
        var newBuff = Object.Instantiate(buff);
        newBuff.RemoveCloneInName();
        newBuff.SetCasterAP(caster.Data.AbilityPower);
        newBuff.Init(creature);
        return newBuff;
    }

    public static T ApplyShield<T>(this Creature creature, T shield) where T : Shield
    {
        var newShield = Object.Instantiate(shield);
        newShield.Init(creature.Health);
        newShield.RemoveCloneInName();
        creature.Health.Shields.Add(newShield);
        return newShield;
    }
}
