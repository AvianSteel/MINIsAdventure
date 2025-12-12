using System.Collections;
using UnityEngine;


public class LaserController : MonoBehaviour
{
    private PlayerControler playerControler;
    private float moveDirection; // +Up / -Down
    private float slideDirection; // -Left / +Right
    [SerializeField] private GameObject laserParent;
    [SerializeField] private Rigidbody2D rb;
    public float laserDirection;
    public float laserDmg;

    [SerializeField] private AudioClip hitLaserSound; 

    private Rigidbody2D playerRb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /// <summary>
    /// Gets the players facing direction to be pointing in before starting the coroutine to move it
    /// </summary>
    void Start()
    {
        playerControler = GameObject.Find("Player").gameObject.GetComponent<PlayerControler>();
        playerRb = GameObject.Find("Player").gameObject.GetComponent<Rigidbody2D>();
        moveDirection = playerControler.moveDirection;
        slideDirection = playerControler.slideDirection;

        if(moveDirection > 0)
        {
            laserDirection = -90f;
        }else if (moveDirection < 0)
        {
            laserDirection = 90f;
        }
        if (slideDirection > 0)
        {
            laserDirection = 180f;
        }else if (slideDirection < 0)
        {
            laserDirection = 0f;
        }
        StartCoroutine(LaserWiggle());

    }
    /// <summary>
    /// Rotates the laser up and down in the initial direction it was activated.
    /// </summary>
    /// <returns></returns>
    private IEnumerator LaserWiggle()
    {
        AudioSource.PlayClipAtPoint(hitLaserSound, transform.position);
        laserDmg = playerControler.GetComponent<PlayerControler>().laserLvl; // higher ability lvl, higher dmg
        laserDmg = Mathf.Round(Mathf.Log(0.2f * laserDmg) + 5.5f);
        for (int i = 0; i < 45; i++)
        {
            laserDirection++;
            yield return new WaitForSeconds(0.01f);
        }
        for(int i = 0;i < 90; i++)
        {
            laserDirection--;
            yield return new WaitForSeconds(0.01f);
        }
       
        playerControler.StartCooldown("laser");
        StopCoroutine(LaserWiggle());
        Destroy(laserParent);
    }
    /// <summary>
    /// Deals damage to any enemy which gets hit by the laser
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SwordFish")
        {
            collision.gameObject.GetComponent<SwordfishBehaviour>().EnemyHit(laserDmg);

        }
        else if (collision.gameObject.name == "Squid")
        {
            collision.gameObject.GetComponent<SquidBehaviour>().enemyHit(laserDmg);

        }
        else if (collision.gameObject.name == "Puffer")
        {
            collision.gameObject.GetComponent<PufferBehaviour>().enemyHit(laserDmg);

        }
    }

    /// <summary>
    /// Moves the laser alongside the player
    /// </summary>
    void Update()
    {
        moveDirection = playerControler.moveDirection;
        slideDirection = playerControler.slideDirection;
        rb.position = new Vector2(playerRb.position.x, playerRb.position.y);
        rb.rotation = laserDirection;
    }
}
