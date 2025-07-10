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

    // NUEVO: Panel de confirmación de salida
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

    // NUEVO: Abre panel de confirmación
    public void ConfirmarSalida()
    {
        panelInicial.SetActive(false); // Oculta los botones del menú
        panelConfirmacionSalida.SetActive(true); // Muestra la confirmación
    }


    // NUEVO: Cierra el popup sin salir
    public void CancelarSalida()
    {
        panelConfirmacionSalida.SetActive(false); // Oculta la confirmación
        panelInicial.SetActive(true); // Vuelve a mostrar el menú
    }


    // NUEVO: Sale del juego
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
