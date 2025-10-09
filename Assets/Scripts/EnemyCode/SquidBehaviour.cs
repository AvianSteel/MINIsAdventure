using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject drop;
    [SerializeField] private GameObject ink;

    private StatDropController dropController;
    public GameObject enemySpawn;

    public float speed;
    public float loungeSpeed; 
    public int dropChance; // higher number lees liekly it drops
    private bool lockTarget; // the point where the players was and launge there
    private GameObject cloneStorage;
    public float hp;
    public float atackInterval;
    private float initialShootDistance;
    public float shootDistance;

    private int dropRoll;
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        // dropController = GameObjectsa.Find("DropController").GetComponent<StatDropController>();
        Vector3 loungePoint = target.transform.position;

        Vector3 direction = (loungePoint - transform.position).normalized;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        initialShootDistance = shootDistance;
        shootDistance = shootDistance + Random.Range(-1.1f, 1.0f);

        StartCoroutine(DoISHoot());
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > shootDistance) // if close lounge
        {
           
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize(); // Keep velocity consistent

            gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
            


            
        }
        else if (Vector2.Distance(transform.position, target.transform.position) < shootDistance-1)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize(); // Keep velocity consistent

            shootDistance = initialShootDistance + Random.Range(-1.6f, 1.6f);

            gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * (speed * -1);
        }

       



    }

    private void shoot()
    {
        print("shoot");
        cloneStorage = Instantiate(ink, transform.position, Quaternion.identity);
        cloneStorage.name = "Ink";
        cloneStorage.GetComponent<AmmoControler>().targetToMoveTowards = target;
        cloneStorage.SetActive(true);

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




    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ammo")
        {
            collision.gameObject.SetActive(false);
            enemyHit(collision.gameObject.GetComponent<AmmoControler>().bulletDmg);

        }
        else if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerControler>().hitPlayer(1);
            gameObject.SetActive(false);
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
