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
                Debug.LogError($"[TextLocalizer] No se encontr� TMP_Text en '{gameObject.name}'");
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
            Debug.LogWarning($"No se encontr� traducci�n para la clave '{localizationKey}'");
        }
    }
}
