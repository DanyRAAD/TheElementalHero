using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMenuUIManager : MonoBehaviour
{
    public void RegresarAlMenuPrincipal()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
