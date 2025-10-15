using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Squid");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
