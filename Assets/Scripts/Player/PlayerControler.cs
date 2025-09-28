using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerControler : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction move; //up and down
    private InputAction slide; // left and right
    private InputAction laser;
    private InputAction restart;
    private InputAction quit;

    [SerializeField] private GameObject leftZone;
    [SerializeField] private GameObject topZpne;
    [SerializeField] private GameObject rightZone;
    [SerializeField] private GameObject downZone;
    [SerializeField] private GameObject EnemySpawner;
    [SerializeField] private GameObject laserObject;

    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text scoreText;

    private bool isPlMoving; // player move up / down
    private bool isPlSliding;// player move left / right
    private bool isPlStationary;// player is in place / no movement detected
    public bool canLaser; // laser is on cooldown if set to false

    public float moveDirection;
    public float slideDirection;

    public float PlSpeed; // player speed
    public float atackTime; // how long it takes before next shot

    public int hp;
    private int score;

    void Start()
    {
        isPlSliding = true;
        livesText.text = ("Lives: " + hp);
        scoreText.text = ("Score: " + score);

        playerInput.currentActionMap.Enable();  //Enable action map
        move = playerInput.currentActionMap.FindAction("Move");
        slide = playerInput.currentActionMap.FindAction("Slide");
        laser = playerInput.currentActionMap.FindAction("Laser");
        restart = playerInput.currentActionMap.FindAction("Restart"); 
        quit = playerInput.currentActionMap.FindAction("Exit");

    move.started += Move_started;
        move.canceled += Move_canceled;
        slide.started += Slide_started;
        slide.canceled += Slide_canceled;
        laser.started += Laser_started;
        restart.started += Restart_started;
        quit.started += Quit_started;
        canLaser = true;
    }

    

    private void Laser_started(InputAction.CallbackContext obj)
    {
        if (canLaser)
        {
            Instantiate(laserObject);
            canLaser = false;
        }
    }
    private void Restart_started(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Quit_started(InputAction.CallbackContext obj)
    {
        EditorApplication.isPlaying = false; // Stop play mode in the editor    }
        Application.Quit();
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

    public IEnumerator LaserCooldown()
    {
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
        }
        canLaser = true;
    }
    public void StartCooldown(string ability)
    {
        if (ability == "laser")
        {
            StartCoroutine(LaserCooldown());
        }
        
    }
    private void FixedUpdate()
    {
        if (isPlMoving)
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = 
                new Vector2(0, moveDirection * PlSpeed * Time.deltaTime);


        }

        if (isPlSliding)
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = 
                new Vector2(slideDirection * PlSpeed * Time.deltaTime, 0);

        }

        if (isPlStationary)
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        }
    }

    /// <summary>
    /// Temporary Stat increase to Move speed exclusively.
    /// </summary>
    /// <param name="collision"> What is being collided with </param>
    public void increaseSpeed(float speed)
    {
        PlSpeed += speed;
    }

    public void hitPlayer(int dmg)
    {
        hp = hp - dmg;
        livesText.text = ("Lives: " + hp);


        if (hp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ScoreUp(int scoreIncrease)
    {
        score += scoreIncrease;
        scoreText.text = ("Score: " + score);

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
