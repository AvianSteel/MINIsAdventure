using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControler : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction move; //up and down
    private InputAction slide; // left and right
    private InputAction laser;


    [SerializeField] private GameObject leftZone;
    [SerializeField] private GameObject topZpne;
    [SerializeField] private GameObject rightZone;
    [SerializeField] private GameObject downZone;
    [SerializeField] private GameObject EnemySpawner;
    [SerializeField] private GameObject laserObject;


    private bool isPlMoving; // player move up / down
    private bool isPlSliding;// player move left / right
    private bool isPlStationary;// player is in place / no movement detected

    public float moveDirection;
    public float slideDirection;

    public float PlSpeed; // player speed
    public float atackTime; // how long it takes before next shot



    void Start()
    {
        isPlSliding = true;

        playerInput.currentActionMap.Enable();  //Enable action map
        move = playerInput.currentActionMap.FindAction("Move");
        slide = playerInput.currentActionMap.FindAction("Slide");
        laser = playerInput.currentActionMap.FindAction("Laser");

        move.started += Move_started;
        move.canceled += Move_canceled;
        slide.started += Slide_started;
        slide.canceled += Slide_canceled;
        laser.started += Laser_started;


    }

    private void Laser_started(InputAction.CallbackContext obj)
    {
        Instantiate(laserObject);
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
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, moveDirection * PlSpeed * Time.deltaTime);


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
            slideDirection = slide.ReadValue<float>();

            // depending on the movement direction the coresponding zone is activated others are desactivated
            if (moveDirection > 0)
            {
                topZpne.SetActive(true);
                leftZone.SetActive(false);
                rightZone.SetActive(false);
                downZone.SetActive(false);

            }
            else if (moveDirection < 0)
            {
                topZpne.SetActive(false);
                leftZone.SetActive(false);
                rightZone.SetActive(false);
                downZone.SetActive(true);
            }

        }

        if (isPlSliding)
        {
            slideDirection = slide.ReadValue<float>();
            moveDirection = move.ReadValue<float>();

            // depending on the movement direction the coresponding zone is activated others are desactivated
            if (slideDirection > 0)
            {
                topZpne.SetActive(false);
                leftZone.SetActive(false);
                rightZone.SetActive(true);
                downZone.SetActive(false);
            }
            else if (slideDirection < 0)
            {
                topZpne.SetActive(false);
                leftZone.SetActive(true);
                rightZone.SetActive(false);
                downZone.SetActive(false);
            }
        }

    }
}
