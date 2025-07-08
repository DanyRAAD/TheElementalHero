using UnityEngine;
using TMPro;

public class LanguageSelector : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;

    void Start()
    {
        int savedLang = PlayerPrefs.GetInt("language", 0);
        languageDropdown.value = savedLang;
        LanguageManager.Instance.SetLanguage(savedLang);
    }

    public void OnLanguageChanged(int value)
    {
        // Vista previa (opcional)
        LanguageManager.Instance.SetLanguage(value);
    }

    public void OnSaveLanguage()
    {
        int selected = languageDropdown.value;
        LanguageManager.Instance.SetLanguage(selected);
    }
}
