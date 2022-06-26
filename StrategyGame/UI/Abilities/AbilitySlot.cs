using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using TMPro;

public class AbilitySlot : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private TMP_Text _description;

    public void Output(Ability ability, Creature creature)
    {
        _image.sprite = ability.Sprite;
        var fixedKey = ability.LocalizationID;

        var description = new LocalizedString("Abilities", fixedKey);
        ability.UpdateLocalizationArguments(creature);
        var args = ability.LocalizationArgs;
        description.Arguments = new object[] { args };
        _description.text = description.GetLocalizedString();
    }
}
