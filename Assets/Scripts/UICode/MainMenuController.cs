using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject controlsUI;
    [SerializeField] private AudioClip buttonSound;


    public void StartGame()
    {
        AudioSource.PlayClipAtPoint(buttonSound, transform.position);
        SceneManager.LoadScene("Beta");
    }

    public void QuitGame()
    {
        AudioSource.PlayClipAtPoint(buttonSound, transform.position);
        Application.Quit();
    }
    public void MainMenu()
    {
        AudioSource.PlayClipAtPoint(buttonSound, transform.position);
        SceneManager.LoadScene("MainMenuScene");
    }
    public void ControlsMenu()
    {
        AudioSource.PlayClipAtPoint(buttonSound, transform.position);
        SceneManager.LoadScene("PauseMenuScene");
    }
    public void ControlsButton()
    {
        AudioSource.PlayClipAtPoint(buttonSound, transform.position);
        controlsUI.SetActive(true);
    }
    public void BackButton()
    {
        AudioSource.PlayClipAtPoint(buttonSound, transform.position);
        controlsUI.SetActive(false);
    }
}
