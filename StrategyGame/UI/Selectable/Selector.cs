using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Selector : MonoBehaviour
{
    [SerializeField]
    protected Vector3 _punchScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField]
    protected Image _transparentSelector, _mainSelector;
    [SerializeField]
    protected SelectableComponent _selectedSlot, _mouseOverSlot;
    [SerializeField]
    protected float _transitionSpeed = 0.3f;
    [SerializeField]
    private float _sizeMultiplier = 1f, _smallSized = 1f, _bigSized = 1.2f;

    protected DG.Tweening.Core.TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> _transparentSelectorFade;

    private void Awake()
    {
        _mainSelector.rectTransform.sizeDelta *= _sizeMultiplier;
        _transparentSelector.rectTransform.sizeDelta *= _sizeMultiplier;
    }

    private void Start()
    {
        if (_mainSelector == null) { return; }
        var sequence = DOTween.Sequence()
            .Append(_mainSelector.transform.DOScale(Vector3.one * _bigSized, 0.5f))
            .Append(_mainSelector.transform.DOScale(_smallSized, 0.5f));
        sequence.SetLoops(-1, LoopType.Restart);
    }

    public virtual void Select(SelectableComponent slot)
    {
        if (_selectedSlot == slot) { return; }
        _selectedSlot = slot;
        _mainSelector.transform.DOKill();
        _mainSelector.transform.DORotate(new Vector3(0f, 0f, slot.Rotation), 0.1f);
        _mainSelector.transform.DOMove(_selectedSlot.transform.position, _transitionSpeed);
        _mainSelector.transform.DOPunchScale(_punchScale, 0.2f);
    }

    public virtual void ChangePosition(SelectableComponent slot)
    {
        _transparentSelectorFade.Kill();
        _transparentSelectorFade = _transparentSelector.DOColor(new Color(1f, 1f, 1f, 0.3f), 0.3f); 
        if (_mouseOverSlot == slot) { return; }
        _mouseOverSlot = slot;
        _transparentSelector.transform.DOKill();
        _transparentSelector.transform.DOScale(1f, 0.5f);
        _transparentSelector.transform.DORotate(new Vector3(0f, 0f, slot.Rotation), 0.1f);
        _transparentSelector.transform.DOMove(slot.transform.position, _transitionSpeed);
    }

    public virtual void Exit()
    {
        _transparentSelectorFade.Kill();
        _transparentSelectorFade = _transparentSelector.DOColor(new Color(0f, 0f, 0f, 0f), 1.3f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            _mouseOverSlot = null;
        });
    }
}