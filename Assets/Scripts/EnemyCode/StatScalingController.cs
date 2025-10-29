using System.Collections;
using UnityEngine;

public class StatScalingController : MonoBehaviour
{
    public float statScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statScale = 1f;
        StartCoroutine(StatScalingTimer());
    }
    public IEnumerator StatScalingTimer()
    {
        while (true)
        {
            statScale += 0.01f;
            yield return new WaitForSeconds(1);
        }
    }
}
