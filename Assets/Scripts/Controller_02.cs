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
    [SerializeField] GameObject Text03;
    [SerializeField] TMP_Text Text03_L;
    [SerializeField] TMP_Text Text03_T;
    [SerializeField] GameObject Text04;
    [SerializeField] GameObject Text05;
    [SerializeField] TMP_Text Text05_A;
    [SerializeField] GameObject Text06;
    [SerializeField] TMP_Text Text06_T;
    [SerializeField] GameObject Text07;
    [SerializeField] GameObject Text08;

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
    [SerializeField] GameObject Treber;
    [SerializeField] GameObject Bubbles02;
    [SerializeField] Material BrewMat;

    [SerializeField] AudioSource Machine;

    //Material Swap
    private Material[] originalMaterials;
    [SerializeField] Material transparentMat;

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
    private bool moveTreber = false;
    public float speed = 0.1f;
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
            if(Vector3.Distance(Brew02.transform.position, targetPosition) <0.001)
                onFinish = true;
        }
        if (moveTowards == false && onFinish == true) // Move To Positin DOWN
        {
            float step = speed * Time.deltaTime;
            Brew02.transform.position = Vector3.MoveTowards(Brew02.transform.position, startPosition, step);
            if (Vector3.Distance(Brew02.transform.position, startPosition) < 0.001)
                onFinish = false;
        }
        if (moveTreber == true) // Move To Treber Positin DOWN
        {
            float step = speed * Time.deltaTime;
            Treber.transform.position = Vector3.MoveTowards(Treber.transform.position, startPosition, step);
            if (Vector3.Distance(Treber.transform.position, startPosition) < 0.001)
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
        //Debug.LogError("Step00 is aktiv");
        Text01.SetActive(true);
        Holo_Area02.SetActive(false);
 
        StartCoroutine(WaitButton());
        Invoke("ForwardBT", 2f);
        i = 0;
    }
    public void Step01()
    {
        //Debug.LogError("Step01 is aktiv");
        Text01.SetActive(false);
        Text02.SetActive(true);
        //TankFront.SetActive(false);
        SaveAndChangeMaterials(TankFront);
        Sinkboden.layer = HighlightLayer;
        Invoke("ForwardBT", 2f);
        StartCoroutine(WaitButton());
        i = 1;
    }
    public void Step02()
    {
        //Debug.LogError("Step02 is aktiv");
        Text02.SetActive(false);
        Text03.SetActive(true);
        Sinkboden.layer = default;
        Switch01.layer = HighlightLayer;
        Switch02.layer = HighlightLayer;
        Text03_L.fontStyle = FontStyles.Normal;
        Text03_T.fontStyle = FontStyles.Normal;
        count = 0;
        s1 = false;
        s2 = false;
        Switch01.GetComponent<CheckRotation>().enabled = true;
        Switch02.GetComponent<TempRotConvert>().enabled = true;
        //Button Backward and Index Number
        StartCoroutine(WaitButton());
        i = 2;
        if (s1 && s2 == true)
            Step04();
    }

    public void Step03() // Switch 1 Umfüllen
    {
        if (s1 == false)
        {
            //Debug.LogError("Step03 is aktiv");
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
            Text03_L.fontStyle = FontStyles.Strikethrough;
            //Index Number
            s1 = true;
            Counter01();
        }
    }

    public void Step04() // Switch 2 Temp
    {
        if (s2 == false)
        {
            //Debug.LogError("Step03 is aktiv");
            Switch02.layer = Default;
            Bubbles02.SetActive(true);

            //Deaktivate Switch
            Switch02.GetComponent<TempRotConvert>().enabled = false;
            Text03_T.fontStyle = FontStyles.Strikethrough;
            //Index Number
            s2 = true;
            Counter01();
        }
    }

    public void Step05() // Counter01 Finish
    {
        //Debug.LogError("Step04 is aktiv");


        //Button Backward and Index Number
        Invoke("ForwardBT", 2f);
    }



    public void Step06()
    {
        //Debug.LogError("Step06 is aktiv");
        Text03.SetActive(false);
        Text04.SetActive(true);
        Invoke("ForwardBT", 2f);
        StartCoroutine(WaitButton());
        i = 3;
    }

    public void Step07()
    {
        PipeHole.SetActive(true);
        Fluid_P2.SetActive(false);
        //Debug.LogError("Step07 is aktiv");
        Text04.SetActive(false);
        Text05.SetActive(true);

        //Aktivate Switch
        Switch03.layer = HighlightLayer;
        Switch03.GetComponent<CheckRotation>().enabled = true;

        //Button Backward and Index Number
        StartCoroutine(WaitButton());
        i = 4;
    }
    public void Step08() // Switch 3 Abpumpen
    {
        //Debug.LogError("Step08 is aktiv");
        Switch03.layer = Default;

        moveTowards = false;
        Treber.SetActive(true);
        //Deaktivate Switch
        Switch03.GetComponent<CheckRotation>().enabled = false;
        Text05_A.fontStyle = FontStyles.Strikethrough;
        //Index Number
        Invoke("ForwardBT", 2f);
    }
    public void Step09()
    {
        //Debug.LogError("Step09 is aktiv");
        Text05.SetActive(false);
        Text06.SetActive(true);
        //Aktivate Switch
        Switch04.layer = HighlightLayer;
        Switch04.GetComponent<CheckRotation>().enabled = true;
        StartCoroutine(WaitButton());
        i = 5;
    }


    public void Step10() // Switch 4 Austrebern
    {
        //Debug.LogError("Step10 is aktiv");
        Schacht.layer = HighlightLayer;
        Switch04.layer = Default;
        Machine.Play();
        rotate = true;
        moveTreber = true;
        //fluid go down
        //Deaktivate Switch
        Switch04.GetComponent<CheckRotation>().enabled = false;
        Text06_T.fontStyle = FontStyles.Strikethrough;
        //Button Backward and Index Number

        Invoke("ForwardBT", 2f);
    }
    public void Step11() // 
    {
        //Debug.LogError("Step11 is aktiv");
        Text06.SetActive(false);
        Text07.SetActive(true);
        Schacht.layer = Default;
        //TankFront.SetActive(true);
        Machine.Stop();
        ResetMaterials(TankFront);
        Bubbles02.SetActive(false);
        Brew02.SetActive(false);
        rotate = false;

        //Buttons and Index Number
        StartCoroutine(WaitButton());
        Invoke("ForwardBT", 2f);
        i = 6;
    }

    public void Step12()
    {
        //Debug.LogError("Step12 is aktiv");
        Text07.SetActive(false);
        Text08.SetActive(true);
        Holo_Area03.SetActive(true);
        //Button Backward and Index Number
        StartCoroutine(WaitButton());
        i = 7;

    }



    public void Counter01()
    {
        count++;
        if (count == MaxCount)
        {
            Step05();
        }
    }

    public void forward()
    {
        //Debug.LogError("Forward is aktiv");
        Action[] steps = new Action[]{
                Step00,
                Step01,
                Step02,
                Step06,
                Step07,
                Step09,
                Step11,
                Step12
            };
        i++;
        steps[i]();
    }
    public void backward()
    {
        //Debug.LogError("Backward is aktiv");
        Action[] steps = new Action[]{
                Step00,
                Step01,
                Step02,
                Step06,
                Step07,
                Step09,
                Step11
            };

        Text01.SetActive(false);
        Text02.SetActive(false);
        Text03.SetActive(false);
        Text04.SetActive(false);
        Text05.SetActive(false);
        Text06.SetActive(false);
        Text07.SetActive(false);
        Text08.SetActive(false);
        i--;
        steps[i]();
    }
    IEnumerator WaitButton()
    {
        //Debug.LogError("WaitButton is aktiv");
        Forward_BT.SetActive(false);
        Backward_BT.SetActive(false);
        yield return new WaitForSeconds(2f);
        Backward_BT.SetActive(true);
    }
    private void ForwardBT()
    {
        Forward_BT.SetActive(true);
    }

    void SaveAndChangeMaterials(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].material;
            renderers[i].material = transparentMat;
        }
    }

    void ResetMaterials(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = originalMaterials[i];
        }
    }
}
