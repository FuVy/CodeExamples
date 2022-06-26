using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableComponent : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler
{
    [field: SerializeField]
    public float Rotation;
    [SerializeField]
    private Selector _selector;

    public void SetSelector(Selector selector) => _selector = selector;

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == 0 &&
            !eventData.dragging)
        {
            _selector.Select(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _selector.ChangePosition(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _selector.Exit();
    }
}
