using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LeverRotationTarget : MonoBehaviour
{
    private float targetRotation = 322;
    public TextMeshPro Textfeld;
    public AudioSource Switch;
    public AudioSource Abdocken;
    public UnityEvent onSolved;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.localEulerAngles.x <= targetRotation && transform.localEulerAngles.x > 180 && !isActive)
        {
            Debug.Log("RotationReached.");
            LeverPulled();
        }
    }

    private void LeverPulled()
    {
        isActive = true;
        Textfeld.text = "RETTUNGSKAPSEL ERFOLGREICH GELÖST - GUTE REISE";
        Switch.Play();
        Invoke("abdock", 1f);
        onSolved.Invoke();
    }
    void abdock()
    {
        Abdocken.Play();
    }
}
