using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;
using System.Collections;
public class GameControler : MonoBehaviour
{
    public List<GameObject> deadEnemies = new List<GameObject>();
    public int spawnEnemies;
    public float timeBetweenSpawn;
    [SerializeField] GameObject enemySpawner;

    private void Start()
    {
        StartCoroutine(callSpawn());
    }


    private IEnumerator callSpawn()
    {

        enemySpawner.GetComponent<EnemySpawnControler>().SpawnEnemy();

        yield return new WaitForSeconds(timeBetweenSpawn);
        repeatSpawn();
    }


    private void repeatSpawn()
    {
        if (spawnEnemies > 0)
        {
            spawnEnemies--;
            StartCoroutine(callSpawn());

        }
    }

}
