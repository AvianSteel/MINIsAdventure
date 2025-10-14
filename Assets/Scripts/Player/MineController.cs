using System.Collections;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public float mineDmg;
    private PlayerControler playerControler;

    [SerializeField] private AudioClip mineExplosionSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControler = GameObject.Find("Player").gameObject.GetComponent<PlayerControler>();
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(MineCountdown());
    }

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
       
        AudioSource.PlayClipAtPoint(mineExplosionSound, transform.position);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().enemyHit(mineDmg);
        }
    }

    private void Explode()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
        playerControler.StartCooldown("sea mine");
    }
}
