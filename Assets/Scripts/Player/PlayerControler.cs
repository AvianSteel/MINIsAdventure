using System.Collections;
using TMPro;
using UnityEditor;
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
    private InputAction dash;

    private InputAction restart;
    private InputAction quit;

    [SerializeField] private GameObject leftZone;
    [SerializeField] private GameObject topZpne;
    [SerializeField] private GameObject rightZone;
    [SerializeField] private GameObject downZone;
    [SerializeField] private GameObject EnemySpawner;
    [SerializeField] private GameObject laserObject;
    [SerializeField] private GameObject seaMine;
    [SerializeField] private GameObject dashImpact;


    public GameObject abilityMenu;
    public int exceptionChoice;// which ability is not avilable for choice

    [SerializeField] private GameObject downSkin;
    [SerializeField] private GameObject upSkin;
    [SerializeField] private GameObject leftSkin;
    [SerializeField] private GameObject rightSkin;

    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text scoreText;

   
    [SerializeField] private AudioClip hitSound;
   
    private bool isPlMoving; // player move up / down
    private bool isPlSliding;// player move left / right
    private bool isPlStationary;// player is in place / no movement detected
    public bool canLaser; // laser is on cooldown if set to false
    public int laserLvl;// if ability lvl is 0 it must be unlocked first, every lvl > 1 makes it stronger
    public bool canMine;
    public int mineLvl;
    public bool isChoosingAbility;
    public int activeTarget;

    public bool canDash;
    public int dashLvl;
    public bool dashInvulnerab;

    public GameObject dashUI;
    public GameObject mineUI;
    public GameObject laserUI;

    public float moveDirection;
    public float slideDirection;
    public float ammoDmg; // will be inherited by the bullet clones

    public float PlSpeed; // player speed
    public float atackTime; // how long it takes before next shot
    public float abilityTime; // Cooldown reduction for abilities
    public int Pldefense; // Damage value reduction, player will always take at least one damage
    public float DashDuration;
    public float dashSpeedMultiplier;

    public int hp;
    public static int score;


    public float timeRemainLaser;
    public float timeRemainMine;
    public float timeRemainDash;
    [SerializeField] private StatScalingController statController;

    private float ogPlSpeed;
    void Start()
    {
        activeTarget = 0;
        isPlSliding = true;
        score = 0;
        scoreText.text = score.ToString();

        playerInput.currentActionMap.Enable();  //Enable action map
        move = playerInput.currentActionMap.FindAction("Move");
        slide = playerInput.currentActionMap.FindAction("Slide");
        laser = playerInput.currentActionMap.FindAction("Laser");
        mine = playerInput.currentActionMap.FindAction("Mine");
        dash = playerInput.currentActionMap.FindAction("Dash");

        restart = playerInput.currentActionMap.FindAction("Restart"); 
        quit = playerInput.currentActionMap.FindAction("Exit");

        move.started += Move_started;
        move.canceled += Move_canceled;
        slide.started += Slide_started;
        slide.canceled += Slide_canceled;
        laser.started += Laser_started;
        mine.started += Mine_started;
        dash.started += Dash_started;
        restart.started += Restart_started;
        quit.started += Quit_started;
        canLaser = true;
        canMine = true;
        canDash = true;
        laserLvl = 0;
        mineLvl = 0;
        dashLvl = 0;


        dashUI.gameObject.SetActive(false);
        laserUI.gameObject.SetActive(false);
        mineUI.gameObject.SetActive(false);
        ogPlSpeed = PlSpeed;

        Cursor.visible = false;
    }
    #region Controls Actions
    private void Dash_started(InputAction.CallbackContext obj)
    {
        AbilityChoice(3);

        if (canDash && dashLvl > 0 && (isPlSliding || isPlMoving))
        {
            ogPlSpeed = PlSpeed;
            StartCooldown("dash");
            canDash = false;
            PlSpeed *= dashSpeedMultiplier;
            dashInvulnerab = true;
            timeRemainDash = 3 - abilityTime;
        }
    }
    private void Mine_started(InputAction.CallbackContext obj)
    {
        AbilityChoice(2);

        if (canMine && mineLvl > 0)
        {
            Instantiate(seaMine,transform.position,Quaternion.identity);
            canMine = false;
            timeRemainMine = 5 - abilityTime;
        }
    }

    private void Laser_started(InputAction.CallbackContext obj)
    {
        AbilityChoice(1);

        if (canLaser && laserLvl > 0)
        {
            Instantiate(laserObject);
            canLaser = false;
            timeRemainLaser = 10 - abilityTime;
        }
    }
    private void Restart_started(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Quit_started(InputAction.CallbackContext obj)
    {
        //StopCoroutine(statController.StatScalingTimer());
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
    #endregion


    /// <summary>
    /// Performs dash cooldown before re enabling dash
    /// </summary>
    /// <returns></returns>
    public IEnumerator DashCooldown()
    {
        float origPlSpeed = PlSpeed;
        PlSpeed *= dashSpeedMultiplier + dashLvl; // higher dash lvl, higher speed

        for (int i = 1; i >= 0; i--)
        {
         
            yield return new WaitForSeconds(DashDuration); // wait for dash duration than change the speed to original
            PlSpeed = origPlSpeed;
            dashImpact.SetActive(true); // move them aside
            dashInvulnerab = false;
            StartCoroutine(SocialDistance());
        }
        while (timeRemainDash > 0)
        {
            timeRemainDash -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        canDash = true;
        StopCoroutine(DashCooldown());
    }
    /// <summary>
    /// Performs cooldown of laser ability before re enabling laser
    /// </summary>
    /// <returns></returns>
    public IEnumerator LaserCooldown()
    {
        while (timeRemainLaser > 0)
        {
            timeRemainLaser -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        canLaser = true;
        StopCoroutine(LaserCooldown());

    }

    /// <summary>
    /// Performs the cooldown actions for the mine ability before enabling ability again
    /// </summary>
    /// <returns></returns>
    /// 

    public IEnumerator SocialDistance()
    {
        yield return new WaitForSeconds(0.5f);
        dashImpact.SetActive(false);
    }
    public IEnumerator MineCooldown()
    {
        while (timeRemainMine > 0)
        {
            timeRemainMine -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        canMine = true;
        StopCoroutine(MineCooldown());
    }
    /// <summary>
    /// Function called to start the respective abilities cooldown timer
    /// </summary>
    /// <param name="ability"></param>
    public void StartCooldown(string ability)
    {
        if (ability == "laser")
        {
            StartCoroutine(LaserCooldown());
        }else if(ability == "sea mine")
        {
            StartCoroutine(MineCooldown());
        }
        else if (ability == "dash")
        {
            StartCoroutine(DashCooldown());
        }

    }
    /// <summary>
    /// Moving player based on the movement being inputted
    /// </summary>
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
        ogPlSpeed = PlSpeed;
    }
    /// <summary>
    /// Increases defense 
    /// </summary>
    /// <param name="defense"></param>
    public void increaseDefense(int defense)
    {
        Pldefense += defense;
    }
    /// <summary>
    /// Decreases time between attacks
    /// CURRENTLY BUGGED AND CHANGES AN UNUSED VARIABLE
    /// </summary>
    /// <param name="atkSpeed"></param>
    public void increaseAttackSpeed(float atkSpeed)
    {
        atackTime -= atkSpeed;
        if ( atackTime < 0.25f)
        {
            atackTime = 0.25f;
        }
    }
    /// <summary>
    /// Decreases the cooldown of all abilities
    /// </summary>
    /// <param name="abilitySpeed"></param>
    public void increaseAbilitySpeed(float abilitySpeed)
    {
        abilityTime += abilitySpeed;
    }
    /// <summary>
    /// Damages the player by dealing damage - defense value. Damage taken cannot be zero
    /// </summary>
    /// <param name="dmg"></param>
    public void hitPlayer(int dmg)
    {
        if (!dashInvulnerab)
        {
            int damage = (dmg - Pldefense);
            if (damage < 1)
            {
                damage = 1;
            }
          
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
            hp = hp - damage;
            upSkin.GetComponent<EnemyBlinkWhite>().FlashRed();
            downSkin.GetComponent<EnemyBlinkWhite>().FlashRed();
            leftSkin.GetComponent<EnemyBlinkWhite>().FlashRed();
            rightSkin.GetComponent<EnemyBlinkWhite>().FlashRed();

            if (hp <= 0)
            {
                SceneManager.LoadScene("GameOverScene");
            }
        }
        
    }
    /// <summary>
    /// Increase score by a controllable integer amount
    /// </summary>
    /// <param name="scoreIncrease"></param>
    public void ScoreUp(int scoreIncrease)
    {
        score += scoreIncrease;
        scoreText.text = (score.ToString());

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
                activeTarget = 1; // up
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
                activeTarget = 3;

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
                activeTarget = 2;

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
                activeTarget = 0;

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

    


    /// <summary>
    /// called down by the 3 input actions for abilities, every time the key is pressed the game checks if player has to chose an ability
    ///  1 = laser   2 = mine    3 = dash
    /// </summary>
    public void AbilityChoice(int abilityNum)
    {
        if (isChoosingAbility)
        {
            if (abilityNum == 1 && exceptionChoice != 1)
            {
                StopCoroutine(LaserCooldown());
                timeRemainLaser = 0;
                canLaser = true;
                laserLvl++;
                laserUI.gameObject.SetActive(true);
                abilityMenu.GetComponent<ChooseAbility>().closeChoice();

            }
            else if (abilityNum == 2 && exceptionChoice != 2)
            {
                StopCoroutine(MineCooldown());
                timeRemainMine = 0;
                canMine = true;
                mineLvl++;
                mineUI.gameObject.SetActive(true);
                abilityMenu.GetComponent<ChooseAbility>().closeChoice();

            }
            else if (abilityNum == 3 && exceptionChoice != 3)
            {
                StopCoroutine(DashCooldown());
                timeRemainDash = 0;
                canDash = true;
                dashLvl++;
                dashInvulnerab = false;
                PlSpeed = ogPlSpeed;
                dashUI.gameObject.SetActive(true);
                abilityMenu.GetComponent<ChooseAbility>().closeChoice();

            }
        }
    }



    public void OnDestroy()
    {
        move.started -= Move_started;
        move.canceled -= Move_canceled;
        slide.started -= Slide_started;
        slide.canceled -= Slide_canceled;
        laser.started -= Laser_started;
        mine.started -= Mine_started;
        dash.started -= Dash_started;
        restart.started -= Restart_started;
        quit.started -= Quit_started;
    }

}
