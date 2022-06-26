using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityOutput : MonoBehaviour
{
    [SerializeField]
    private Transform _container;

    [SerializeField]
    private AbilitySlot _prefab;

    private List<AbilitySlot> _currentAbilities = new List<AbilitySlot>();

    public void Output(Ability[] abilities, Creature creature)
    {
        Hide();
        gameObject.SetActive(true);
        foreach (Ability ability in abilities)
        {
            if (ability.ShowInInspector && ability.LocalizationID != null && ability.LocalizationID != string.Empty)
            {
                var newAbility = Instantiate(_prefab, _container);
                _currentAbilities.Add(newAbility);
                newAbility.Output(ability, creature);
            }
        }
    }

    public void Hide()
    {
        foreach (AbilitySlot slot in _currentAbilities)
        {
            Destroy(slot.gameObject);
        }
        gameObject.SetActive(false);
        _currentAbilities.Clear();
    }
}
