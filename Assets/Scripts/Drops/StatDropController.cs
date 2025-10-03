using UnityEngine;

// the code does not spawn the drops anymore, it is handled by the enemy controler
// the drop itself needs to colide with the player and not the other way around in order to not colide with the triger zone
public class StatDropController : MonoBehaviour
{

    public float speedBonus;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerControler>().increaseSpeed(speedBonus);
          Destroy(gameObject);
        }
    }
}
