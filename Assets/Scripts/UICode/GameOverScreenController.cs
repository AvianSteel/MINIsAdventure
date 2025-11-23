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
        timerText.text = TimerController.minute.ToString() + ":" + TimerController.second.ToString();
    }
}
