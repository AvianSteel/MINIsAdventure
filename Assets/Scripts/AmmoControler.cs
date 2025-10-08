using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AmmoControler : MonoBehaviour
{
    public bool isInk;
    public GameObject targetToMoveTowards;
    public float bulletDmg; // how much dmg the bullet has, inherited from player controler, and will be accesed by the enemy when it colides
    public float speed; //0 - 10
    public GameObject ZoneHost; // the triger zone that summoned the bullet

    void Start()
    {
        Vector2 direction = targetToMoveTowards.transform.position - transform.position;
        direction.Normalize(); // Keep velocity consistent

        gameObject.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
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
        bulletGetsOld();
    }

    public void bulletGetsOld()
    {
        ZoneHost.GetComponent<TargetZoneControler>().ListOldBullet(gameObject);
        gameObject.SetActive(false);
    }


}
