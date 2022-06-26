using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewDifficultySelector : MonoBehaviour
{
    [field: SerializeField]
    public int Difficulty { get; private set; } = 0;
    [field: SerializeField]
    public Image Icon { get; private set; }
    [SerializeField]
    private Image _shadow;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private PreviewDifficultySelector[] _otherButtons;

    private void Awake()
    {
        _button.onClick.AddListener(Select);
        if (Difficulty == GameSession.Save.SelectedDifficulty)
        {
            Select();
        }
    }

    private void Select()
    {
        _shadow.enabled = false;
        GameSession.Save.SetDifficulty(Difficulty);
        foreach (var holder in _otherButtons)
        {
            holder.Deselect();
        }
    }

    public void Deselect()
    {
        _shadow.enabled = true;
    }
}
