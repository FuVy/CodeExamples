using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LocalizationChanger : MonoBehaviour
{
    private int _localesCount = 0;
    private int _currentIndex = 0;

    private void Start()
    {
        var initializeOperation = LocalizationSettings.SelectedLocaleAsync;
        if (initializeOperation.IsDone)
        {
            InitializeCompleted(initializeOperation);
        }
        initializeOperation.Completed += InitializeCompleted;
    }

    private void InitializeCompleted(AsyncOperationHandle obj)
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;
        if (PlayerPrefs.HasKey("Localization"))
        {
            var localeName = PlayerPrefs.GetString("Localization");
            _localesCount = locales.Count;
            var localeFound = false;
            for (int i = 0; i < _localesCount; i++)
            {
                if (locales[i].ToString() == localeName)
                {
                    LocalizationSettings.SelectedLocale = locales[i];
                    localeFound = true;
                    _currentIndex = i;
                    break;
                }
            }
            if (!localeFound)
            {
                SaveLocale();
            }
        }
        else
        {
            SaveLocale();
        }
        var CheckedlocaleName = PlayerPrefs.GetString("Localization");
        for (int i = 0; i < locales.Count; i++)
        {
            if (locales[i].ToString() == CheckedlocaleName)
            {
                LocalizationSettings.SelectedLocale = locales[i];
            }
        }
    }

    public void Change(int value)
    {
        _currentIndex += value;
        if (_currentIndex >= _localesCount)
        {
            _currentIndex = 0;
        }
        else if (_currentIndex < 0)
        {
            _currentIndex = _localesCount - 1;
        }
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_currentIndex];
    }

    public void ChangeScene(string sceneName)
    {
        SaveLocale();
        SceneManager.LoadScene(sceneName);
    }

    public void SaveLocale()
    {
        PlayerPrefs.SetString("Localization", LocalizationSettings.SelectedLocale.ToString());
    }
}
