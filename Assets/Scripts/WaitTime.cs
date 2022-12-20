using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitTime : MonoBehaviour
{
    public UnityEvent onSolved;

    public float waitTime = 5f;
    private float T = 5f;

    public void StartTimer(float T)
    {
        waitTime = T;
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        onSolved.Invoke();
    }
}
