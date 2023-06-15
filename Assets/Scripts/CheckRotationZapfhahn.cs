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
    public bool notGrabbed = false;
    private bool isActive = false;
    public UnityEvent onSolved;
    public UnityEvent stop;
    private float startRot;

    private bool rotBack = false;
    private Quaternion startRotation;
    public Quaternion targetRotation;
    public float rotationDuration = 2.0f; // Adjust the duration as per your requirement
    private float rotationTimer = 0.0f;

    private void Start()
    {
        startRotation = transform.rotation;
        startRot = transform.localEulerAngles.x;

    }
    void Update()
    {

        //Debug.LogError(currentRotation.x);
        if (AlwaysAktive==true || gameObject.GetComponent<Grabbable>()._activeTransformer != null) // If GameObject is Grabbed
        {
            //Zielbereich +-Range/2 fuer Tolleranz
            if (currentRotation.x <= (TagetArea + (TagetRange / 2)) && currentRotation.x >= (TagetArea - (TagetRange / 2)) && isActive == false || currentRotation.x == TagetArea)
            {
                //Debug.LogError("in if");
                Solved();
                //StartCoroutine(WaitSec());
            }
            //Zielbereich +-Range/2 fuer Tolleranz
            if (currentRotation.x <= (TagetArea - (TagetRange / 2)) && isActive==true || currentRotation.x == startRot)
            {
                //Debug.LogError("in Else");
                Stop();
                //StartCoroutine(WaitSec());
            }

        }



        /*
        if (AlwaysAktive == false)// || gameObject.GetComponent<Grabbable>()._activeTransformer == null)
        {
            StartCoroutine(RotateBack());
            if (currentRotation.x == startRot)
                rotBack = false;
        }
        */

        //currentRotation.x <= (TagetArea - (TagetRange / 2)) || currentRotation.x == startRot
        if (currentRotation != transform.localEulerAngles)
        {
            currentRotation = transform.localEulerAngles;
        }

        if (currentRotation.x != startRotation.x)
        {
            if (notGrabbed==true && AlwaysAktive == true)// || gameObject.GetComponent<Grabbable>()._activeTransformer == null)
            {
                if (rotBack == false)
                {
                    RotateBack();
                }
                //Debug.LogError("RotateBack");
                rotationTimer += Time.deltaTime;
                float t = rotationTimer / rotationDuration;
                transform.rotation = Quaternion.Slerp(targetRotation, startRotation, t);
            }
            //Debug.LogError("RotateBack");
        }
        if (currentRotation.x == startRotation.x)
        {
            rotBack = false;
        }
    }
    
    void RotateBack()
    {
        rotBack = true;
        rotationTimer = 0.0f;
        targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    IEnumerator WaitSec()
    {
        yield return new WaitForSeconds(0.1f);
        Solved();
    }

    private void Solved()
    {
        StopAllCoroutines();


        if (isActive==false)
        {
            //Debug.LogError("Solved");
            isActive = true;
            onSolved.Invoke();
        }

    }
    private void Stop()
    {
        StopAllCoroutines();
        if (isActive==true)
        {
            //Debug.LogError("Stop");
            isActive = false;
            stop.Invoke();
        }
    }
}