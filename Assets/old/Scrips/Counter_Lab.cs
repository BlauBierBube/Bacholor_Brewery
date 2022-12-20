using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Counter_Lab : MonoBehaviour
{
    private int Count;
    private bool isActive = false;
    public AudioSource SprecherEnding;
    public float Time = 10f;
    public UnityEvent OnSolved;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Count == 3 && !isActive)
        {
            FinalCount();
            
        }
    }

    private void FinalCount()
    {
        isActive = true;
        OnSolved.Invoke();
        Invoke("SprecherAudio", Time);
    }

    
    private void SprecherAudio()
    {
        SprecherEnding.Play();
    }

    public void AddCount()
    {
        Count = Count + 1;
    }

    public void SubtractCount()
    {
        Count = Count - 1;
    }
}
