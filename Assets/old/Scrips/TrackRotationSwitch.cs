using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TrackRotationSwitch : MonoBehaviour
{
    public float targetRotation = 100;
    public TextMeshPro Textfield;

    public bool loop = false;

    public UnityEvent onSolved;

    // Start is called before the first frame update
    void Start()
    {
        Textfield.text = Mathf.Round(transform.eulerAngles.y) + "";
    }

    // Update is called once per frame
    void Update()
    {
        if (loop == true)
        {
            //Debug.LogError("Rutine Gestartet");
            Textfield.text = Mathf.Round(transform.eulerAngles.y) + "";
            //Wenn der Y Winkel der Zielwinkel ist, dann mach..
            if (Mathf.Round(transform.eulerAngles.y) == targetRotation)
            {
                Debug.LogError(targetRotation + "Ziel Winkel erreicht");
                onSolved.Invoke();
            }
            //Wenn der Y Winkel kleiner oder Grˆﬂer als der Zielwinkel ist dann mach ...
            else if (Mathf.Round(transform.eulerAngles.y) > targetRotation)
            {
                //Debug.LogError(Mathf.Round(transform.eulerAngles.y) + " ist noch zu groﬂ");
            }
            else if (transform.eulerAngles.y < targetRotation)
            {
                //Debug.LogError(Mathf.Round(transform.eulerAngles.y) + " ist noch zu klein");
            }
        }
    }

    public void StopLoop()
    {
        loop = false;

    }
    public void StartLoop()
    {
        loop = true;
    }
   
}
