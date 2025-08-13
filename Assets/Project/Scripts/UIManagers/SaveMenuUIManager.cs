using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  

public class SaveMenuUIManager : MonoBehaviour
{
    public GameObject panelSaveMenu;

    public Button continuarButton; 

    private void Start()
    {
        
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
            GameState.cargarPartidaGuardada = true; 
            SceneManager.LoadScene("RuinasAntiguas");
        }
        else
        {
            Debug.LogWarning("No hay partida guardada para continuar.");
        }
    }
}
