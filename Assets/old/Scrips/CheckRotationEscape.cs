using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckRotationEscape : MonoBehaviour
{
    public TextMeshPro Textfield;
    public TextMeshPro Textfield1;
    public GameObject Door;

    private Vector3 oldRotation;
    private Vector3 currentRotation;
    private Vector3 doorRotation;

    private bool isRight = false;
    /*
    private float FrequenceBereichText = 100;
    private float Bereich = 10;
    private float Frequence = 360;
    private float Count = 0;
    private bool turnRight = false;
    */


    void Update()
    {
        if (currentRotation != transform.eulerAngles)
        {
            oldRotation = currentRotation;
            currentRotation = transform.eulerAngles;
            Textfield.text = Mathf.Round(currentRotation.z) + "";
            isRight = GetRotateDirection(oldRotation, currentRotation);
            if (isRight == true)
                Textfield.text = Mathf.Round(this.transform.localRotation.eulerAngles.z) + ""; 
                Mathf.Round(Door.transform.eulerAngles.z);
                
            if (isRight == false)
                Textfield1.text = "Negativ";
        }
    }


    // return true if rotating clockwise
    // return false if rotating counterclockwise
    bool GetRotateDirection(Vector3 from, Vector3 to)
    {
        float fromZ = from.z;
        float toZ = to.z;
        float clockWise = 0f;
        float counterClockWise = 0f;

        if (fromZ <= toZ)
        {
            clockWise = toZ - fromZ;
            counterClockWise = fromZ + (360 - toZ);
        }
        else
        {
            clockWise = (360 - fromZ) + toZ;
            counterClockWise = fromZ - toZ;
        }
        return (clockWise <= counterClockWise);
    } 
}



