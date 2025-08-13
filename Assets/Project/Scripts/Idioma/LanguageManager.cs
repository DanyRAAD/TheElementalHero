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
        {"selecciona personaje", "Selecciona Personaje" },
        {"presiona 'E' para recoger ","Presiona 'E' para recoger " },
        {"presiona 'E' para colocar pieza", "Presiona 'E' para colocar pieza" },
        {"piezas disponibles:","Piezas disponibles:" },
        {"necesitas","Necesitas una pieza para colocar"},
        {"guardar juego","Guardar Juego" },
        {"salir al menu","Salir al Menú" },
        {"salir del juego","Salir del juego" },
        {"salir sin guardar","¿Salir sin guardar?"},
        {"abrir cofre","Presiona 'F' Para abrir cofre" },
        {"abrir puerta","Presiona F para abrir puerta" },
        {"llave","¡Ya tienes la llave en tu inventario!" },
        {"necesitas llave","Necesitas una llave" },
        {"vida","Poción de Vida" },
        {"escudo","Poción de Escudo" },
        {"comprar","Comprar" },
        {"vida maxima","¡Tienes vida Máxima!" },
        {"escudo maximo","¡Tienes escudo Máximo!" },
        {"sin posiones vida","No tienes posiones de vida" },
        {"sin posiones escudo","No tienes posiones de escudo" },
        {"tienda","Tienda" },
        {"destruir","Necesitas destruir todos los objetos para continuar" },
        {"final","Próximo nivel muy pronto, espéralo" }
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
        {"selecciona personaje","Select Character" },
        {"presiona 'E' para recoger ","press 'E' to pick up" },
        {"presiona 'E' para colocar pieza","Press E to place piece" },
        {"piezas disponibles:", "Available parts:"},
        {"necesitas","You need a piece to place" },
        {"guardar juego","Save Game" },
        {"salir al menu","Exit to Menu" },
        {"salir del juego","Exit the game" },
        {"salir sin guardar","Exit without saving?" },
        {"abrir cofre","Press F to open chest" },
        {"abrir puerta","Press F to open door" },
        {"llave","You now have the key in your inventory!" },
        {"necesitas llave","You need a key" },
        {"vida","Life Potion" },
        {"escudo","Shield Potion" },
        {"comprar","Buy" },
        {"vida maxima","You have Maximum Life!" },
        {"escudo maximo","You have Maximum Shield!" },
        {"sin posiones vida","You have no life posions" },
        {"sin posiones escudo","You have no shield posions" },
        {"tienda","Shop"},
        {"destruir","You need to destroy all objects to continue."},
        {"final","Next level coming soon, wait for it"}
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
        int defaultIndex = 0; 

        PlayerPrefs.SetInt("language", defaultIndex);
        PlayerPrefs.Save();

        if (languageDropdown != null)
        {
            languageDropdown.value = defaultIndex; 
        }

        ApplyLanguage(); 
    }


}
