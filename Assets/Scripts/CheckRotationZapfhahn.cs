using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;
using System.Collections;
using UnityEngine.Events;

public class CheckRotationZapfhahn : MonoBehaviour
{
    private Vector3 currentRotation;


    public float TagetArea = 100;
    public float TagetRange = 10;

    public bool AlwaysAktive = true;
    private bool isActive = false;
    public UnityEvent onSolved;
    public UnityEvent stop;
    private float startRot;


    private void Start()
    {
        startRot = transform.localEulerAngles.x;
    }
    void Update()
    {

        if(isActive != true && AlwaysAktive==true || gameObject.GetComponent<Grabbable>()._activeTransformer != null) // If GameObject is Grabbed
        {
            //Zielbereich +-Range/2 fuer Tolleranz
            if (currentRotation.x <= (TagetArea + (TagetRange / 2)) && currentRotation.x >= (TagetArea - (TagetRange / 2)) || currentRotation.x == TagetArea)
            {
                StartCoroutine(WaitSec());
            }
            else
            {
                StopAllCoroutines();
            }

        }if(startRot == currentRotation.x && isActive == true)
        {
            Stop();
        }
        
    }

    IEnumerator WaitSec()
    {
        yield return new WaitForSeconds(2);
        Solved();
    }

    private void Solved()
    {
        StopAllCoroutines();
        isActive = true;
        //Textfield.text = "is Solved";
        onSolved.Invoke();
    }
    private void Stop()
    {
        StopAllCoroutines();
        isActive = false;
        stop.Invoke();
    }
}