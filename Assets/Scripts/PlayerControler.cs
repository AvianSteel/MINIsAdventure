using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControler : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction move; //up and down
    private InputAction slide; // left and right


    private bool isPlMoving; // player move up / down
    private bool isPlSliding;// player move left / right
    private bool isPlStationary;// player is in place / no movement detected

    private float moveDirection;
    private float slideDirection;

    public float PlSpeed; // player speed

    void Start()
    {
        isPlSliding = true;

        playerInput.currentActionMap.Enable();  //Enable action map
        move = playerInput.currentActionMap.FindAction("Move");
        slide = playerInput.currentActionMap.FindAction("Slide");

        move.started += Move_started;
        move.canceled += Move_canceled;
        slide.started += Slide_started;
        slide.canceled += Slide_canceled;


    }


    private void Move_canceled(InputAction.CallbackContext obj)
    {
        isPlMoving = false;

        if (!isPlSliding) // if player not mooving ether upwards or sideways mark stationary as true
        {
            isPlStationary = true;
        }
    }
    private void Move_started(InputAction.CallbackContext obj)
    {
        isPlMoving = true;
        isPlStationary = false;
        isPlSliding = false;

    }


    private void Slide_canceled(InputAction.CallbackContext obj)
    {
        isPlSliding = false;

        if (!isPlMoving) // if player not mooving ether upwards or sideways mark stationary as true
        {
            isPlStationary = true;
        }
    }
    private void Slide_started(InputAction.CallbackContext obj)
    {
        isPlMoving = false;
        isPlStationary = false;


        isPlSliding = true;

        //print("Movement started");0.   1       
    }

    private void FixedUpdate()
    {
        if (isPlMoving)
        {
            print("Forward" + (moveDirection * 50 * Time.deltaTime));
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, moveDirection * 50 * Time.deltaTime);


        }

        if (isPlSliding)
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(slideDirection * PlSpeed * Time.deltaTime, 0);

        }

        if (isPlStationary)
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        }



    }

    void Update()
    {
        if (isPlMoving)
        {
            moveDirection = move.ReadValue<float>();

        }

        if (isPlSliding)
        {
            slideDirection = slide.ReadValue<float>();

        }

    }
}
