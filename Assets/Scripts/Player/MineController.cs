using System.Collections;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public float mineDmg;
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
            collision.gameObject.GetComponent<EnemyBehaviour>().enemyHit(mineDmg);
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
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        AudioSource.PlayClipAtPoint(mineExplosionSound, transform.position);
        spriteRenderer.color = Color.red;
        playerControler.StartCooldown("sea mine");
    }
}
