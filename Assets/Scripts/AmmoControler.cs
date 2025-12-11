using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AmmoControler : MonoBehaviour
{
    public GameObject squidParent; // only used if the bullet is ink shot by squid
    public bool isInk;
    public GameObject targetToMoveTowards;
    public float bulletDmg; // how much dmg the bullet has, inherited from player controler, and will be accesed by the enemy when it colides
    public float speed; //0 - 10
    public GameObject ZoneHost; // the triger zone that summoned the bullet

    void Start()
    {

       

     //   Vector2 direction = targetToMoveTowards.transform.position - transform.position;
      //  direction.Normalize(); // Keep velocity consistent
     
      //  gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
    }

    private void OnEnable()
    {
        StartCoroutine(BulletLife());
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // reset ammo velocity
        if (!isInk)
        {
            gameObject.transform.right = targetToMoveTowards.transform.position - transform.position; // make ammo face fish???
            Vector2 direction = targetToMoveTowards.transform.position - transform.position;
            direction.Normalize(); // Keep velocity consistent

            gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        }
        
    }
    private void OnDisable()
    {
         gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // reset ammo velocity

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && isInk)
        {
            collision.gameObject.GetComponent<PlayerControler>().hitPlayer(1);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(7);
        if (isInk)
        {
            bulletGetsOld();
        }
        
            

        
    }

    public void bulletGetsOld()
    {if (!isInk)
        {
            ZoneHost.GetComponent<TargetZoneControler>().ListOldBullet(gameObject);
            gameObject.SetActive(false);
        }
    else
        {
            squidParent.GetComponent<SquidBehaviour>().enemySpawn.GetComponent<EnemySpawnControler>().listInk(gameObject);
            gameObject.SetActive(false);
        }
        
    }
 


}
