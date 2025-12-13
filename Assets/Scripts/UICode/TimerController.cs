using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public static int minute;
    public static int second;
    public TMP_Text timer;
    public float statScaleGlobal;
    private float secPassed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        secPassed = 5;
        second = 0;
        minute = 0;
        statScaleGlobal = 1f;
        timer.text = minute.ToString() + ":00";
        StartCoroutine(TimerCount());
    }

    IEnumerator TimerCount()
    {
        while(true)
        {
            second++;
            secPassed += 0.05f;
            statScaleGlobal = (0.05f * (Mathf.Pow(secPassed, 2) + 1));
            //Debug.Log("Timer Scale: " + statScaleGlobal.ToString());
            if(second == 60)
            {
                second = 0;
                minute++;
            }
            yield return new WaitForSeconds(1);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(second < 10)
        {
            timer.text = minute.ToString() + ":0" + second.ToString();
        }
        else
        {
           timer.text = minute.ToString() + ":" + second.ToString();
        }
           
    }
}
