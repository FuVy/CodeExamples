using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Locations/LevelData"), System.Serializable]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private DifficultyLocations[] _locations;
    public LocationData[] Locations => _locations[GameSession.Save.SelectedDifficulty].Locations;

    [SerializeField]
    private LevelData[] _levelsToOpen;
    public LevelData[] LevelsToOpen => _levelsToOpen;

    [SerializeField]
    private bool _initiallyOpen;
    public bool InitiallyOpen => _initiallyOpen;

    [SerializeField]
    private int _budgetLimit;
    public int BudgetLimit => _budgetLimit;

    [Header("Награды за уровень")]
    [SerializeField]
    private int _mithrilReward;
    public int MithrilReward => _mithrilReward;

    [SerializeField]
    private int _budgetReward;
    public int BudgetReward => _budgetReward;

    [SerializeField]
    private int _skillPointsReward;
    public int SkillPointsReward => _skillPointsReward;

    [SerializeField]
    private ItemData[] _itemsReward;
    public ItemData[] ItemsReward => _itemsReward;

    [SerializeField]
    private string[] _heroesReward;
    public string[] HeroesReward => _heroesReward;

    [SerializeField]
    private MenuTab.Type[] _uiElements;
    public MenuTab.Type[] UIElements => _uiElements;

    [field: SerializeField]
    public MenuPopUp.ScenePopupPair[] MenuPopupsToPlay { get; private set; }

    [field: SerializeField]
    public  OnWinAction[] ActionsOnWin { get; private set; }

    [field: SerializeField]
    public Achievement Achievement { get; private set; }

    [System.Serializable]
    private struct DifficultyLocations
    {
        public LocationData[] Locations;
    }
}
