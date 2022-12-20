using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collider_isTrigger : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private void Start()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(this.gameObject + "Collision");
        if (other.gameObject.CompareTag("Player"))
        {
            OnEnter.Invoke();
        }
    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log(this.gameObject + "No Collision");
        if (other.gameObject.CompareTag("Player"))
        {
            OnExit.Invoke();
        }
    }
}

