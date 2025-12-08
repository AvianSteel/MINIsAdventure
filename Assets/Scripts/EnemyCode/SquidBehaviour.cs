using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class SquidBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject drop;
    [SerializeField] private GameObject abilityDrop;
    [SerializeField] private GameObject dmgPopUp;
    [SerializeField] private GameObject deathParticles;


    [SerializeField] private GameObject ink;
    [SerializeField] private GameObject skin;


    private StatDropController dropController;
    public GameObject enemySpawn;

    public float speed;
    public float loungeSpeed; 
    public int dropChance; // higher number lees liekly it drops
    public int abilityDropChance; // higher number lees liekly it drops

    private bool lockTarget; // the point where the players was and launge there
    private GameObject cloneStorage;
    private GameObject squidStorage;

    public float hp;
    private static float Originalhp = 1; // will be usedas a reference to reset the HP when object pooled 

    public float atackInterval;
    private float initialShootDistance;
    public float shootDistance;

    [SerializeField] private GameObject squidSkin;
    SpriteRenderer sr; // reference to how the skin is pointed

    private float statScaleSquid;

    public TimerController timerController;
    private int dropRoll;

    [SerializeField] private BoxCollider2D coll1;
    [SerializeField] private BoxCollider2D coll2;
    void Start()
    {

        target = GameObject.FindWithTag("Player");
        sr = squidSkin.GetComponent<SpriteRenderer>(); // reference to how the skin is oriented

        timerController = (TimerController)GameObject.FindWithTag("Canvas").GetComponent("TimerController");
        // dropController = GameObjectsa.Find("DropController").GetComponent<StatDropController>();
        Vector3 loungePoint = target.transform.position;

        Vector3 direction = (loungePoint - transform.position).normalized;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        initialShootDistance = shootDistance;
        shootDistance = shootDistance + Random.Range(-1.1f, 1.0f);

        StartCoroutine(DoISHoot());
    }

    public void SquidInit()
    {
        gameObject.SetActive(true);
        timerController = (TimerController)GameObject.FindWithTag("Canvas").GetComponent("TimerController");
        statScaleSquid = timerController.statScaleGlobal;
        hp = Originalhp;
        hp *= statScaleSquid;
        hp = Mathf.Round(hp);
        target = GameObject.FindWithTag("Player");
        sr = squidSkin.GetComponent<SpriteRenderer>(); // reference to how the skin is oriented
        Vector3 loungePoint = target.transform.position;

        Vector3 direction = (loungePoint - transform.position).normalized;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        initialShootDistance = shootDistance;
        shootDistance = shootDistance + Random.Range(-1.1f, 1.0f);
        coll1.enabled = true;
        coll2.enabled = true;
        StartCoroutine(DoISHoot());
    }
    private void OnEnable()
    {
        // when object pooled enemy hp is lower than zero, so check on that
        if (hp <= 0)
        {
            hp = Originalhp;
            hp *= statScaleSquid;
            hp = Mathf.Round(hp);
            //Debug.Log("Spawning an object pooled squid");
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {

        if (Vector2.Distance(transform.position, target.transform.position) > shootDistance) // if close 
        {


           
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize(); // Keep velocity consistent

            gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
            //sr.flipX = false;



        }
        else if (Vector2.Distance(transform.position, target.transform.position) < shootDistance-1)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize(); // Keep velocity consistent

            shootDistance = initialShootDistance + Random.Range(-1.6f, 1.6f);

            gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * (speed * -1);
              sr.flipX = true;
        }

        transform.right = target.transform.position - transform.position;// always face the target (where to move)

        if (transform.position.x > target.transform.position.x && sr.flipX == false)
        {
            //  sr.flipX = true;
           // sr.flipY = true;

        }
        else if (transform.position.x < target.transform.position.x && sr.flipX == true)
        {
            // sr.flipX = false;
           // sr.flipY = false;
        }


    }

    /// <summary>
    /// calls a courutine that fires a projectile that continues to the players position at the beginning of the function
    /// </summary>
    private void shoot()
    {
        StartCoroutine(TurnAroundAndShoot());

    }

    private IEnumerator DoISHoot()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= shootDistance)
        {
            shoot();
        }
        
        yield return new WaitForSeconds(atackInterval);
        repeatDoIShoot();
    }
    /// <summary>
    /// forms a loop with DoISHoot so that it always checks of there is anybody in the list
    /// </summary>
    private void repeatDoIShoot()
    {
        StartCoroutine(DoISHoot());
    }

    /// <summary>
    /// turn around momentarily to throw the ink from the tentacles
    /// </summary>
    /// <returns></returns>
    private IEnumerator TurnAroundAndShoot()
    {
        if (sr.flipY == true)
        {
            sr.flipY = false;
        }
        else
        {
            sr.flipY = true;
        }

        yield return new WaitForSeconds(0.4f);

        if (enemySpawn.GetComponent<EnemySpawnControler>().SquidInk.Count > 0)
        {
            cloneStorage = enemySpawn.GetComponent<EnemySpawnControler>().SquidInk[0];
            cloneStorage.SetActive(true);
            cloneStorage.transform.position = skin.transform.position;
            cloneStorage.GetComponent<AmmoControler>().targetToMoveTowards = target;
            cloneStorage.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // reset ammo velocity


            cloneStorage.transform.right = target.transform.position - transform.position; // make ink face player when shot

            cloneStorage.GetComponent<AmmoControler>().squidParent = gameObject;
            enemySpawn.GetComponent<EnemySpawnControler>().SquidInk.Remove(cloneStorage);

            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize(); // Keep velocity consistent
            cloneStorage.GetComponent<Rigidbody2D>().linearVelocity = direction * (speed * 2);

            print("ink pooled");
        }
        else
        {
            cloneStorage = Instantiate(ink, skin.transform.position, Quaternion.identity);
            cloneStorage.name = "Ink";
            cloneStorage.GetComponent<AmmoControler>().targetToMoveTowards = target;
            cloneStorage.SetActive(true);
            cloneStorage.transform.right = target.transform.position - transform.position; // make ink face player when shot
            cloneStorage.GetComponent<AmmoControler>().squidParent = gameObject;

            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize(); // Keep velocity consistent
            cloneStorage.GetComponent<Rigidbody2D>().linearVelocity = direction * (speed * 2);

        }


        if (sr.flipY == true)
        {
            sr.flipY = false;
        }
        else
        {
            sr.flipY = true;
        }
    }


    /// <summary>
    /// Takes damage on collision with a bullet or player, dealing damage to player upon collision
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ammo")
        {
            enemyHit(collision.gameObject.GetComponent<AmmoControler>().bulletDmg);
            collision.gameObject.GetComponent<AmmoControler>().bulletGetsOld();


        }
        else if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerControler>().hitPlayer(1);
            enemyHit(10);
        }
    }

    /// <summary>
    /// enemy gets dmg, called by whatever hits it like laser and ammo, if hp<0 the next function is called
    /// </summary>
    /// <param name="dmg"></param>
    public void enemyHit(float Pldmg)
    {
        int tempDmg = Mathf.FloorToInt(Pldmg);
        hp -= Pldmg;

        


        skin.GetComponent<EnemyBlinkWhite>().FlashRed();
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
            enemyDie();
        }
        else
        {
            if (enemySpawn.GetComponent<EnemySpawnControler>().dmgList.Count > 0 && squidStorage)
            {


                squidStorage.GetComponent<DmgPopUp>().enemySpawnControler = enemySpawn;
                squidStorage = enemySpawn.GetComponent<EnemySpawnControler>().dmgList[0];
                squidStorage.SetActive(true);
                enemySpawn.GetComponent<EnemySpawnControler>().dmgList.Remove(cloneStorage);
                squidStorage.GetComponent<DmgPopUp>().Setup(tempDmg, gameObject.transform);
            }
            else
            {
                squidStorage = Instantiate(dmgPopUp, transform.position, Quaternion.identity);
                squidStorage.GetComponent<DmgPopUp>().Setup(tempDmg, gameObject.transform);
                squidStorage.GetComponent<DmgPopUp>().enemySpawnControler = enemySpawn;

            }
        }


    }


    /// <summary>
    /// Increases the score, then checks if it will drop anything 
    /// </summary>
    public void enemyDie()
    {
        target.GetComponent<PlayerControler>().ScoreUp(25); // increase score


        dropRoll = Random.Range(0, dropChance);
        if (dropRoll == dropChance / 2)
        {
            Instantiate(drop, gameObject.transform.position, Quaternion.identity);
        }
        dropRoll = Random.Range(0, abilityDropChance);
        if (dropRoll == abilityDropChance / 2)
        {
            Instantiate(abilityDrop, gameObject.transform.position, Quaternion.identity);
        }

        enemySpawn.GetComponent<EnemySpawnControler>().listDeadEnemy(gameObject); // list the enemy in the object pool

        gameObject.SetActive(false);
    }

} 
