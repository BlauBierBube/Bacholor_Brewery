using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Power_Switch : MonoBehaviour
{
    private float targetRotation = 325;
    public TextMeshPro Textfeld;

    public Material material;




    private bool isActiv = false;
    public UnityEvent onSolved;

    // Start is called before the first frame update
    void Start()
    {
        Textfeld.text = "Power Off";
        material.SetColor("_EmissionColor", new Color(0.9622641f, 0.231488f, 0.231488f, 1));
    }

    private void Update()
    {
        //Debug.LogError(transform.eulerAngles.x);
        if (transform.eulerAngles.x <= targetRotation && transform.eulerAngles.x >= 100 && isActiv == false)
        {
            PowerOn();
        }
    }
    private void PowerOn()
    {
        Textfeld.text = "Power On";
        //Debug.LogError("Power ON");


        onSolved.Invoke();
        isActiv = true;

        material.SetColor("_EmissionColor", new Color(0.3361072f, 0.7830188f, 0.3612165f, 1));

    }
}
