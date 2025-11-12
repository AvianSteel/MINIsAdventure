using UnityEngine;

/// <summary>
/// this code will control the chosing of abilities and pause the game while dooing so, speaks with 
/// game controler with functions and is called by the ability drop accesing player's function
/// </summary>
public class ChooseAbility : MonoBehaviour
{
    [SerializeField] private GameObject lasser;
    [SerializeField] private GameObject mine;
    [SerializeField] private GameObject mine2; // same thing but on the 2nd slot
    [SerializeField] private GameObject dash;
    private int randomHolder; // holds a random value, used to calculate which ability will be unvalible;
    public GameObject player;



    public void giveAChoice()
    {
        player.GetComponent<PlayerControler>().isChoosingAbility = true;

        lasser.SetActive(true);
        mine.SetActive(true);
        mine2.SetActive(true);
        dash.SetActive(true);
        randomHolder = Random.Range(1, 4);

        if (randomHolder == 1)
        {
            lasser.SetActive(false); 
            mine2.SetActive(false);
        }
        else if (randomHolder == 2)
        {
            mine.SetActive(false);
            mine2.SetActive(false);

        }
        else if (randomHolder == 3)
        {
            dash.SetActive(false);
            mine.SetActive(false);

        }
        player.GetComponent<PlayerControler>().exceptionChoice = randomHolder; // this ability can't be chosen
        Time.timeScale = 0f; // Pauses the game
    }

    public void closeChoice()
    {
        Time.timeScale = 1f; // unpauses the game
        player.GetComponent<PlayerControler>().isChoosingAbility = false;
        gameObject.SetActive(false);

    }


}
