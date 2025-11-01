using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject controlsMenu;


    public void StartGame()
    {
        SceneManager.LoadScene("Alpha");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void ControlsMenu()
    {
        SceneManager.LoadScene("PauseMenuScene");
    }
}
