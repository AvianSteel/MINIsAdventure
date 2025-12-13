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
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private PlayerControler playerControler;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("029_Decline_09");
        audioSource.Play();

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
        if(playerControler.isChoosingAbility == false)
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
        audioSource.Play();
        DeactivateMenu();
    }
    public void QuitButton()
    {
        audioSource.Play();
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        isPaused = false;
        SceneManager.LoadScene("MainMenuScene");
    }
}
