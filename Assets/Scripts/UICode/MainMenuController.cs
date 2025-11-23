using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject controlsUI;
    [SerializeField] private AudioSource audioSource;


    public void StartGame()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("029_Decline_09");
        audioSource.Play();
       
        SceneManager.LoadScene("Beta");
    }

    public void QuitGame()
    {
        
        Application.Quit();
    }
    public void MainMenu()
    {
        audioSource.Play();

       
        SceneManager.LoadScene("MainMenuScene");
    }
    public void ControlsMenu()
    {
        audioSource.Play();

        
        SceneManager.LoadScene("ControlsScene");
    }
    public void ControlsButton()
    {
        audioSource.Play();

        
        controlsUI.SetActive(true);
    }
    public void BackButton()
    {
        audioSource.Play();

        
        controlsUI.SetActive(false);
    }
}
