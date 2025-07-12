using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMenuUIManager : MonoBehaviour
{
    public GameObject panelLoadGame;
    public GameObject panelSaveMenu;
    public GameObject panelNewGame;
    public void RegresarAlMenuPrincipal()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

    public void IrLoadGame()
    {
        panelSaveMenu.SetActive(false);
        panelLoadGame.SetActive(true);
    }

    public void ReturnSaveMenu()
    {
        panelLoadGame.SetActive(false);
        panelSaveMenu.SetActive(true);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Cinematica");
    }

}
