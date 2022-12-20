using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChangeText_3Numbers : MonoBehaviour
{

    public TextMeshPro Textfield_Top;
    public TextMeshPro Textfield_Mid;
    public TextMeshPro Textfield_Bot;
    public TextMeshPro GPSOffline;

    public float targetNumber_01 = 40;
    public float targetNumber_02 = 40;
    public float targetNumber_03 = 40;

    public float currentDisplayNumber_01 = 80;
    public float currentDisplayNumber_02 = 80;
    public float currentDisplayNumber_03 = 80;

    private bool check_01 = false;
    private bool check_02 = false;
    private bool check_03 = false;

    public UnityEvent onSolved;



    // Start is called before the first frame update
    void Start()
    {
        Textfield_Top.text = currentDisplayNumber_01+"";
        Textfield_Mid.text = currentDisplayNumber_02 + "";
        Textfield_Bot.text = currentDisplayNumber_03 + "";
    }



    public void Solved()
    {
        if (check_01 && check_02 && check_03 == true)
        {
            onSolved.Invoke();
            GPSOffline.text = "Online";
            Debug.Log(this.gameObject.name + " is Solved");
        }
    }

    public void CountUp_01()
    {
        currentDisplayNumber_01++;
        Textfield_Top.text = currentDisplayNumber_01 + "";
        if (currentDisplayNumber_01 == targetNumber_01)
        {
            check_01 = true;
            Solved();
        }
        else
            check_01 = false;
    }
    public void CountDown_01()
    {
        currentDisplayNumber_01--;
        Textfield_Top.text = currentDisplayNumber_01 + "";
        if (currentDisplayNumber_01 == targetNumber_01)
        {
            check_01 = true;
            Solved();
        }
        else
            check_01 = false;
    }
    public void CountUp_02()
    {
        currentDisplayNumber_02++;
        Textfield_Mid.text = currentDisplayNumber_02 + "";
        if (currentDisplayNumber_02 == targetNumber_02)
        {
            check_02 = true;
            Solved();
        }
        else
            check_02 = false;
    }
    public void CountDown_02()
    {
        currentDisplayNumber_02--;
        Textfield_Mid.text = currentDisplayNumber_02 + "";
        if (currentDisplayNumber_02 == targetNumber_02)
        {
            check_02 = true;
            Solved();
        }
        else
            check_02 = false;
    }
    public void CountUp_03()
    {
        currentDisplayNumber_03++;
        Textfield_Bot.text = currentDisplayNumber_03 + "";
        if (currentDisplayNumber_03 == targetNumber_03)
        {
            check_03 = true;
            Solved();
        }
        else
            check_03 = false;
    }
    public void CountDown_03()
    {
        currentDisplayNumber_03--;
        Textfield_Bot.text = currentDisplayNumber_03 + "";
        if (currentDisplayNumber_03 == targetNumber_03)
        {
            check_03 = true;
            Solved();
        }
        else
            check_03 = false;
    }
}
