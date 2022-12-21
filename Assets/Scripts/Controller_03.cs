using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Controller_03 : MonoBehaviour
{
    [SerializeField] GameObject Holo_Area01;
    [SerializeField] GameObject Holo_Area02;

    [SerializeField] GameObject Text01;
    [SerializeField] GameObject Text02;
    [SerializeField] GameObject Text03;
    [SerializeField] GameObject Text04;
    [SerializeField] GameObject Text05;
    [SerializeField] GameObject Text06;
    [SerializeField] GameObject Text07;

    [SerializeField] GameObject Button01;


    [SerializeField] GameObject Switch01;


    [SerializeField] GameObject PipeHole;
    [SerializeField] GameObject TankFront;
    [SerializeField] GameObject Inside;


    [SerializeField] GameObject WaterAni;
    [SerializeField] GameObject Brew01;
    [SerializeField] GameObject Bubbles;
    [SerializeField] Material BrewMat;

    private int Default;
    public int HighlightLayer = 6;

    // Wait Time between actions
    public float waitTime = 5f;

    // Counter Dependence
    private float count = 0;
    private float MaxCount = 2;
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
    }

    public void Step00()
    {
        StartCoroutine(Step00_());
    }

    IEnumerator Step00_()
    {
        Text01.SetActive(true);
        Holo_Area01.SetActive(false);
        yield return new WaitForSecondsRealtime(waitTime);
        Text01.SetActive(false);
        Text02.SetActive(true);
        Switch01.layer = HighlightLayer;
    }


    public void Step01() // Switch 1
    {
        Switch01.layer = Default;
        Bubbles.SetActive(true);
        moveTowards = true;
        BrewMat.SetColor("DeepColor", new Color(224, 129, 0, 255));
        BrewMat.SetColor("_WaterColorShallow", new Color(152, 117, 28, 154));
        BrewMat.SetColor("_WaterColorDeep", new Color(84, 45, 4, 253));       
    }

    public void Step02() // Button 1
    {
        Button01.layer = Default;
    }

    public void Step03()
    {
        StartCoroutine(Step03_());
    }
    public IEnumerator Step03_() // Counter Finish
    {
        Text02.SetActive(false);
        Text03.SetActive(true);

        PipeHole.SetActive(true);
        yield return new WaitForSecondsRealtime(waitTime);


    }
    public void Step04()
    {
        StartCoroutine(Step04_());
    }
    public IEnumerator Step04_() // Counter Finish
    {
        Text02.SetActive(false);
        Text03.SetActive(true);
        PipeHole.SetActive(true);

        yield return new WaitForSecondsRealtime(waitTime);
        Text03.SetActive(false);
        Text04.SetActive(true);

    }
    public void Step05()
    {
        StartCoroutine(Step05_());
    }
    public IEnumerator Step05_() // Button 2
    {
        Holo_Area01.SetActive(false);
        TankFront.SetActive(false);

        Text04.SetActive(false);
        Text05.SetActive(true);
        yield return new WaitForSecondsRealtime(waitTime);
        Text05.SetActive(false);
        Text06.SetActive(true);

    }

    public void Step06()
    {
        StartCoroutine(Step06_());
    }
    public IEnumerator Step06_() // Switch 2
    {
        rotate = true;
        Text06.SetActive(false);
        yield return new WaitForSecondsRealtime(waitTime);



        yield return new WaitForSecondsRealtime(waitTime);

        Text07.SetActive(true);
        Holo_Area02.SetActive(true);
        TankFront.SetActive(true);

        Bubbles.SetActive(false);
        Brew01.SetActive(false);
        rotate = false;
    }



    public void Counter()
    {
        count++;
        if (count == MaxCount)
        {
            Step03();
        }
    }
}
