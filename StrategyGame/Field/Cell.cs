using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    [SerializeReference]
    private Creature _containedCreature;
    public Creature ContainedCreature => _containedCreature;

    [SerializeReference]
    private SpriteRenderer _renderer;
    public SpriteRenderer Renderer => _renderer;

    [field: SerializeField]
    public EffectsHolder EffectsHolder { get; private set; }

    private Vector2Int _cellIndexes;
    public Vector2Int CellIndexes => _cellIndexes;

    public List<Aura> Auras = new List<Aura>();

    [SerializeReference]
    private BoxCollider _collider;

    [SerializeReference]
    private SpriteRenderer _popupHighlight;
    [SerializeReference]
    private SpriteRenderer _highlight;

    public event UnityAction<Creature> OnCreatureStep;
    public event UnityAction OnTurnStart;

    public int Y => _cellIndexes.y;
    public int X => _cellIndexes.x;

    public void SetRendererStatus(bool enabledStatus) => _renderer.enabled = enabledStatus;
    public void SetContainedCreature(Creature creature, bool triggerChange = true)
    {
        _containedCreature = creature;
        if (_containedCreature != null && triggerChange)
        {
            OnCreatureStep?.Invoke(_containedCreature);
        }
    }
    public void SetCellIndexes(Vector2Int indexes) => _cellIndexes = indexes;
    public void SetColliderStatus(bool status) => _collider.enabled = status;
    public void SetPopupHighlightState(bool state) => _popupHighlight.enabled = state;
    public void SetHighlightColor(Color color) => _highlight.color = color;
    public void SetHighlightState(bool state) => _highlight.enabled = state;
    public void TriggerTurnStart() => OnTurnStart?.Invoke();
}
