using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PlayerMove playerMove;
    private InputAction pauseMenu;
    private bool isPaused;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private AudioClip buttonSound;
    


    private void Awake()
    {
        playerMove = new PlayerMove();
    }

    private void OnEnable()
    {
        pauseMenu = playerMove.Player.Pause;
        pauseMenu.Enable();

        pauseMenu.performed += Pause;
    }

    private void OnDisable()
    {
        pauseMenu.Disable();
    }

    void Pause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
    }

    void DeactivateMenu()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        isPaused = false;
    }

    public void ResumeButton()
    {
        AudioSource.PlayClipAtPoint(buttonSound, transform.position);
        DeactivateMenu();
    }
    public void QuitButton()
    {
        AudioSource.PlayClipAtPoint(buttonSound, transform.position);
        SceneManager.LoadScene("MainMenuScene");
    }
}
