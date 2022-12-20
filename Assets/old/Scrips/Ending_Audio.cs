using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending_Audio : MonoBehaviour
{
    public AudioSource Sprecher;
    public float Time = 4f;
    public float Time_Credits = 8f;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndingAudio()
    {
        Invoke("TriggerAudio", Time);
    }

    private void TriggerAudio()
    {
        Sprecher.Play();
    }


    public void Credits()
    {
        Invoke("RollCredits", Time_Credits);
    }


    private void RollCredits()
    {
        anim.SetBool("GameEnd", true);
    }
}
