using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("VirticalSlice");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
