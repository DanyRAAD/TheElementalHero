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

   
    public void ConfirmarSalida()
    {
        panelInicial.SetActive(false); 
        panelConfirmacionSalida.SetActive(true); 
    }


    
    public void CancelarSalida()
    {
        panelConfirmacionSalida.SetActive(false); 
        panelInicial.SetActive(true); 
    }


    
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
