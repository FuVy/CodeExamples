using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Locations/LocationData")]
public class LocationData : ScriptableObject
{
    [SerializeField]
    private Sprite _environment;
    public Sprite Environment => _environment;

    [SerializeField]
    private Sprite _environmentForeground;
    public Sprite EnvironmentForeground => _environmentForeground;

    [SerializeField]
    private Vector2 _environmentOffset = new Vector2(0f, 180f);
    public Vector2 EnvironmentOffset => _environmentOffset;

    [field: SerializeField]
    public Vector2 FieldOffset { get; private set; }

    [field: SerializeField]
    public float CellsY { get; private set; } = 0f;

    [SerializeField]
    private int _enemiesToKill;
    public int EnemiesToKill => _enemiesToKill;

    [SerializeField]
    private int _goldReward;
    public int GoldReward => _goldReward;

    [SerializeField]
    private int _rewardRandomness = 0;
    public int RewardRandomness => _rewardRandomness;

    [SerializeField]
    private MonsterData _boss;
    public MonsterData Boss => _boss;

    [SerializeField]
    private Wave[] _waves;
    public Wave[] Waves => _waves;

    [SerializeField, Range(1, 9)]
    private int _minimumMobs = 2;
    public int MinimumMobs => _minimumMobs;

    [SerializeField, Range(1, 9)]
    private int _maximumMobs = 6;
    public int MaximumMobs => _maximumMobs;

    [SerializeField]
    private BattlePopup[] _popups;
    public BattlePopup[] PopUps => _popups;

    public bool TryGetWaveAt(int turn, out Wave wave)
    {
        wave = new Wave();
        wave.Turn = -10;
        var found = false;
        for (int i = 0; i < _waves.Length; i++)
        {
            if (_waves[i].Turn == turn)
            {
                wave = _waves[i];
                found = true;
                break;
            }
        }
        return found;
    }

    public MonsterData MonsterAt(int index)
    {
        List<MonsterData> monsters = new List<MonsterData>();
        foreach (Wave wave in _waves)
        {
            foreach (MonsterData data in wave.Monsters)
            {
                monsters.Add(data);
            }
        }

        if (index > monsters.Count - 1)
        {
            return monsters[monsters.Count - 1];
        }
        else
        {
            return monsters[index];
        }
    }

    [System.Serializable]
    public struct Wave
    {
        public int Turn;
        public MonsterData[] Monsters;
    }
}
