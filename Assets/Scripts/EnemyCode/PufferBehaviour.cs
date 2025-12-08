using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PufferBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject drop;
    [SerializeField] private GameObject abilityDrop;
    [SerializeField] private GameObject dmgPopUp;
    [SerializeField] private GameObject deathParticles;


    private StatDropController dropController;
    public GameObject enemySpawn;

    public float speed;
    public int dropChance; // higher number lees liekly it drops
    public int abilityDropChance; // higher number lees liekly it drops

    private bool lockTarget; // the point where the players was and launge there
    private GameObject cloneStorage;
    public float hp;
    private static float Originalhp = 5; // will be usedas a reference to reset the HP when object pooled 
    private static float originalSpeed = 1;


    public float angle; // what is the angle between fish and player

    private int dropRoll;
    private bool puff; // is it inflated
    public Vector3 puffScale;

    SpriteRenderer sr; // reference to how the skin is pointed

    [SerializeField] private GameObject NoPufSkinX;
    [SerializeField] private GameObject NoPufSkinY;



    [SerializeField] private GameObject PufSkinX;
    [SerializeField] private GameObject PufSkinY;



    public int angleToTarget;

    [SerializeField] private StatScalingController statController;
    private float statScalePuff;
    private TimerController timerController;
    [SerializeField] private BoxCollider2D coll1;
    [SerializeField] private BoxCollider2D coll2;

    void Start()
    {
        Originalhp = hp;

        target = GameObject.FindWithTag("Player");
       // statController = target.gameObject.GetComponent<StatScalingController>();
        statScalePuff = statController.statScale;
        hp *= statScalePuff;
        speed *= statScalePuff;
        Mathf.Round(hp);
        puff = false;
        
        // dropController = GameObjectsa.Find("DropController").GetComponent<StatDropController>();
      //  Vector3 loungePoint = target.transform.position;

       // Vector3 direction = (loungePoint - transform.position).normalized;
       // gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;



    }
    private void OnEnable()
    {
        // when object pooled enemy hp is lower than zero, so check on that
        if (hp <= 0)
        {
            hp = Originalhp;
        }
    }

    public void PufferInit()
    {
        gameObject.SetActive(true);
        hp = Originalhp;
        speed = originalSpeed;
        timerController = (TimerController)GameObject.FindWithTag("Canvas").GetComponent("TimerController");
        statScalePuff = timerController.statScaleGlobal;
        target = GameObject.FindWithTag("Player");
        hp *= statScalePuff;
        speed *= statScalePuff;
        Mathf.Round(hp);
        puff = false;
        coll1.enabled = true;
        coll2.enabled = true;
    }

    void FixedUpdate ()
    {
        Vector3 toPlayer = target.transform.position - transform.position;

        angle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg; // gets the angle to player (0 - 180 = top side) (0 - -180 = down side)


        if (Vector2.Distance(transform.position, target.transform.position) > 0 ) // always move to player
        {

            if (angle < 45 && angle > -45 ) // moving right
            {
                //  sr.flipX = true;
                if (puff)
                {
                    sr = PufSkinX.GetComponent<SpriteRenderer>(); // reference to how the skin is oriented

                    PufSkinX.SetActive(true);
                    PufSkinY.SetActive(false);
                    NoPufSkinX.SetActive(false);
                    NoPufSkinY.SetActive(false);
                }
                else
                {
                    sr = NoPufSkinX.GetComponent<SpriteRenderer>(); // reference to how the skin is oriented

                    NoPufSkinX.SetActive(true);
                    NoPufSkinY.SetActive(false);
                    PufSkinX.SetActive(false);
                    PufSkinY.SetActive(false);
                }
                sr.flipY = false;



            }
            else if (angle < 135 && angle > 45) // moving up
            {

                if (puff)
                {
                    PufSkinX.SetActive(false);
                    PufSkinY.SetActive(true);
                    NoPufSkinX.SetActive(false);
                    NoPufSkinY.SetActive(false);
                }
                else
                {
                    NoPufSkinX.SetActive(true);
                    NoPufSkinY.SetActive(true);
                    PufSkinX.SetActive(false);
                    PufSkinY.SetActive(false);
                }

                // sr.flipX = false;
                // sr.flipY = false;

                // launge animation = false

            }
            else if ((angle < 180 && angle > 135) || (angle <= -135 && angle >= -180)) // mooving left
            {
                if (puff)
                {
                    sr = PufSkinX.GetComponent<SpriteRenderer>(); // reference to how the skin is oriented
                    PufSkinX.SetActive(true);
                    PufSkinY.SetActive(false);
                    NoPufSkinX.SetActive(false);
                    NoPufSkinY.SetActive(false);
                }
                else
                {
                    sr = NoPufSkinX.GetComponent<SpriteRenderer>(); // reference to how the skin is oriented

                    NoPufSkinX.SetActive(true);
                    NoPufSkinY.SetActive(false);
                    PufSkinX.SetActive(false);
                    PufSkinY.SetActive(false);
                }
                // sr.flipX = false;
                sr.flipY = true;

                

            }
            else if (angle < -45 && angle > -135) // mooving down
            {
                if (puff)
                {
                    sr = PufSkinX.GetComponent<SpriteRenderer>(); // reference to how the skin is oriented
                    PufSkinX.SetActive(false);
                    PufSkinY.SetActive(true);
                    NoPufSkinX.SetActive(false);
                    NoPufSkinY.SetActive(false);
                }
                else
                {
                    sr = NoPufSkinX.GetComponent<SpriteRenderer>(); // reference to how the skin is oriented

                    NoPufSkinX.SetActive(false);
                    NoPufSkinY.SetActive(true);
                    PufSkinX.SetActive(false);
                    PufSkinY.SetActive(false);
                }
                

            }

            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize(); // Keep velocity consistent
            if (puff)
            {
                gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed / 3;

                // make the needed skin visible

                sr = PufSkinX.GetComponent<SpriteRenderer>(); // reference to how the skin is oriented

            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;

                // make the needed skin visible

                sr = NoPufSkinX.GetComponent<SpriteRenderer>(); // reference to how the skin is oriented

            }


            transform.right = target.transform.position - transform.position;// always face the target (where to move)

            // flip the skin if necesary
            if (transform.position.x > target.transform.position.x && sr.flipY == false)
            {
                sr.flipY = true;

            }
            else if (transform.position.x < target.transform.position.x && sr.flipY == true)
            {
                // sr.flipX = false;
                sr.flipY = false;
            }


        }




        

    }

  
    /// <summary>
    /// Expands the pufferfish outward, then waits for 2 seconds before deflating back to normal size
    /// </summary>
    /// <param name="targetScale"></param>
    /// <param name="growDuration"></param>
    /// <returns></returns>
    IEnumerator DoPuff(Vector3 targetScale, float growDuration)
    {
        PufSkinX.GetComponent<EnemyBlinkWhite>().FlashRed(); // make it blink red
        PufSkinY.GetComponent<EnemyBlinkWhite>().FlashRed(); // make it blink red

        NoPufSkinX.GetComponent<EnemyBlinkWhite>().FlashRed();
        NoPufSkinY.GetComponent<EnemyBlinkWhite>().FlashRed();

        Vector3 initialScale = transform.localScale;
        float elapsed = 0f;
        puff = true;

        // Grow
        while (elapsed < growDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / growDuration;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }
        transform.localScale = targetScale;

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Shrink back
        elapsed = 0f;
        while (elapsed < growDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / growDuration;
            transform.localScale = Vector3.Lerp(targetScale, initialScale, t);
            yield return null;
        }

        transform.localScale = initialScale;
        puff = false;
    }

   



    /// <summary>
    /// Takes damage on contact with player or ammo, deals damage to player upon collision
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
            collision.gameObject.GetComponent<PlayerControler>().hitPlayer(3);
            enemyHit(10);
        }
    }

    /// <summary>
    /// enemy gets dmg, called by whatever hits it like laser and ammo, if hp<0 the next function is called
    /// </summary>
    /// <param name="dmg"></param>
    public void enemyHit(float Pldmg)
    {


        if (!puff)
        {
            int tempDmg = Mathf.FloorToInt(Pldmg);
            hp -= Pldmg;

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
                StartCoroutine(DoPuff(puffScale,2));
            }
        }
        


    }


    /// <summary>
    /// Increases score before checking if it will drop anything
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
