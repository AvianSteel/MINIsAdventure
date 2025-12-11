using UnityEngine;
using UnityEngine.InputSystem;

public class ForceQuit : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private InputAction forceQuit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        forceQuit = playerInput.currentActionMap.FindAction("ForceQuit");

        forceQuit.started += ForceQuit_started;
    }

    private void ForceQuit_started(InputAction.CallbackContext obj)
    {
        print("Quitting");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
