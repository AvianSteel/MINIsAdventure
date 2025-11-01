using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject controlsUI;


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
    public void ControlsButton()
    {
        controlsUI.SetActive(true);
    }
    public void BackButton()
    {
        controlsUI.SetActive(false);
    }
}
