using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject drop;

    private StatDropController dropController;
    public GameObject enemySpawn;

    public float speed;
    public int dropChance; // higher number lees liekly it drops
    private bool lockTarget; // the point where the players was and launge there
    private GameObject cloneStorage;
    public float hp;


    private int dropRoll;
    private bool puff; // is it inflated
    public Vector3 puffScale;

    SpriteRenderer sr; // reference to how the skin is pointed

    [SerializeField] private GameObject NoPufSkin;

    [SerializeField] private GameObject PufSkin;


    public int angleToTarget;

    void Start()
    {
        puff = false;
        target = GameObject.FindWithTag("Player");
        // dropController = GameObjectsa.Find("DropController").GetComponent<StatDropController>();
      //  Vector3 loungePoint = target.transform.position;

       // Vector3 direction = (loungePoint - transform.position).normalized;
       // gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;



    }

    void FixedUpdate ()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > 0 ) // always move to player
        {
           
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize(); // Keep velocity consistent
            if (puff)
            {
                gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed / 3;

                // make the needed skin visible
                NoPufSkin.SetActive(false);
                PufSkin.SetActive(true);
                sr = PufSkin.GetComponent<SpriteRenderer>(); // reference to how the skin is oriented

            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;

                // make the needed skin visible
                PufSkin.SetActive(false);
                NoPufSkin.SetActive(true);
                sr = NoPufSkin.GetComponent<SpriteRenderer>(); // reference to how the skin is oriented

            }


            transform.right = target.transform.position - transform.position;// always face the target (where to move)

            // flip the skin if necesary
            if (transform.position.x > target.transform.position.x && sr.flipY == false)
            {
                //  sr.flipX = true;
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
            hp -= Pldmg;
            if (hp <= 0)
            {
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
        gameObject.SetActive(false);
    }

}
