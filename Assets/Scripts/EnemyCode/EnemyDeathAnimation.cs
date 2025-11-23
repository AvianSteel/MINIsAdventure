using System.Collections;
using UnityEngine;

public class EnemyDeathAnimation : MonoBehaviour
{
    public int enemyType; // 1 = sword, 2 = squid, 3 = puff
    Rigidbody2D rb;
    SpriteRenderer sr;
    private Color c;

    [SerializeField] private Collider2D col1;
    [SerializeField] private Collider2D col2;

    [SerializeField] private GameObject animSides;
    [SerializeField] private GameObject animUp;
    [SerializeField] private GameObject animDown;
    [SerializeField] private GameObject animLounge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
  
    }

    public void startDeathBehaviour(int enemyNum)
    {
        //print("whywhywhywhy");
        StartCoroutine(FadeOut());
    }

    IEnumerator SwordDeath(float deathDuration)
    {

        float alpha = 1.0f;
        float elapsed = 0f;

        rb = gameObject.GetComponent<Rigidbody2D>();
        //  rb.simulated = false; 

        col1.enabled = false;
        col2.enabled = false;


        // Grow
        while (alpha > 0.0)
        {
            Color cl;




            alpha -= 0.01f;
            elapsed += Time.deltaTime;
            float t = elapsed / deathDuration;

            sr = animSides.GetComponent<SpriteRenderer>();
            cl = sr.color;
            cl.a = alpha;
            sr.color = cl;

            sr = animUp.GetComponent<SpriteRenderer>();
            cl = sr.color;
            cl.a = alpha;
            sr.color = cl;

            sr = animDown.GetComponent<SpriteRenderer>();
            cl = sr.color;
            cl.a = alpha;
            sr.color = cl;

            sr = animLounge.GetComponent<SpriteRenderer>();
            cl = sr.color;
            cl.a = alpha;
            sr.color = cl;
            print(sr.color);

            //           yield return null;

        }
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<EnemyBehaviour>().enemySpawn.GetComponent<EnemySpawnControler>().listDeadEnemy(gameObject);
        gameObject.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        //print("!!!!!!!!!!");

        float startAlpha;
        float t = 0;
        float alpha;
        col1.enabled = false;
        col2.enabled = false;
        //print("22222222222");
        
        while (t < 15)
        {

            t += Time.deltaTime/2;
            

            

            sr = animSides.GetComponent<SpriteRenderer>();
            startAlpha = sr.color.a;
            alpha = Mathf.Lerp(startAlpha, 0f, t / 5);
            c = sr.color;
            c.a = alpha;
            sr.color = c;


            sr = animUp.GetComponent<SpriteRenderer>();
            startAlpha = sr.color.a;
            alpha = Mathf.Lerp(startAlpha, 0f, t / 5);
            c = sr.color;
            c.a = alpha;
            sr.color = c;

            sr = animDown.GetComponent<SpriteRenderer>();
            startAlpha = sr.color.a;
            alpha = Mathf.Lerp(startAlpha, 0f, t / 5);
            c = sr.color;
            c.a = alpha;
            sr.color = c;

            sr = animLounge.GetComponent<SpriteRenderer>();
            startAlpha = sr.color.a;
            alpha = Mathf.Lerp(startAlpha, 0f, t / 5);
            c = sr.color;
            c.a = alpha;
            sr.color = c;

            yield return null;
        }

        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<EnemyBehaviour>().enemySpawn.GetComponent<EnemySpawnControler>().listDeadEnemy(gameObject);
        gameObject.SetActive(false);
    }
}