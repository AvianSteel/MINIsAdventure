using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    private PlayerControler playerControler;
    private float moveDirection; // +Up / -Down
    private float slideDirection; // -Left / +Right
    [SerializeField] private GameObject laserParent;
    [SerializeField] private Rigidbody2D rb;
    public float laserDirection;

    private Rigidbody2D playerRb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    private IEnumerator LaserWiggle()
    {
        for(int i = 0; i < 45; i++)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().enemyDie();
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = playerControler.moveDirection;
        slideDirection = playerControler.slideDirection;
        rb.position = new Vector2(playerRb.position.x, playerRb.position.y);
        rb.rotation = laserDirection;
    }
}
