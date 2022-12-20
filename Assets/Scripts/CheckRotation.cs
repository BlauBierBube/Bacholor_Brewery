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

    private float grad = 0;

    public bool AlwaysAktive = true;
    private bool isSolved = false;

    public UnityEvent onSolved;


    private void Start()
    {
        
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
                        grad = (currentRotation.y - 180) * -0.833f;
                        Textfield.text = Mathf.Round(grad) + " °C";
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

// return true if rotating clockwise
// return false if rotating counterclockwise
bool GetRotateDirection(Vector3 from, Vector3 to)
    {
        float fromY = from.y;
        float toY = to.y;
        float clockWise = 0f;
        float counterClockWise = 0f;

        if (fromY <= toY)
        {
            clockWise = toY - fromY;
            counterClockWise = fromY + (360 - toY);
        }
        else
        {
            clockWise = (360 - fromY) + toY;
            counterClockWise = fromY - toY;
        }
        return (clockWise <= counterClockWise);
    }
}