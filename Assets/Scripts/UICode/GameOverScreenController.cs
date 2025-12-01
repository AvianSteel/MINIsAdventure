using TMPro;
using UnityEngine;

public class GameOverScreenController : MonoBehaviour
{
    [SerializeField] private TMP_Text playerScoreText;
    [SerializeField] private TMP_Text timerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScoreText.text = PlayerControler.score.ToString();
        if(TimerController.second > 10)
        {
            timerText.text = TimerController.minute.ToString() + ":" + TimerController.second.ToString();
        }
        else
        {
            timerText.text = TimerController.minute.ToString() + ":0" + TimerController.second.ToString();
        }
        
    }
}
