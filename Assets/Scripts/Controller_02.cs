using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class Controller_02 : MonoBehaviour
{
    [SerializeField] GameObject Holo_Area02;
    [SerializeField] GameObject Holo_Area03;

    [SerializeField] GameObject Text01;
    [SerializeField] GameObject Text02;
    [SerializeField] TMP_Text Text02_L;
    [SerializeField] TMP_Text Text02_T;
    [SerializeField] GameObject Text03;
    [SerializeField] GameObject Text04;
    [SerializeField] TMP_Text Text04_A;
    [SerializeField] GameObject Text05;
    [SerializeField] TMP_Text Text05_T;
    [SerializeField] GameObject Text06;
    [SerializeField] GameObject Text07;

    [SerializeField] GameObject Switch01;
    [SerializeField] GameObject Switch02;
    [SerializeField] GameObject Switch03;
    [SerializeField] GameObject Switch04;

    [SerializeField] GameObject Forward_BT;
    [SerializeField] GameObject Backward_BT;

    [SerializeField] GameObject PipeHole;
    [SerializeField] GameObject TankFront;
    [SerializeField] GameObject Inside;
    [SerializeField] GameObject Schacht;
    [SerializeField] GameObject Sinkboden;

    [SerializeField] GameObject Fluid_P2;
    [SerializeField] GameObject Brew02;
    [SerializeField] GameObject Bubbles02;
    [SerializeField] Material BrewMat;
    private int i = 0;
    private int Default;
    public int HighlightLayer = 6;

    // Counter Dependence
    private float count = 0;
    private float MaxCount = 2;
    private bool s1 = false;
    private bool s2 = false;
    private bool s3 = false;
    private bool s4 = false;


    // moveToPosition
    public bool moveTowards = false;
    private bool onFinish = false;
    public float speed = 1f;
    public Vector3 targetPosition;
    private Vector3 startPosition;


    // Rotation Object
    public float rotationSpeed = 1f;
    private bool rotate = false;



    // Start is called before the first frame update
    void Start()
    {
        startPosition = Brew02.transform.position; //for moveToPosition
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate == true)
        {
            Inside.transform.Rotate(0, -rotationSpeed, 0, Space.World);
        }

        if (moveTowards == true && onFinish == false) // Move To Positin UP
        {
            float step = speed * Time.deltaTime;
            Brew02.transform.position = Vector3.MoveTowards(Brew02.transform.position, targetPosition, step);
            if(Vector3.Distance(Brew02.transform.position, startPosition) <0.001)
                onFinish = true;
        }
        if (moveTowards == false && onFinish == true) // Move To Positin DOWN
        {
            float step = speed * Time.deltaTime;
            Brew02.transform.position = Vector3.MoveTowards(targetPosition, Brew02.transform.position, step);
            if (Vector3.Distance(targetPosition, Brew02.transform.position) < 0.001)
                onFinish = false;
        }
    }

    public void StepOutOfHolo()
    {
        Holo_Area02.SetActive(true);
        Text01.SetActive(false);
        Text02.SetActive(false);
        Text03.SetActive(false);
        Text04.SetActive(false);
        Text05.SetActive(false);
        Text06.SetActive(false);
        Text07.SetActive(false);
    }

    public void Step00()
    {
        Debug.LogError("Step00 is aktiv");
        Text01.SetActive(true);
        Holo_Area02.SetActive(false);
 
        StartCoroutine(WaitButton());
        Invoke("ForwardBT", 2f);
        i = 0;
    }

    public void Step01()
    {
        Debug.LogError("Step01 is aktiv");
        Text01.SetActive(false);
        Text02.SetActive(true);

        Switch01.layer = HighlightLayer;
        Switch02.layer = HighlightLayer;
        Switch01.GetComponent<CheckRotation>().enabled = true;
        Switch02.GetComponent<CheckRotation>().enabled = true;
        //Button Backward and Index Number
        StartCoroutine(WaitButton());
        i = 1;
        if (s1 && s2 == true)
            Step04();
    }

    public void Step02() // Switch 1 Läutern
    {
        Debug.LogError("Step02 is aktiv");
        Switch01.layer = default;
        PipeHole.SetActive(false);
        Fluid_P2.SetActive(true);
        Brew02.SetActive(true);
        moveTowards = true;
        //BrewMat.SetColor("DeepColor", new Color(224, 129, 0, 255));
        //BrewMat.SetColor("_WaterColorShallow", new Color(152, 117, 28, 154));
        //BrewMat.SetColor("_WaterColorDeep", new Color(84, 45, 4, 253));

        //Deaktivate Switch
        Switch01.GetComponent<CheckRotation>().enabled = false;
        Text02_L.fontStyle = FontStyles.Strikethrough;
        //Index Number
        if (s1 == false)
        {
            s1 = true;
            Counter01();
        }
    }

    public void Step03() // Switch 2 Temp
    {
        Debug.LogError("Step03 is aktiv");
        Switch02.layer = Default;
        Bubbles02.SetActive(true);

        //Deaktivate Switch
        Switch02.GetComponent<CheckRotation>().enabled = false;
        Text02_T.fontStyle = FontStyles.Strikethrough;
        //Index Number
        if (s2 == false)
        {
            s2 = true;
            Counter01();
        }
    }

    public void Step04() // Counter01 Finish
    {
        Debug.LogError("Step04 is aktiv");


        //Button Backward and Index Number
        Invoke("ForwardBT", 2f);
    }

    public void Step05()
    {
        Debug.LogError("Step05 is aktiv");
        Text02.SetActive(false);
        Text03.SetActive(true);
        TankFront.SetActive(false);
        Sinkboden.layer = HighlightLayer;
        Invoke("ForwardBT", 2f);
        StartCoroutine(WaitButton());
        i = 2;
    }

    public void Step06()
    {
        PipeHole.SetActive(true);
        Debug.LogError("Step06 is aktiv");
        Text03.SetActive(false);
        Text04.SetActive(true);
        Sinkboden.layer = Default;


        //Aktivate Switch
        Switch03.layer = HighlightLayer;
        Switch03.GetComponent<CheckRotation>().enabled = true;

        //Button Backward and Index Number
        StartCoroutine(WaitButton());
        i = 3;
    }
    public void Step07() // Switch 3 Abpumpen
    {
        Debug.LogError("Step07 is aktiv");
        Switch03.layer = Default;

        //Deaktivate Switch
        Switch03.GetComponent<CheckRotation>().enabled = false;
        Text04_A.fontStyle = FontStyles.Strikethrough;
        //Index Number
        Invoke("ForwardBT", 2f);
        i = 4;
    }
    public void Step08()
    {
        Debug.LogError("Step08 is aktiv");
        Text04.SetActive(false);
        Text05.SetActive(true);
        //Aktivate Switch
        Switch04.layer = HighlightLayer;
        Switch04.GetComponent<CheckRotation>().enabled = true;
        StartCoroutine(WaitButton());
        i = 5;
    }


    public void Step09() // Switch 4 Austrebern
    {
        Debug.LogError("Step09 is aktiv");
        Schacht.layer = HighlightLayer;
        Switch04.layer = Default;
        rotate = true;

        //fluid go down
        //Deaktivate Switch
        Switch04.GetComponent<CheckRotation>().enabled = false;
        Text05_T.fontStyle = FontStyles.Strikethrough;
        //Button Backward and Index Number

        Invoke("ForwardBT", 2f);
        i = 6;
    }
    public void Step10() // 
    {
        Debug.LogError("Step10 is aktiv");
        Text05.SetActive(false);
        Text06.SetActive(true);
        Schacht.layer = Default;
        TankFront.SetActive(true);
        Bubbles02.SetActive(false);
        Brew02.SetActive(false);
        rotate = false;

        //Buttons and Index Number
        StartCoroutine(WaitButton());
        Invoke("ForwardBT", 2f);
        i = 7;
    }

    public void Step11()
    {
        Debug.LogError("Step11 is aktiv");
        Text06.SetActive(false);
        Text07.SetActive(true);
        Holo_Area03.SetActive(true);
        //Button Backward and Index Number
        StartCoroutine(WaitButton());
        i = 8;

    }



    public void Counter01()
    {
        count++;
        if (count == MaxCount)
        {
            Step04();
        }
    }

    public void forward()
    {
        Debug.LogError("Forward is aktiv");
        Action[] steps = new Action[]{
                Step00,
                Step01,
                Step05,
                Step06,
                Step07,
                Step08,
                Step09,
                Step10,
                Step11
            };
        i++;
        steps[i]();
    }
    public void backward()
    {
        Debug.LogError("Backward is aktiv");
        Action[] steps = new Action[]{
                Step00,
                Step01,
                Step05,
                Step06,
                Step07,
                Step08,
                Step09,
                Step10
            };

        Text01.SetActive(false);
        Text02.SetActive(false);
        Text03.SetActive(false);
        Text04.SetActive(false);
        Text05.SetActive(false);
        Text06.SetActive(false);
        Text07.SetActive(false);
        i--;
        steps[i]();
    }
    IEnumerator WaitButton()
    {
        Debug.LogError("WaitButton is aktiv");
        Forward_BT.SetActive(false);
        Backward_BT.SetActive(false);
        yield return new WaitForSeconds(2f);
        Backward_BT.SetActive(true);
    }
    private void ForwardBT()
    {
        Forward_BT.SetActive(true);
    }
}
