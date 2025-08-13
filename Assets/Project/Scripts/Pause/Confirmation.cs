using UnityEngine;
using UnityEngine.SceneManagement;

public class Confirmation : MonoBehaviour
{
    public enum Action { None, ExitToMenu, QuitGame }
    public Action action = Action.None;

    public void ConfirmYes()
    {
        if (action == Action.ExitToMenu)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu"); 
        }
        else if (action == Action.QuitGame)
        {
            Application.Quit();
        }
    }

    public void ConfirmNo()
    {
        gameObject.SetActive(false);
        action = Action.None;
    }
}
