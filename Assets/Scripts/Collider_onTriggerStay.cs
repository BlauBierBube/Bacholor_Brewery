using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collider_onTriggerStay : MonoBehaviour
{
    public UnityEvent OnStay;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private void Start()
    {

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Fluid"))
        {
            OnStay.Invoke();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(this.gameObject + "Collision");
        if (other.gameObject.CompareTag("Fluid"))
        {
            OnEnter.Invoke();
        }
    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log(this.gameObject + "No Collision");
        if (other.gameObject.CompareTag("Fluid"))
        {
            OnExit.Invoke();
        }
    }
}

