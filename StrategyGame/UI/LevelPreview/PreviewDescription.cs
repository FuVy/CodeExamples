using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewDescription : MonoBehaviour
{
    [SerializeField]
    private Image _mapPreview;
    [SerializeField]
    private TMPro.TMP_Text _mithrilReward, _budgetReward, _skillPointsReward;

    private void Awake()
    {
        InitMap();
        InitRewards();
    }

    private void InitMap()
    {
        var level = GameSession.Level;
        var length = level.Locations.Length;
        _mapPreview.sprite = GameSession.Level.Locations[length - 1].Environment;
    }

    private void InitRewards()
    {
        var level = GameSession.Level;
        if (GameSession.Save.CompletedLevels.Contains(GameSession.Level.name))
        {
            _mithrilReward.text = "0";
            _budgetReward.text = "0";
            _skillPointsReward.text = "0";
        }
        else
        {
            _mithrilReward.text = $"{level.MithrilReward}";
            _budgetReward.text = $"{level.BudgetReward}";
            _skillPointsReward.text = $"{level.SkillPointsReward}";
        }
    }
}
