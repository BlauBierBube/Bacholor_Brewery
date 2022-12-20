using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class EscapePod_Rotation : MonoBehaviour
{
    private float targetRotation = 1800;
    public Light Led_1;
    public Light Led_2;
    public Light Led_3;
    public Light Led_4;
    public Light Led_5;



    public UnityEvent onSolved;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Mathf.Round(transform.eulerAngles.y) >= 360)
        {
            Led_1.color = new Color32(254, 9, 0, 255);
        }

        if (Mathf.Round(transform.eulerAngles.y) >= 720)
        {
            Led_2.color = new Color32(254, 9, 0, 255);
        }

        if (Mathf.Round(transform.eulerAngles.y) >= 1080)
        {
            Led_3.color = new Color32(254, 9, 0, 255);
        }

        if (Mathf.Round(transform.eulerAngles.y) >= 1440)
        {
            Led_4.color = new Color32(254, 9, 0, 255);
        }

        if (Mathf.Round(transform.eulerAngles.y) >= targetRotation)
        {
            Led_5.color = new Color32(254, 9, 0, 255);
            Debug.LogError(targetRotation + "Ziel Winkel erreicht");
            onSolved.Invoke();
        }
    }
}
