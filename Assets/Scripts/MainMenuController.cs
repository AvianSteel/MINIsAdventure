using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Score+Boundary");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
