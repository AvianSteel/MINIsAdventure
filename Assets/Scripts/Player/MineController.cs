using System.Collections;
using UnityEngine;

public class MineController : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticle;

    public float initiaLiefetime;
    public float mineDmg;
    private float explRadious;
    private PlayerControler playerControler;

    [SerializeField] private AudioClip mineExplosionSound;
    [SerializeField] private AudioClip hitSound;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControler = GameObject.Find("Player").gameObject.GetComponent<PlayerControler>();
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(MineCountdown());
    }

    /// <summary>
    /// Starts a countdown for the mine. It is inactive for 2.5 seconds, then has a large collider for the next
    /// 0.5 seconds before being destroyed
    /// </summary>
    /// <returns></returns>
    private IEnumerator MineCountdown()
    {
        mineDmg = playerControler.GetComponent<PlayerControler>().mineLvl; // higher lvl, higher dmg
        for (int i = 0; i < 6; i++)
        {
            
            if (i >= 5)
            {
                Explode();
            }
            yield return new WaitForSeconds(0.5f);
        }
      
        Destroy(gameObject);
    }

    /// <summary>
    /// Deals damage to enemies when they enter the mine explosion collider
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SwordFish")
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().EnemyHit(mineDmg);
        }
        else if (collision.gameObject.name == "Squid")
        {
            collision.gameObject.GetComponent<SquidBehaviour>().enemyHit(mineDmg);

        }
        else if (collision.gameObject.name == "Puffer")
        {
            collision.gameObject.GetComponent<PufferBehaviour>().enemyHit(mineDmg);

        }
    }
/// <summary>
/// Enables the mine collider, turning it red for now
/// Then starts the cooldown
/// </summary>
    private void Explode()
    {
        explRadious = playerControler.GetComponent<PlayerControler>().mineLvl; // higher ability lvl, bigger explosion
        ParticleSystem ps = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        var main = ps.main;          // get the main module
        if (explRadious < 4)
        {
            main.startLifetime = initiaLiefetime / explRadious;   // set lifetime to 2.5 seconds
        }


        gameObject.GetComponent<CircleCollider2D>().radius = explRadious; // higher ability lvl, bigger explosion
        main.startSpeed = explRadious;

        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        AudioSource.PlayClipAtPoint(mineExplosionSound, transform.position);
        spriteRenderer.color = Color.red;
        playerControler.StartCooldown("sea mine");
    }
}
