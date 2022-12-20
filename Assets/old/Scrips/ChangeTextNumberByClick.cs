using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextNumberByClick : MonoBehaviour
{

    public TextMeshPro Textfield;
    public float targetNumber = 40;
    public float currentDisplayNumber = 80;
    private bool rightNumber = false;


    // Start is called before the first frame update
    void Start()
    {
        Textfield.text = currentDisplayNumber+"";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountUp()
    {
        currentDisplayNumber++;
        Textfield.text = currentDisplayNumber + "";
        if (currentDisplayNumber == targetNumber)
            rightNumber = true;
    }
    public void CountDown()
    {
        currentDisplayNumber--;
        Textfield.text = currentDisplayNumber + "";
        if (currentDisplayNumber == targetNumber)
            rightNumber = true;
    }
}
