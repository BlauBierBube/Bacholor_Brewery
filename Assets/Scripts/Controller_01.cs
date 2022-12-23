using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Controller_01 : MonoBehaviour
{
    [SerializeField] GameObject Holo_Area01;
    [SerializeField] GameObject Holo_Area02;

    [SerializeField] GameObject Text01;
    [SerializeField] GameObject Text02;
    [SerializeField] GameObject Text03;
    [SerializeField] GameObject Text04;
    [SerializeField] GameObject Text05;
    [SerializeField] GameObject Text06;

    [SerializeField] GameObject Button01;
    [SerializeField] GameObject Button02;

    [SerializeField] GameObject LiquideTop;
    [SerializeField] GameObject LiquideBot;

    [SerializeField] GameObject Forward_BT;
    [SerializeField] GameObject Backward_BT;

    [SerializeField] GameObject Switch01;
    [SerializeField] GameObject Switch02;

    [SerializeField] GameObject PipeHole;
    [SerializeField] GameObject TankHole;
    [SerializeField] GameObject TankFront;
    [SerializeField] GameObject Inside;

    [SerializeField] GameObject WaterAni;
    [SerializeField] GameObject Brew01;
    [SerializeField] GameObject Bubbles;
    [SerializeField] GameObject Particels;
    [SerializeField] Material BrewMat;

    private int i = 0;
    private int Default;
    public int HighlightLayer = 6;

    // Wait Time between actions
    public float waitTime = 5f;

    // Counter Dependence
    private float count = 0;
    private float MaxCount = 3;
    private bool b1 = false;
    private bool b2 = false;
    private bool s1 = false;

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
        startPosition = Brew01.transform.position; //for moveToPosition
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate == true)
        {
            Inside.transform.Rotate(0, -rotationSpeed, 0, Space.World);
            Particels.transform.Rotate(0, -rotationSpeed, 0, Space.World);
        }

        if (moveTowards == true && onFinish == false)
        {
            float step = speed * Time.deltaTime;
            Brew01.transform.position = Vector3.MoveTowards(Brew01.transform.position, targetPosition, step);
            if(Vector3.Distance(Brew01.transform.position, startPosition) <0.001)
                onFinish = true;
        }
    }

    public void StepOutOfHolo()
    {
        Holo_Area01.SetActive(true);
        Text01.SetActive(false);
        Text02.SetActive(false);
        Text03.SetActive(false);
        Text04.SetActive(false);
        Text05.SetActive(false);
        Text06.SetActive(false);
    }

    public void Step00()
    {
        Text01.SetActive(true);
        Holo_Area01.SetActive(false);
        i++;
    }
    public void Step01()
    {
        Text01.SetActive(false);
        Text02.SetActive(true);
        Button01.layer = HighlightLayer;
        Button02.layer = HighlightLayer;
        Switch01.layer = HighlightLayer;
        i++;
    }


    public void Step02() // Button 1
    {
        Button01.layer = Default;
        PipeHole.SetActive(false);
        WaterAni.SetActive(true);
        moveTowards = true;
        //BrewMat Blue/Green #3A5B95
        //BrewMat.SetColor("DeepColor", new Color(10,140,140,50));
        //BrewMat.SetColor("_WaterColorShallow", new Color(10, 140, 140, 50));
        //BrewMat.SetColor("_WaterColorDeep", new Color(10, 140, 140, 50));
        if (b1 == false)
        {
            b1 = true;
            Counter();
        }
        ScaleObject(LiquideTop);
        i++;
    }
    

    public void Step03() // Button 2
    {
        Button02.layer = Default;
        TankHole.SetActive(false);
        //BrewMat Yellow #E08100
        //BrewMat.SetColor("DeepColor", new Color(224, 129, 0, 255));
        //BrewMat.SetColor("_WaterColorShallow", new Color(152, 117, 28, 154));
        //BrewMat.SetColor("_WaterColorDeep", new Color(84, 45, 4, 253));
        Particels.SetActive(true);
        if (b2 == false)
        {
            b2 = true;
            Counter();
        }
        ScaleObject(LiquideBot);
        i++;
    }

    public void Step04() // Switch 1
    {
        Switch01.layer = Default;
        Bubbles.SetActive(true);
        if (s1 == false)
        {
            s1 = true;
            Counter();
        }
        i++;
    }
    public void Step05() // Counter Finish
    {
        Text02.SetActive(false);
        Text03.SetActive(true);
        PipeHole.SetActive(true);
        TankHole.SetActive(true);
        i++; 
    }
    public void Step06() 
    { 
        Text03.SetActive(false);
        Text04.SetActive(true);
        Switch02.layer = HighlightLayer;
        i++;
    }
    public void Step07() // Switch 2
    {
        Switch02.layer = Default;
        Text04.SetActive(false);
        TankFront.SetActive(false);
        rotate = true;
        i++;
    }
    public void Step08()
    {
        Text05.SetActive(true);
        i++;
    }
    public void Step09()
    {
        Text05.SetActive(false);
        Text06.SetActive(true);
        Holo_Area02.SetActive(true);
        TankFront.SetActive(true);
        
        Bubbles.SetActive(false);
        Brew01.SetActive(false);
        rotate = false;
        i++;
    }
    public void Counter()
    {
        count++;
        if (count == MaxCount)
        {
            Step04();
        }
    }


    public void forward()
    {
        Action[] steps = new Action[]{
            Step00,
            Step01,
            Step02,
            Step03,
            Step04,
            Step05,
            Step06,
            Step07,
            Step08,
            Step09
        };
        i++;
        steps[i]();
    }
    public void backward()
    {
        Action[] steps = new Action[]{
            Step00,
            Step01,
            Step02,
            Step03,
            Step04,
            Step05
        };
        i--;
        steps[i]();
    }
    IEnumerable ScaleObject(GameObject O)
    {
        float scaleFactor = 1.0f;
        float scaleMax = 2.0f;
        float scaleIncrement = 0.01f;

        while (scaleFactor > scaleMax)
        {
            // Scale the object by the current scale factor
            O.transform.localScale = new Vector3(0, 0, scaleFactor);

            // Wait for a short amount of time before scaling again
            yield return new WaitForSeconds(0.1f);

            // Decrement the scale factor
            scaleFactor -= scaleIncrement;
        }
    }
}
