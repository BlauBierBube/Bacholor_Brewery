using System.Collections;
using System.Collections.Generic;
using UnityEngine;    
using UnityEngine.Events;

public class MoveToPosition : MonoBehaviour
{
    private bool trigger = false;
    private bool triggerInverse = false;

    public bool onstart = false;
    public bool ontarget = false;
    public float speed = 1f;
    public Vector3 target;
    private Vector3 startposition;
    public UnityEvent isOnStart;
    public UnityEvent isOnFinish;

    public void Start()
    {
        startposition = transform.position;
    }

    public void Update()
    {
        if (trigger == true && transform.position != target && ontarget == false)
        {

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                ///Debug.LogError("is On Finish");
                isOnFinish.Invoke();
                ontarget = true;
                onstart = false;
            }


        }
        if (triggerInverse == true && transform.position != startposition && onstart == false)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, startposition, step);
            if (Vector3.Distance(transform.position, startposition) < 0.001f)
            {
                ///Debug.LogError("is On Start");
                isOnStart.Invoke();
                onstart = true;
                ontarget = false;
            }

        }
    }
    public void Trigger()
    {
        trigger = true;
        triggerInverse = false;

    }
    public void TriggerInverse()
    {
        trigger = false;
        triggerInverse = true;

    }
}


