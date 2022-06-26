using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class HeroSelector : Selector
{
    public List<HeroData> Heroes { get; private set; } = new List<HeroData>();
    private int _maxHeroes = 4;
    private int _currentQuantity = 0;

    public static HeroSelector Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        Heroes.Clear();
    }

    public bool TryAdding(HeroData creature)
    {
        if (_currentQuantity < _maxHeroes)
        {
            Heroes.Add(creature);
            _currentQuantity++;
            return true;
        }
        return false;
    }

    public void Remove(HeroData creature)
    {
        Heroes.Remove(creature);
        _currentQuantity--;
        if (_currentQuantity < 0)
        {
            _currentQuantity = 0;
        }
    }

    public async void LoadBattle()
    {
        await LimitItems();

        if (_currentQuantity == _maxHeroes)
        {
            GameSession.Save.FightStats.Clear();
            GameSession.Save.FightStats.Add("SPENT_CURRENCY", 0);
            for (int i = 0; i < Heroes.Count; i++)
            {
                GameSession.Save.FightStats.Add($"USED_HERO_{Heroes[i].name}", true);
                Heroes[i] = Instantiate(Heroes[i]);
            }
            GameSession.SetHeroes(Heroes.ToArray());
            gameObject.SetActive(false);
            GameSession.Save.Potions.Clear();

            SceneManager.LoadScene("BattleScene");
        }
    }

    private async Task LimitItems()
    {
        var items = GameSession.Save.Items;
        if (items == null) { return; }
        var config = await UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<Config>("Config").Task;
        var limit = config.BackpackLevels[GameSession.Save.Upgrades[UpgradeType.Backpack]].SlotsQuantity;
        if (items.Length > limit)
        {
            GameSession.Items.RemoveRange(limit, items.Length - limit);
            System.Array.Resize(ref items, limit);
            GameSession.Save.SetItems(items);
        }
    }

    private void OnDisable()
    {
        Heroes.Clear();
    }

    public void Return()
    {
        SceneManager.LoadScene("MapScene");
    }

    public override void Select(SelectableComponent selectable)
    {
    }

    public override void ChangePosition(SelectableComponent selectable)
    {
        _transparentSelectorFade.Kill();
        _transparentSelectorFade = _transparentSelector.DOColor(new Color(1f, 1f, 1f, 0.3f), 0.3f);
        if (_mouseOverSlot == selectable) { return; }
        _mouseOverSlot = selectable;
        _transparentSelector.transform.DOKill();
        _transparentSelector.transform.DOScale(1f, 0.5f);
        _transparentSelector.transform.DORotate(new Vector3(0f, 0f, selectable.Rotation), 0.1f);
        _transparentSelector.transform.DOMove(selectable.transform.position, _transitionSpeed);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}