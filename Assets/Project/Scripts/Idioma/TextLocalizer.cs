using UnityEngine;
using TMPro;

public class TextLocalizer : MonoBehaviour
{
    public string localizationKey;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void UpdateText()
    {
        if (text == null)
        {
            text = GetComponent<TMP_Text>();
            if (text == null)
            {
                Debug.LogError($"[TextLocalizer] No se encontró TMP_Text en '{gameObject.name}'");
                return;
            }
        }

        var currentLanguage = LanguageManager.Instance.GetCurrentLanguage();

        if (currentLanguage.TryGetValue(localizationKey.ToLower(), out string value))
        {
            text.text = value;
        }
        else
        {
            Debug.LogWarning($"No se encontró traducción para la clave '{localizationKey}'");
        }
    }
}
