using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject drop;
    [SerializeField] private GameObject abilityDrop;

    public GameObject enemySpawn;
    private StatDropController dropController;
    public float speed;
    public float loungeSpeed; 
    public int dropChance; // higher number lees liekly it drops
    public int abilityDropChance; // higher number lees liekly it drops

    private bool lockTarget; // the point where the players was and launge there
    public float hp;
    public GameObject pl;

    [SerializeField] private GameObject swordSkin;
    SpriteRenderer sr;


    private int dropRoll;

    [SerializeField] private StatScalingController statController;
    private float statScaleSword;

    void Start()
    {
        target = GameObject.FindWithTag("Player"); // can be changed to anything that needs to be followed by enemy, example mine
        statController = target.gameObject.GetComponent<StatScalingController>();
        statScaleSword = statController.statScale;
        hp *= statScaleSword;
        speed *= statScaleSword;
        loungeSpeed *= statScaleSword;
        Mathf.Round(hp);

        sr = swordSkin.GetComponent<SpriteRenderer>(); // reference to how the skin is oriented

        
        pl = GameObject.FindWithTag("Player"); // used to get referenvce to the player code
        // dropController = GameObjectsa.Find("DropController").GetComponent<StatDropController>();
        Vector3 loungePoint = target.transform.position;

        Vector3 direction = (loungePoint - transform.position).normalized;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > 3 && !lockTarget) // if close lounge
        {
           
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize(); // Keep velocity consistent

            gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;

            transform.right = target.transform.position - transform.position;// always face the target (where to move)


            if (transform.position.x > target.transform.position.x && sr.flipX == false)
            {
                //  sr.flipX = true;
                  sr.flipY = true;

            }
            else if (transform.position.x < target.transform.position.x && sr.flipX == true)
            {
               // sr.flipX = false;
                sr.flipY = false;
            }

        }
        else if (Vector2.Distance(transform.position, target.transform.position) <= 3 && !lockTarget ) // if far away stop launging
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize(); // Keep velocity consistent

            lounge(direction);
            lockTarget = true;
        }
        else if (lockTarget && Vector2.Distance(transform.position, target.transform.position) > 5)
        {
            lockTarget = false;

        }


    }
    /// <summary>
    /// Performs a lunge at the players position at beginning of lunge
    /// </summary>
    /// <param name="direction"></param>
    private void lounge(Vector2 direction)
    {
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * loungeSpeed;

    }
   
    /// <summary>
    /// If colliding with a bullet or the player, takes damage after dealing some if colliding with player
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
            collision.gameObject.GetComponent<PlayerControler>().hitPlayer(2);
            enemyHit(10);
        }
    }

    /// <summary>
    /// enemy gets dmg, called by whatever hits it like laser and ammo, if hp<0 the next function is called
    /// </summary>
    /// <param name="dmg"></param>
    public void enemyHit(float Pldmg)
    {
        hp -= Pldmg;
        if (hp <= 0)
        {
            enemyDie();
        }


    }
       
        

            


    /// <summary>
    /// Increases player score, then checks to see if it will drop something before being added to a list of dead
    /// enemies to respawn later.
    /// </summary>
    public void enemyDie()
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
            Instantiate(abilityDrop, gameObject.transform.position, Quaternion.identity);
        }

        enemySpawn.GetComponent<EnemySpawnControler>().listDeadEnemy(gameObject);
        gameObject.SetActive(false);
    }

}
