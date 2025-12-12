using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordfishBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject drop;
    [SerializeField] private GameObject abilityDrop;
    [SerializeField] private GameObject dmgPopUp;
    [SerializeField] private GameObject deathParticles;


    [SerializeField] private GameObject animSides;
    [SerializeField] private GameObject animUp;
    [SerializeField] private GameObject animDown;
    [SerializeField] private GameObject animLounge;



    public GameObject enemySpawn;
    private StatDropController dropController;
    public float speed;
    public float loungeSpeed;
    public int dropChance; // higher number lees liekly it drops
    public int abilityDropChance; // higher number lees liekly it drops

    private bool lockTarget; // the point where the players was and launge there
    public float hp;
    private static float Originalhp = 3; // will be usedas a reference to reset the HP when object pooled 
    private static float OriginalSpeed = 1;
    private static float OriginalLoungeSpeed = 2.5f;
    private GameObject cloneStorage;
    public GameObject pl;

    [SerializeField] private GameObject swordSkin;
    SpriteRenderer sr;
    SpriteRenderer loungeSr;


    private int dropRoll;
    private float angle; // what is the angle between fish and player

    private float statScaleSword;
    public TimerController timerController;

    [SerializeField] private BoxCollider2D coll1;
    [SerializeField] private BoxCollider2D coll2;
    void Start()
    {
        target = GameObject.FindWithTag("Player"); // can be changed to anything that needs to be followed by enemy, example mine

        sr = animSides.GetComponent<SpriteRenderer>(); // reference to how the skin is oriented
        loungeSr = animLounge.GetComponent<SpriteRenderer>();

        pl = GameObject.FindWithTag("Player"); // used to get referenvce to the player code
        // dropController = GameObjectsa.Find("DropController").GetComponent<StatDropController>();
        Vector3 loungePoint = target.transform.position;

        Vector3 direction = (loungePoint - transform.position).normalized;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;

        timerController = (TimerController)GameObject.FindWithTag("Canvas").GetComponent("TimerController");
        statScaleSword = timerController.statScaleGlobal;
        coll1.enabled = true;
        coll2.enabled = true;
    }

    public void SwordfishInit()
    {
        target = GameObject.FindWithTag("Player"); // can be changed to anything that needs to be followed by enemy, example mine

        sr = animSides.GetComponent<SpriteRenderer>(); // reference to how the skin is oriented
        loungeSr = animLounge.GetComponent<SpriteRenderer>();

        pl = GameObject.FindWithTag("Player"); // used to get referenvce to the player code
        Vector3 loungePoint = target.transform.position;

        Vector3 direction = (loungePoint - transform.position).normalized;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        timerController = (TimerController)GameObject.FindWithTag("Canvas").GetComponent("TimerController");
        gameObject.SetActive(true);
        coll1.enabled = true;
        coll2.enabled = true;
        Debug.Log("Swordfish Initialized");
        hp = Originalhp;
        speed = OriginalSpeed;
        loungeSpeed = OriginalLoungeSpeed;
        statScaleSword = timerController.statScaleGlobal;
        hp *= statScaleSword;
        speed *= statScaleSword;
        loungeSpeed *= statScaleSword;
        hp = Mathf.Round(hp);
    }

    private void OnEnable()
    {
        // when object pooled enemy hp is lower than zero, so check on that
        if (hp <= 0)
        {
            hp = Originalhp;
            speed = OriginalSpeed;
            loungeSpeed = OriginalLoungeSpeed;
            statScaleSword = timerController.statScaleGlobal;
            hp *= statScaleSword;
            speed *= statScaleSword;
            loungeSpeed *= statScaleSword;
            hp = Mathf.Round(hp);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 toPlayer = pl.transform.position - transform.position;

        angle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg; // gets the angle to player (0 - 180 = top side) (0 - -180 = down side)

        if (Vector2.Distance(transform.position, target.transform.position) > 3 && !lockTarget) // if close lounge
        {

            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize(); // Keep velocity consistent

            gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;

            transform.right = target.transform.position - transform.position;// always face the target (where to move)


            if (angle < 45 && angle > -45 && sr.flipX == false) // moving right
            {
                //  sr.flipX = true;
                sr.flipY = false;
                loungeSr.flipY = false;

                animSides.SetActive(true);
                animUp.SetActive(false);
                animDown.SetActive(false);
                animLounge.SetActive(false); // launge animation = false

            }
            else if (angle < 135 && angle > 45) // moving up
            {

                // sr.flipX = false;
                // sr.flipY = false;
                animSides.SetActive(false);
                animUp.SetActive(true);
                animDown.SetActive(false);
                animLounge.SetActive(false); // launge animation = false

            }
            else if ((angle < 180 && angle > 135) || (angle <= -135 && angle >= -180)) // mooving left
            {

                // sr.flipX = false;
                sr.flipY = true;
                loungeSr.flipY = true;

                animSides.SetActive(true);
                animUp.SetActive(false);
                animDown.SetActive(false);
                animLounge.SetActive(false); // launge animation = false

            }
            else if (angle < -45 && angle > -135) // mooving down
            {

                animSides.SetActive(false);
                animUp.SetActive(false);
                animDown.SetActive(true);
                animLounge.SetActive(false); // launge animation = false

            }


        }
        else if (Vector2.Distance(transform.position, target.transform.position) <= 3 && !lockTarget) // if far away stop launging
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize(); // Keep velocity consistent

            Lounge(direction);
            lockTarget = true;




        }
        else if (lockTarget && Vector2.Distance(transform.position, target.transform.position) > 5)
        {
            lockTarget = false;
            animSides.SetActive(false);
            animUp.SetActive(false);
            animDown.SetActive(false);
        }


    }
    /// <summary>
    /// Performs a lunge at the players position at beginning of lunge
    /// </summary>
    /// <param name="direction"></param>
    private void Lounge(Vector2 direction)
    {
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * loungeSpeed;

        // if it is curently louging check if it moovse left or right comapred to player, if so turn the lounge animation on
        if ((angle < 45 && angle > -45) || ((angle < 180 && angle > 135) || (angle <= -135 && angle >= -180))) // moving right
        {
            animLounge.SetActive(true); // launge animation = false
            animSides.SetActive(false);


        }

    }

    /// <summary>
    /// If colliding with a bullet or the player, takes damage after dealing some if colliding with player
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ammo")
        {
            EnemyHit(collision.gameObject.GetComponent<AmmoControler>().bulletDmg);
            collision.gameObject.GetComponent<AmmoControler>().bulletGetsOld();

            
        }
        else if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerControler>().hitPlayer(2);
            EnemyHit(10);
        }
        else if (collision.gameObject.name == "CircleZone") // touched secondary zone
        {
            collision.gameObject.GetComponent<TargetZoneControler>().touched2ndZone(gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "CircleZone") // touched secondary zone
        {
            collision.gameObject.GetComponent<TargetZoneControler>().exited2ndZone(gameObject);
        }
    }

    /// <summary>
    /// enemy gets dmg, called by whatever hits it like laser and ammo, if hp<0 the next function is called
    /// </summary>
    /// <param name="dmg"></param>
    public void EnemyHit(float Pldmg)
    {
        int tempDmg = Mathf.FloorToInt(Pldmg);
        hp -= Pldmg;

        

        


        if (hp <= 0)
        {
            if (enemySpawn.GetComponent<EnemySpawnControler>().DeadParticles.Count > 0)
            {
                cloneStorage = enemySpawn.GetComponent<EnemySpawnControler>().DeadParticles[0];
                cloneStorage.transform.position = gameObject.transform.position;
                cloneStorage.SetActive(true);
                enemySpawn.GetComponent<EnemySpawnControler>().DeadParticles.RemoveAt(0);
                cloneStorage.GetComponent<DeathParticleController>().ParticleGetOld();
                

            }
            else
            {
                cloneStorage = Instantiate(deathParticles, transform.position, Quaternion.identity);
                cloneStorage.GetComponent<DeathParticleController>().enemySpawn = enemySpawn;
                cloneStorage.GetComponent<DeathParticleController>().ParticleGetOld();
            }

                EnemyDie();
        }
        else
        {
            if (enemySpawn.GetComponent<EnemySpawnControler>().dmgList.Count > 0 )
            {

                cloneStorage = (enemySpawn.GetComponent<EnemySpawnControler>().dmgList[0]);
               // cloneStorage.GetComponent<DmgPopUp>().enemySpawnControler = enemySpawn;
                cloneStorage.SetActive(true);
                enemySpawn.GetComponent<EnemySpawnControler>().dmgList.RemoveAt(0);

                cloneStorage.GetComponent<DmgPopUp>().Setup(tempDmg, gameObject.transform);
            }
            else
            {
                cloneStorage = Instantiate(dmgPopUp, transform.position, Quaternion.identity);
                cloneStorage.GetComponent<DmgPopUp>().Setup(tempDmg, gameObject.transform);
                cloneStorage.GetComponent<DmgPopUp>().enemySpawnControler = enemySpawn;

            }
        }
        animSides.GetComponent<EnemyBlinkWhite>().FlashRed();
        animUp.GetComponent<EnemyBlinkWhite>().FlashRed();
        animDown.GetComponent<EnemyBlinkWhite>().FlashRed();
        animLounge.GetComponent<EnemyBlinkWhite>().FlashRed();

    }






    /// <summary>
    /// Increases player score, then checks to see if it will drop something before being added to a list of dead
    /// enemies to respawn later.
    /// </summary>
    public void EnemyDie()
    {
        pl.GetComponent<PlayerControler>().ScoreUp(25); // increase score


        dropRoll = Random.Range(0, dropChance);
        if (dropRoll == dropChance / 2)
        {
            Instantiate(drop, gameObject.transform.position, Quaternion.identity);
        }

        dropRoll = Random.Range(0, abilityDropChance);
        if (dropRoll == abilityDropChance / 2)
        {
            if (enemySpawn.GetComponent<EnemySpawnControler>().abilityDropsSpawned < enemySpawn.GetComponent<EnemySpawnControler>().abilityDropsSpawnLimit)
            {
                enemySpawn.GetComponent<EnemySpawnControler>().abilityDropsSpawned += 1;
                Instantiate(abilityDrop, gameObject.transform.position, Quaternion.identity);

            }

        }

        enemySpawn.GetComponent<EnemySpawnControler>().listDeadEnemy(gameObject); // list the enemy in the object pool

        gameObject.SetActive(false);
    }

    

}




