using UnityEngine;

// the code does not spawn the drops anymore, it is handled by the enemy controler
// the drop itself needs to collide with the player and not the other way around in order to not colide with the triger zone
public class StatDropController : MonoBehaviour
{
    private int randomStat;

    public float speedBonus;
    public float attackSpeedBonus;
    public float abilitySpeedBonus;
    public int defenseBonus;
    [SerializeField] private bool uloksAbility; // if this is checked this drop will add a new ability to the player and won't affect stats

    [SerializeField] private AudioClip pickUpSound;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            AudioSource.PlayClipAtPoint(pickUpSound, transform.position);

            if (uloksAbility)
            {
                
                    randomStat = Random.Range(1, 4);
                    if (randomStat == 1)
                    {
                        print("laser lvl+");
                        collision.gameObject.GetComponent<PlayerControler>().laserLvl += 1;
                    }
                    else if (randomStat == 2)
                    {
                        print("mine lvl+");
                        collision.gameObject.GetComponent<PlayerControler>().mineLvl += 1;

                    }
                    else if (randomStat == 3)
                    {
                        print("dash lvl+");
                        collision.gameObject.GetComponent<PlayerControler>().dashLvl += 1;

                    }
                
                Destroy(gameObject);

            }
            else
            {
                randomStat = Random.Range(0, 4);
                if (randomStat == 0)
                {
                    collision.gameObject.GetComponent<PlayerControler>().increaseSpeed(speedBonus);
                }
                else if (randomStat == 1)
                {
                    collision.gameObject.GetComponent<PlayerControler>().increaseAttackSpeed(attackSpeedBonus);
                }
                else if (randomStat == 2)
                {
                    collision.gameObject.GetComponent<PlayerControler>().increaseAbilitySpeed(abilitySpeedBonus);
                }
                else if (randomStat == 3)
                {
                    collision.gameObject.GetComponent<PlayerControler>().increaseDefense(defenseBonus);
                }
                Destroy(gameObject);
            }              
        }
    }
}
