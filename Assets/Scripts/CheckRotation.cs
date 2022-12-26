using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;
using System.Collections;
using UnityEngine.Events;

public class CheckRotation : MonoBehaviour
{
    private Vector3 oldRotation;
    private Vector3 currentRotation;

    [SerializeField] TextMeshPro Textfield;
    [SerializeField] GameObject Needle;

    public float TagetArea = 100;
    public float TagetRange = 10;

    public bool AlwaysAktive = true;
    private bool isSolved = false;
    public UnityEvent onSolved;
    private float startRot;


    private void Start()
    {
        startRot = transform.localEulerAngles.y;
    }
    void Update()
    {

        if(isSolved != true && AlwaysAktive==true || gameObject.GetComponent<Grabbable>()._activeTransformer != null) // If GameObject is Grabbed
        {
            //Zielbereich +-Range/2 fuer Tolleranz
            if (currentRotation.y <= (TagetArea + (TagetRange / 2)) && currentRotation.y >= (TagetArea - (TagetRange / 2)) || currentRotation.y == TagetArea)
            {
                StartCoroutine(WaitSec());
            }
            else
            {
                StopAllCoroutines();
            }

            if (currentRotation != transform.localEulerAngles)
                {
                    oldRotation = currentRotation;
                    currentRotation = transform.localEulerAngles;

                    if (Needle != null)
                        Needle.transform.localEulerAngles = currentRotation;

                    if (Textfield != null)
                    {
                        Textfield.text = Mathf.Round((currentRotation.y - 180) * -0.833f) + " °C";
                    }
                }
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
        isSolved = true;
        //Textfield.text = "is Solved";
        onSolved.Invoke();
    }
}