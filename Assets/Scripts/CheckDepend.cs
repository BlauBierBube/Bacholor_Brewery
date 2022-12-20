using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckDepend : MonoBehaviour
{

    private float x = 0;
    public float Anzahl = 0;

    public UnityEvent onSolved;


    public void CheckUp()
    {
        x++;
    }

    // Update is called once per frame
    void Update()
    {
        if (x == Anzahl)
        {
            onSolved.Invoke();
        }
    }





}