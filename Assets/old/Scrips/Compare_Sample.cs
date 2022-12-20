using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Compare_Sample : MonoBehaviour
{
    private string SampleName1 = "Sample_1";
    private string SampleName2 = "Sample_2";
    private string SampleName3 = "Sample_3";

    public UnityEvent OnCountGreen;
    public UnityEvent OnCountOrange;
    public UnityEvent OnCountPurple;

    public UnityEvent SubtractCountCountGreen;
    public UnityEvent SubtractCountOrange;
    public UnityEvent SubtractCountPurple;



    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
     
    }

    public void CompareSampleGreen()   
    {
        Debug.Log(gameObject.name);


        if (SampleName1 == gameObject.name)
        {
            OnCountGreen.Invoke();
            Debug.Log("Green Sample Correct ");
        }
    }

    public void ExitSampleGreen()
    {
        Debug.Log(gameObject.name);


        if (SampleName1 == gameObject.name)
        {
            SubtractCountCountGreen.Invoke();
            Debug.Log("GreenExit Count -1 ");
        }
    }

    public void CompareSamplePurple()
    {
        Debug.Log(gameObject.name);


        if (SampleName2 == gameObject.name)
        {
            OnCountPurple.Invoke();
            Debug.Log("Purple Sample Correct ");

        }
    }

    public void ExitSamplePurple()
    {
        Debug.Log(gameObject.name);


        if (SampleName2 == gameObject.name)
        {
            SubtractCountPurple.Invoke();
            Debug.Log("PurpleExit Count -1 ");

        }
    }

    public void CompareSampleOrange()
    {
        Debug.Log(gameObject.name);

        if (SampleName3 == gameObject.name)
        {
            OnCountOrange.Invoke();

            Debug.Log("Orange Sample Correct ");

        }
    }

    public void ExitSampleOrange()
    {
        Debug.Log(gameObject.name);

        if (SampleName3 == gameObject.name)
        {
            SubtractCountOrange.Invoke();
            Debug.Log("OrangeExit Count -1 ");

        }
    }
}
