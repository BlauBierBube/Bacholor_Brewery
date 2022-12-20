using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;
using System.Collections;

public class CheckRotationGPSVolume : MonoBehaviour
{
    public TextMeshPro Textfield;

    private Vector3 oldRotation;
    private Vector3 currentRotation;


    private bool isRight = false;
    public float MainVolume = 1f;
    public float Value = 0.05f;
   
    private bool turnRight = false;

    public AudioSource Noice;
    public AudioSource Text;


    void Update()
    {
        if (currentRotation != transform.eulerAngles)
            if (true)//gameObject.GetComponent<Grabbable>()._activeTransformer != null)
            {

                oldRotation = currentRotation;
                currentRotation = transform.eulerAngles;


                isRight = GetRotateDirection(oldRotation, currentRotation);
                if (isRight == true && MainVolume < 1) // Rechts
                {
                    MainVolume = MainVolume + Value;
                }

                if (isRight == false && MainVolume > 0) // Links
                {
                    MainVolume = MainVolume - Value;
                }
                
                Textfield.text = MainVolume + " MainVolume";

                Noice.volume = Noice.volume + MainVolume;
                Text.volume = Text.volume + MainVolume;
            }
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