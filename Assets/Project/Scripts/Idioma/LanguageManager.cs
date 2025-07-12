using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;
    public TMP_Dropdown languageDropdown;

    public Dictionary<string, string> spanish = new Dictionary<string, string>()
    {
        {"empezar", "Empezar"},
        {"opciones", "Opciones"},
        {"creditos", "Créditos"},
        {"salir", "Salir"},
        {"idioma", "Idioma"},
        {"general", "General"},
        {"sonido", "Sonido"},
        {"controles", "Controles"},
        {"brillo", "Brillo"},
        {"pantalla", "Pantalla"},
        {"espanol", "Español"},
        {"ingles", "Inglés"},
        {"pantalla completa", "Pantalla Completa"},
        {"ventana", "Ventana"},
        {"sin bordes", "Sin bordes"},
        {"volumen general", "Volumen General"},
        {"volumen musica", "Volumen Música"},
        {"volumen efectos", "Volumen Efectos"},
        {"sensibilidad", "Sensibilidad"},
        {"control de camara", "Control de Cámara"},
        {"invertir eje y", "Invertir eje Y"},
        {"invertir eje x", "Invertir eje X"},
        {"si", "Si"},
        {"no", "No"},
        {"guardar","Guardar" },
        {"restablecer opciones","Restablecer opciones" },
        {"¿salir al escritorio?","¿Salir al Escritorio?" },
        {"continuar","Continuar" },
        {"nueva partida","Nueva Partida" },
        {"cargar partida","Cargar Partida" },
        {"selecciona personaje", "Selecciona Personaje" }
    };

    public Dictionary<string, string> english = new Dictionary<string, string>()
    {
        {"empezar", "Start"},
        {"opciones", "Options"},
        {"creditos", "Credits"},
        {"salir", "Exit"},
        {"idioma", "Language"},
        {"general", "General"},
        {"sonido", "Sound"},
        {"controles", "Controls"},
        {"brillo", "Brightness"},
        {"pantalla", "Screen"},
        {"espanol", "Spanish"},
        {"ingles", "English"},
        {"pantalla completa", "Fullscreen"},
        {"ventana", "Windowed"},
        {"sin bordes", "Borderless"},
        {"volumen general", "Master Volume"},
        {"volumen musica", "Music Volume"},
        {"volumen efectos", "SFX Volume"},
        {"sensibilidad", "Sensitivity"},
        {"control de camara", "Camera Control"},
        {"invertir eje y", "Invert Y Axis"},
        {"invertir eje x", "Invert X Axis"},
        {"si", "Yes"},
        {"no", "No"},
        {"guardar","Save" },
        {"restablecer opciones","Reset Options"},
        {"¿salir al escritorio?","Exit to Desktop?" },
        {"continuar","Continue" },
        {"nueva partida","New Game"},
        {"cargar partida","Load Game"},
        {"selecciona personaje","Select Character" }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ApplyLanguage()
    {
        // Actualizar todos los textos
        TextLocalizer[] localizers = Resources.FindObjectsOfTypeAll<TextLocalizer>();
        foreach (TextLocalizer localizer in localizers)
        {
            localizer.UpdateText();
        }

        // Actualizar todos los dropdowns localizables
        DropdownLocalizer[] dropdowns = Resources.FindObjectsOfTypeAll<DropdownLocalizer>(); 
        foreach (DropdownLocalizer dl in dropdowns)
        {
            dl.UpdateOptions();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyLanguage();
    }

    public Dictionary<string, string> GetCurrentLanguage()
    {
        int lang = PlayerPrefs.GetInt("language", 0); // 0: Español, 1: Inglés
        return lang == 0 ? spanish : english;
    }

    public void SetLanguage(int langIndex)
    {
        PlayerPrefs.SetInt("language", langIndex);
        PlayerPrefs.Save();
        ApplyLanguage();
    }

    

    public void ResetIdioma()
    {
        int defaultIndex = 0; // Español por defecto

        PlayerPrefs.SetInt("language", defaultIndex);
        PlayerPrefs.Save();

        if (languageDropdown != null)
        {
            languageDropdown.value = defaultIndex; // Actualiza el Dropdown visualmente
        }

        ApplyLanguage(); // Aplica la traducción a todos los textos
    }


}
