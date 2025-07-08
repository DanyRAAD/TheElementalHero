using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownLocalizer : MonoBehaviour
{
    public string[] optionKeys; // Claves para cada opción, en orden
    private TMP_Dropdown dropdown;

    void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    public void UpdateOptions()
    {
        if (LanguageManager.Instance == null) return;

        // Asegura que el componente TMP_Dropdown esté asignado, incluso si Awake() no se ejecutó aún
        if (dropdown == null)
        {
            dropdown = GetComponent<TMP_Dropdown>();
            if (dropdown == null)
            {
                Debug.LogError($"[DropdownLocalizer] No se encontró TMP_Dropdown en el GameObject '{gameObject.name}'");
                return;
            }
        }

        var currentLanguage = LanguageManager.Instance.GetCurrentLanguage();
        List<TMP_Dropdown.OptionData> translatedOptions = new List<TMP_Dropdown.OptionData>();

        foreach (string key in optionKeys)
        {
            if (currentLanguage.TryGetValue(key.ToLower(), out string translated))
            {
                translatedOptions.Add(new TMP_Dropdown.OptionData(translated));
            }
            else
            {
                Debug.LogWarning($"No se encontró traducción para la clave '{key}'");
                translatedOptions.Add(new TMP_Dropdown.OptionData(key));
            }
        }

        int currentValue = dropdown.value;
        dropdown.options = translatedOptions;
        dropdown.value = currentValue;
        dropdown.RefreshShownValue();
    }

}
