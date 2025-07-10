using UnityEngine;
using TMPro;

public class PantallaManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    private const string MODO_PANTALLA_KEY = "modo_pantalla";
    private const int DEFAULT_MODO = 0; // Pantalla completa

    void Start()
    {
        int modo = PlayerPrefs.GetInt(MODO_PANTALLA_KEY, DEFAULT_MODO);
        dropdown.value = modo;
        AplicarModoPantalla(modo);
    }

    public void CambiarModoPantalla(int valor)
    {
        AplicarModoPantalla(valor);
    }

    public void GuardarModoPantalla()
    {
        PlayerPrefs.SetInt(MODO_PANTALLA_KEY, dropdown.value);
        PlayerPrefs.Save();
    }

    public void RestablecerModoPantalla()
    {
        dropdown.value = DEFAULT_MODO;
        AplicarModoPantalla(DEFAULT_MODO);
        GuardarModoPantalla();
    }

    private void AplicarModoPantalla(int modo)
    {
        switch (modo)
        {
            case 0: // Pantalla completa
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1: // Ventana
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 2: // Sin bordes
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            default:
                Debug.LogWarning("Modo de pantalla desconocido");
                break;
        }
    }
}
