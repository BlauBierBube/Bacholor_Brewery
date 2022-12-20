using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class EnterPincode : MonoBehaviour
{
    public string RightPin;
    private string EnteredPin;

    public TextMeshPro FirstNum;
    public TextMeshPro SecondNum;
    public TextMeshPro ThirdNum; 
    public TextMeshPro FourthNum;


    public string EnteredNum1;
    public string EnteredNum2;
    public string EnteredNum3;
    public string EnteredNum4;

    public UnityEvent onSolved;

    public void EnterFirstNum(string EnteredNum1)
    {
        FirstNum.text = EnteredNum1;
    }

    public void EnterSecondNum(string EnteredNum2)
    {
        SecondNum.text = EnteredNum2;
    }

    public void EnterThirdNum(string EnteredNum3)
    {
        ThirdNum.text = EnteredNum3;
    }

    public void EnterFourthNum(string EnteredNum4)
    {
        FourthNum.text = EnteredNum4;
    }

    private void EnteredCode()
    {
        EnteredPin = EnteredNum1 + EnteredNum2 + EnteredNum3 + EnteredNum4;
        Debug.Log(this.gameObject + EnteredPin);
    }
    // Update is called once per frame
    void Update()
    {
        if(EnteredPin == RightPin)
        {
            onSolved.Invoke();
        }

        else
        {
            
        }

        
    }
}
