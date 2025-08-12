using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Necesario para Button

public class SaveMenuUIManager : MonoBehaviour
{
    public GameObject panelSaveMenu;

    public Button continuarButton; // Asignar en Inspector el botón Continuar

    private void Start()
    {
        // Verifica si hay partida guardada en slot 1
        bool hayPartidaGuardada = SaveLoadManager.instance.SaveExists(1);
        continuarButton.interactable = hayPartidaGuardada;
    }

    public void RegresarAlMenuPrincipal()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void IrLoadGame()
    {
        panelSaveMenu.SetActive(false);
        // Aquí puedes agregar lógica para mostrar panel de carga si tienes
    }

    public void ReturnSaveMenu()
    {
        panelSaveMenu.SetActive(true);
    }

    public void NewGame()
    {
        GameState.cargarPartidaGuardada = false;
        SceneManager.LoadScene("Cinematica");
    }

    public void Continuar()
    {
        if (SaveLoadManager.instance != null && SaveLoadManager.instance.SaveExists(1))
        {
            GameState.cargarPartidaGuardada = true; // Cargar datos guardados
            SceneManager.LoadScene("RuinasAntiguas");
        }
        else
        {
            Debug.LogWarning("No hay partida guardada para continuar.");
        }
    }
}
