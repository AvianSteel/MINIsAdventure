using UnityEngine;

public class EnemySpawnControler : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject swordfish;
    private int direction; // 1 = left screen, 2 = top screen, 3 = right screen, 4 = bottom screen       enemy will spawn in those directions
    private GameObject cloneStorage;
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

        cloneStorage = Instantiate(swordfish, transform.position, Quaternion.identity);
        cloneStorage.name = "Enemy";
            
        
    }
}
