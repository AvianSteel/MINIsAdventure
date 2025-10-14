using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnControler : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject swordfish;
    [SerializeField] GameObject squid;
    [SerializeField] GameObject puffer;

    private int dice; // stores a random number

    public List<GameObject> DeadEnemies = new List<GameObject>();

    private int direction; // 1 = left screen, 2 = top screen, 3 = right screen, 4 = bottom screen       enemy will spawn in those directions
    private GameObject cloneStorage;
    private int enemyTypeChosen;        // 1 = swordfish    2 = squid    3 = pufferfish
    void Start()
    {

    }

    public void SpawnEnemy() // caled from game controler
    {
        direction = Random.Range(1, 5);

        switch (direction)
        {
            case 1:
                gameObject.transform.position = new Vector3(player.transform.position.x - 15, 
                    player.transform.position.y + Random.Range(-5,6), 0);
                break;
                
            case 2:
                gameObject.transform.position = new Vector3(player.transform.position.x + Random.Range(-10, 11), 
                    player.transform.position.y+10, 0);
                break;
            case 3:
                gameObject.transform.position = new Vector3(player.transform.position.x + 15, 
                    player.transform.position.y + Random.Range(-5, 6), 0);
                break;
            case 4:
                gameObject.transform.position = new Vector3(player.transform.position.x + Random.Range(-10, 11), 
                    player.transform.position.y - 10, 0);

                break;
        }

        if (DeadEnemies.Count > 0)
        {
            cloneStorage = DeadEnemies[0];
            cloneStorage.SetActive(true);
            cloneStorage.transform.position = transform.position;
            DeadEnemies.Remove(cloneStorage);
        }
        else
        {
            dice = Random.Range(0, 11);
            if (dice <= 6)
            {
                cloneStorage = Instantiate(swordfish, transform.position, Quaternion.identity);
                cloneStorage.name = "SwordFish";
                cloneStorage.GetComponent<EnemyBehaviour>().enemySpawn = gameObject;

                
            }
            else if(dice <= 9)
            {
                cloneStorage = Instantiate(squid, transform.position, Quaternion.identity);
                cloneStorage.name = "Squid";
                cloneStorage.GetComponent<SquidBehaviour>().enemySpawn = gameObject;
            }
            else if (dice == 10)
            {
                cloneStorage = Instantiate(puffer, transform.position, Quaternion.identity);
                cloneStorage.name = "Puffer";
                cloneStorage.GetComponent<PufferBehaviour>().enemySpawn = gameObject;
            }
        }

        



        

    }
    /// <summary>
    /// Will be callled by dead enemies in order to be placed in the list
    /// </summary>
    /// <param name="enemy"></param>
    public void listDeadEnemy(GameObject enemy)
    {
        DeadEnemies.Add(enemy);
    }

}
