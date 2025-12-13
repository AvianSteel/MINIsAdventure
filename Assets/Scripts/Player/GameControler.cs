using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameControler : MonoBehaviour
{
    public List<GameObject> deadEnemies = new List<GameObject>();
    public int spawnEnemies;
    public float timeBetweenSpawn;
    public float enemySpawnScaling;
    [SerializeField] GameObject enemySpawner;

    private void Start()
    {
        StartCoroutine(callSpawn());
    }


    private IEnumerator callSpawn()
    {

        enemySpawner.GetComponent<EnemySpawnControler>().SpawnEnemy();

        yield return new WaitForSeconds(timeBetweenSpawn);
        if (timeBetweenSpawn > 0.05)
        {
            timeBetweenSpawn -= enemySpawnScaling;

        }
        else
        {
            timeBetweenSpawn = 0.05f;
        }
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
