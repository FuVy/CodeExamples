using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class PreviewCompletedMode : MonoBehaviour
{
    [SerializeField]
    private PreviewDifficultySelector[] _difficulties;
    [SerializeField]
    private Image _modeIcon, _difficulty, _achievement;
    [SerializeField]
    private TMPro.TMP_Text _completedLabel;

    private void Awake()
    {
        InitAchievementIcon();
        if (GameSession.Save.HighestCompletedDifficulty.ContainsKey(GameSession.Level.name))
        {
            _completedLabel.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI", "Completed");
            var difficulty = GameSession.Save.HighestCompletedDifficulty[GameSession.Level.name];
            _difficulty.sprite = _difficulties.Find(x => x.Difficulty == difficulty).Icon.sprite;
        }
        else
        {
            _completedLabel.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI", "NotCompleted");
            _modeIcon.enabled = false;
            _difficulty.enabled = false;
        }
    }

    private void InitAchievementIcon()
    {
        if (GameSession.Level.Achievement != null)
        {
            _achievement.sprite = GameSession.Level.Achievement.Sprite;
            if (GameSession.Save.CompletedAchievements.Contains(GameSession.Level.Achievement.name))
            {
                _achievement.color = new Color(1f, 1f, 1f, 0.3f);
            }
            else
            {
                _achievement.color = Color.white;
            }
        }
        else
        {
            _achievement.gameObject.SetActive(false);
        }
    }
}
