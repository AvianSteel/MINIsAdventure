using UnityEngine;

// the code does not spawn the drops anymore, it is handled by the enemy controler
// the drop itself needs to colide with the player and not the other way around in order to not colide with the triger zone
public class StatDropController : MonoBehaviour
{
    private int randomStat;

    public float speedBonus;
    public float attackSpeedBonus;
    public float abilitySpeedBonus;
    public int defenseBonus;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            randomStat = Random.Range(0, 3);
            if(randomStat == 0)
            {
                collision.gameObject.GetComponent<PlayerControler>().increaseSpeed(speedBonus);
            }else if(randomStat == 1)
            {
                collision.gameObject.GetComponent<PlayerControler>().increaseAttackSpeed(attackSpeedBonus);
            }
            else if(randomStat == 2)
            {
                collision.gameObject.GetComponent<PlayerControler>().increaseAbilitySpeed(abilitySpeedBonus);
            }
            else if(randomStat == 3)
            {
                collision.gameObject.GetComponent<PlayerControler>().increaseDefense(defenseBonus);
            }
                Destroy(gameObject);
        }
    }
}
