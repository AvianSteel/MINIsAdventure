using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction move; //up and down
    private InputAction slide; // left and right
    private InputAction laser;
    private InputAction mine;
    private InputAction restart;
    private InputAction quit;

    [SerializeField] private GameObject leftZone;
    [SerializeField] private GameObject topZpne;
    [SerializeField] private GameObject rightZone;
    [SerializeField] private GameObject downZone;
    [SerializeField] private GameObject EnemySpawner;
    [SerializeField] private GameObject laserObject;
    [SerializeField] private GameObject seaMine;

    [SerializeField] private GameObject downSkin;
    [SerializeField] private GameObject upSkin;
    [SerializeField] private GameObject leftSkin;
    [SerializeField] private GameObject rightSkin;

    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text scoreText;

    private bool isPlMoving; // player move up / down
    private bool isPlSliding;// player move left / right
    private bool isPlStationary;// player is in place / no movement detected
    public bool canLaser; // laser is on cooldown if set to false
    public bool canMine;

    public float moveDirection;
    public float slideDirection;
    public float ammoDmg; // will be inherited by the bullet clones

    public float PlSpeed; // player speed
    public float atackTime; // how long it takes before next shot
    public float abilityTime; // Cooldown reduction for abilities
    public int Pldefense; // Damage value reduction, player will always take at least one damage

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
        mine = playerInput.currentActionMap.FindAction("Mine");
        restart = playerInput.currentActionMap.FindAction("Restart"); 
        quit = playerInput.currentActionMap.FindAction("Exit");

        move.started += Move_started;
        move.canceled += Move_canceled;
        slide.started += Slide_started;
        slide.canceled += Slide_canceled;
        laser.started += Laser_started;
        mine.started += Mine_started;
        restart.started += Restart_started;
        quit.started += Quit_started;
        canLaser = true;
        canMine = true;
    }

    private void Mine_started(InputAction.CallbackContext obj)
    {
        if (canMine)
        {
            Instantiate(seaMine,transform.position,Quaternion.identity);
            canMine = false;
        }
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
        //EditorApplication.isPlaying = false; // Stop play mode in the editor    
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
            yield return new WaitForSeconds(1 - abilityTime);
        }
        canLaser = true;
    }

    public IEnumerator MineCooldown()
    {
        for(int i=0; i < 3; i++)
        {
            yield return new WaitForSeconds(1 - abilityTime);
        }
        canMine = true;
    }
    public void StartCooldown(string ability)
    {
        if (ability == "laser")
        {
            StartCoroutine(LaserCooldown());
        }else if(ability == "sea mine")
        {
            StartCoroutine(MineCooldown());
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
    public void increaseDefense(int defense)
    {
        Pldefense += defense;
    }
    public void increaseAttackSpeed(float atkSpeed)
    {
        atackTime -= atkSpeed;
        if ( atackTime < 0.25f)
        {
            atackTime = 0.25f;
        }
    }
    public void increaseAbilitySpeed(float abilitySpeed)
    {
        abilityTime += abilitySpeed;
    }
    public void hitPlayer(int dmg)
    {
        int damage = (dmg - Pldefense);
        if(damage < 1)
        {
            damage = 1;
        }
        hp = hp - damage;
        livesText.text = ("Lives: " + hp);


        if (hp <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
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

                upSkin.SetActive(true);
                downSkin.SetActive(false);
                leftSkin.SetActive(false);
                rightSkin.SetActive(false);
            }
            else if (moveDirection < 0)
            {
                topZpne.SetActive(false);
                leftZone.SetActive(false);
                rightZone.SetActive(false);
                downZone.SetActive(true);

                upSkin.SetActive(false);
                downSkin.SetActive(true);
                leftSkin.SetActive(false);
                rightSkin.SetActive(false);
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

                upSkin.SetActive(false);
                downSkin.SetActive(false);
                leftSkin.SetActive(false);
                rightSkin.SetActive(true);
            }
            else if (slideDirection < 0)
            {
                topZpne.SetActive(false);
                leftZone.SetActive(true);
                rightZone.SetActive(false);
                downZone.SetActive(false);

                upSkin.SetActive(false);
                downSkin.SetActive(false);
                leftSkin.SetActive(true);
                rightSkin.SetActive(false);
            }
        }

    }

   

}
