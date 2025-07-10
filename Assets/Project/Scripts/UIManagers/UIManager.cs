using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Paneles principales
    public GameObject panelInicial;
    public GameObject panelOpciones;

    // Subpaneles dentro del PanelOpciones
    public GameObject panelGeneral;
    public GameObject panelSonido;
    public GameObject panelControles;

    // NUEVO: Panel de confirmaci�n de salida
    public GameObject panelConfirmacionSalida;

    public void IniciarJuego()
    {
        SceneManager.LoadScene("SaveMenu");
    }

    public void IrAOpciones()
    {
        panelInicial.SetActive(false);
        panelOpciones.SetActive(true);
        MostrarSubpanel("General");
    }

    public void MostrarSubpanel(string subpanel)
    {
        panelGeneral.SetActive(subpanel == "General");
        panelSonido.SetActive(subpanel == "Sonido");
        panelControles.SetActive(subpanel == "Controles");
    }

    public void VolverAlMenuInicial()
    {
        panelOpciones.SetActive(false);
        panelInicial.SetActive(true);
    }

    // NUEVO: Abre panel de confirmaci�n
    public void ConfirmarSalida()
    {
        panelInicial.SetActive(false); // Oculta los botones del men�
        panelConfirmacionSalida.SetActive(true); // Muestra la confirmaci�n
    }


    // NUEVO: Cierra el popup sin salir
    public void CancelarSalida()
    {
        panelConfirmacionSalida.SetActive(false); // Oculta la confirmaci�n
        panelInicial.SetActive(true); // Vuelve a mostrar el men�
    }


    // NUEVO: Sale del juego
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
