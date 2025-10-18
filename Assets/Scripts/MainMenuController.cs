using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("VirticleSlice");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ControlsMenu()
    {
        SceneManager.LoadScene("PauseMenuScene");
    }
}
