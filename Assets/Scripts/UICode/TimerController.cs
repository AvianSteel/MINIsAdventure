using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private int minute;
    private int second;
    public TMP_Text timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        second = 0;
        minute = 0;
        timer.text = minute.ToString() + ":00";
        StartCoroutine(TimerCount());
    }

    IEnumerator TimerCount()
    {
        while(true)
        {
            second++;
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
