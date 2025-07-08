using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Paneles principales
    public GameObject panelInicial;
    public GameObject panelOpciones;

    // Subpaneles dentro del PanelOpciones
    public GameObject panelGeneral;
    public GameObject panelSonido;
    public GameObject panelControles;

    // Llamado desde el botón "Opciones" del menú inicial
    public void IrAOpciones()
    {
        panelInicial.SetActive(false);
        panelOpciones.SetActive(true);
        MostrarSubpanel("General"); // Mostrar General por defecto
    }

    // Cambia entre General, Sonido, Controles
    public void MostrarSubpanel(string subpanel)
    {
        panelGeneral.SetActive(subpanel == "General");
        panelSonido.SetActive(subpanel == "Sonido");
        panelControles.SetActive(subpanel == "Controles");
    }

    // Vuelve al menú principal
    public void VolverAlMenuInicial()
    {
        panelOpciones.SetActive(false);
        panelInicial.SetActive(true);
    }
}
