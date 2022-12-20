using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;
using System.Collections;
using UnityEngine.Events;

public class CheckRotationGPS : MonoBehaviour
{
    public TextMeshPro Textfield;
    public TextMeshPro Textfield1;
    public TextMeshPro Textfield2;
    public TextMeshPro FunkFrequenz;
    public TextMeshPro AudioOffline;

    private Vector3 oldRotation;
    private Vector3 currentRotation;

    private bool isplaying = false;
    private bool isRight = false;
    private bool StartWait = false;
    private float mainVolume = 1f;
    
    
    private float FrequenceBereichText = 100;
    private float Bereich = 10;



    private float NVol = 0;
    private float TVol = 0;

    public AudioSource Noice;
    public AudioSource Text;
    public AudioSource Text1;

    //public GameObject Volume;
    public UnityEvent onSolved;
    private void Start()
    {
        Noice.volume = 1f;
        Text.volume = 0.2f;
        Text1.volume = 0;


    }
    void Update()
    {
        if (currentRotation.y <= (FrequenceBereichText + 3) && currentRotation.y >= (FrequenceBereichText - 3))
        {
            StartWait = true;
            StartCoroutine(WaitSec());
        }
        else
        {
            StartWait = false;
            StopAllCoroutines();
        }
        Textfield1.text = Mathf.Round(Noice.volume) + " V.Noice";
        Textfield2.text = Mathf.Round(Text.volume) + " V.Text";


        if (currentRotation != transform.localEulerAngles)
            if (true) //gameObject.GetComponent<Grabbable>()._activeTransformer != null)
            {
                oldRotation = currentRotation;
                currentRotation = transform.localEulerAngles;

                //mainVolume = Volume.GetComponent<CheckRotationGPSVolume>().MainVolume;

                //Textfield.text = Mathf.Round(currentRotation.y) + "";
                //Textfield1.text = Mathf.Round(transform.eulerAngles.y) + "";

                /*
                isRight = GetRotateDirection(oldRotation, currentRotation);
                if (isRight == true)
                    Textfield1.text = "Positiv";
                    Textfield1.text = Mathf.Round(this.transform.rotation.eulerAngles.y) + "";
                if (isRight == false)
                    Textfield1.text = "Negativ";
                    Textfield1.text = Mathf.Round(this.transform.rotation.eulerAngles.y) + "";
                */
                // bis hier geht es

                // 95 - 100
                // ( 95 - 100 >= 95  && 95 -100 <= 100
                if (currentRotation.y >= (FrequenceBereichText - (Bereich / 2)) && currentRotation.y <= FrequenceBereichText)
                {
                    // (( 0 - 5 ) * ( 100/ ( 10 / 2)) /100
                    //Noice.volume = (((FrequenceBereichText - currentRotation.y) * (100 / (Bereich / 2))) / 100);
                    //Text.volume = 1 - Noice.volume;

                    NVol = (((FrequenceBereichText - currentRotation.y) * (100 / (Bereich / 2))) / 100);
                    TVol = 1 - NVol;

                    Noice.volume = NVol * mainVolume;
                    Text.volume = TVol * mainVolume;

                }

                // 100 - 105
                if (currentRotation.y <= (FrequenceBereichText + (Bereich / 2)) && currentRotation.y >= FrequenceBereichText)
                {
                    // (( 0 - 5 ) * ( 100/ ( 10 / 2)) /100
                    //Noice.volume = (((currentRotation.y - FrequenceBereichText) * (100 / (Bereich / 2))) / 100);
                    //Text.volume = 1 - Noice.volume;

                    NVol = (((currentRotation.y - FrequenceBereichText) * (100 / (Bereich / 2))) / 100);
                    TVol = 1 - NVol;

                    Noice.volume = NVol * mainVolume;
                    Text.volume = TVol * mainVolume;
                }


                Textfield1.text = Mathf.Round(Noice.volume) + " V.Noice";
                Textfield2.text = Mathf.Round(Text.volume) + " V.Text";
                FunkFrequenz.text = Mathf.Round(transform.localEulerAngles.y) +" MHz";

                

            }
    }

    IEnumerator WaitSec()
    {
        yield return new WaitForSeconds(4);
        if(!isplaying)
            Auftrag();
    }

    private void Auftrag()
    {
        isplaying = true;
        StopAllCoroutines();
        Text.volume = 0;
        Noice.volume = 0;
        Text.Stop();
        Noice.Stop();
        Text1.Play();
        Text1.volume = 1;
        Textfield.text = "is Solved";
        onSolved.Invoke();
        FunkFrequenz.text = "Verbindung Hergestellt";
        AudioOffline.text = "Online";
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