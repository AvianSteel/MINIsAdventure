using System.Collections;
using UnityEngine;

public class StatScalingController : MonoBehaviour
{
    public TimerController timerController;
    public float statScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerController = (TimerController)GameObject.FindWithTag("Canvas").GetComponent("TimerController");
        statScale = timerController.statScaleGlobal;
    }
    private void Update()
    {
        statScale = timerController.statScaleGlobal;
    }
}
