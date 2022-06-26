using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LevelHolder : MonoBehaviour
{
    [field: SerializeField]
    public LevelData Data { get; private set; }
    [SerializeField]
    private Color _availableColor, _completedColor, _unavailableColor;
    
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Button _button;

    private bool _available;

    private void Start()
    {
        _image.alphaHitTestMinimumThreshold = 0.1f;
        _button.onClick.AddListener(ChooseLevel);
        if (Data != null)
        {
            if (Data.InitiallyOpen)
            {
                SetAsAvailable();
            }
        }
    }

    public void ChooseLevel()
    {
        if (_available)
        {
            GameSession.SetCurrentLevel(Data);
            SceneManager.LoadScene("HeroSelection");
        }
        else
        {
            MapHandler.Instance.ShowLevelLockedMessage(transform.position);
        }
    }

    private void OnMouseDown()
    {
        ChooseLevel();
    }

    public void SetAsAvailable()
    {
        if (_available)
        {
            return;
        }
        else
        {
            _available = true;
            _image.color = _availableColor;
        }
    }

    public void SetAsUnavailable()
    {
        _image.color = _unavailableColor;
    }

    public void SetAsCompleted()
    {
        _available = true;
        _image.color = _completedColor;
    }
}