using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;


namespace Oculus.Interaction
{
    public class Controller_07 : MonoBehaviour
    {
        [SerializeField] GameObject Holo_Area07;
        [SerializeField] GameObject Holo_Area08;

        [SerializeField] GameObject Text01;
        [SerializeField] GameObject Text02;
        [SerializeField] TMP_Text Text02_U;
        [SerializeField] TMP_Text Text02_T;
        [SerializeField] GameObject Text03;
        [SerializeField] GameObject Text04;

        [SerializeField] GameObject Forward_BT;
        [SerializeField] GameObject Backward_BT;

        [SerializeField] GameObject Switch01;
        [SerializeField] GameObject Switch02;

        [SerializeField] GameObject PipeHole;
        [SerializeField] GameObject TankFront;

        [SerializeField] GameObject WaterAni07;
        [SerializeField] GameObject Brew07;
        [SerializeField] Material BrewMat07;

        private int i = 0;
        private int Default;
        public int HighlightLayer = 6;

        // Counter Dependence
        private float count = 0;
        private float MaxCount = 2;

        private bool s1 = false;
        private bool s2 = false;

        // moveToPosition
        public bool moveTowards = false;
        private bool onFinish = false;
        public float speed = 1f;
        public Vector3 targetPosition;
        private Vector3 startPosition;


        // Start is called before the first frame update
        void Start()
        {
            startPosition = Brew07.transform.position; //for moveToPosition
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.LogError("i = "+i);
            //Debug.LogError("count =" + count);


            if (moveTowards == true && onFinish == false)
            {
                float step = speed * Time.deltaTime;
                Brew07.transform.position = Vector3.MoveTowards(Brew07.transform.position, targetPosition, step);
                if (Vector3.Distance(Brew07.transform.position, startPosition) < 0.001)
                    onFinish = true;
            }
        }

        public void StepOutOfHolo()
        {
            Holo_Area07.SetActive(true);
            Text01.SetActive(false);
            Text02.SetActive(false);
            Text03.SetActive(false);
            Text04.SetActive(false);
        }

        public void Step00()
        {
            //Debug.LogError("Step00 is aktiv");
            Text01.SetActive(true);
            Holo_Area07.SetActive(false);
            //Buttons and Index Number
            StartCoroutine(WaitButton());
            Invoke("ForwardBT", 2f);
            i = 0;
        }
        public void Step01()
        {
            //Debug.LogError("Step01 is aktiv");
            Text01.SetActive(false);
            Text02.SetActive(true);
            Switch02.layer = HighlightLayer;
            Switch01.layer = HighlightLayer;
            Switch01.GetComponent<CheckRotation>().enabled = true;
            Switch02.GetComponent<CheckRotation>().enabled = true;
            //Button Backward and Index Number
            StartCoroutine(WaitButton());
            i = 1;
        }


        public void Step02() // Switch 1 Pumpe
        {
            //Debug.LogError("Step02 is aktiv");
            Switch01.layer = Default;
            Switch01.GetComponent<CheckRotation>().enabled = false;
            PipeHole.SetActive(false);
            WaterAni07.SetActive(true);
            Text02_U.fontStyle = FontStyles.Strikethrough;

            if (s1 == false)
            {
                s1 = true;
                Counter();
            }
        }


        public void Step03() // Switch 2 Temp
        {
            //Debug.LogError("Step03 is aktiv");
            Switch02.GetComponent<CheckRotation>().enabled = false;
            Switch02.layer = Default;
            Text02_T.fontStyle = FontStyles.Strikethrough;


            // Animation + Hole Tank + Brew07

            if (s2 == false)
            {
                s2 = true;
                Counter();
            }
        }

        public void Step04() // Count01 Finish
        {

            //Debug.LogError("Step04 is aktiv");

            Invoke("ForwardBT", 2f);
        }

        public void Step05()
        {
            Text02.SetActive(false);
            Text03.SetActive(true);
            //Debug.LogError("Step05 is aktiv");
            StartCoroutine(WaitButton());
            Invoke("ForwardBT", 2f);
            i = 2;
        }
        public void Step06() // 
        {
            //Aus Step03 deaktivieren Ani..

            //Debug.LogError("Step06 is aktiv");
            Text03.SetActive(false);
            Text04.SetActive(true);
            //Index Number
            StartCoroutine(WaitButton());
            Invoke("ForwardBT", 2f);
            i = 3;
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
                Step05
            };
            i++;
            steps[i]();
        }
        public void backward()
        {
            Action[] steps = new Action[]{
                Step00,
                Step01
            };

            Text01.SetActive(false);
            Text02.SetActive(false);
            Text03.SetActive(false);
            Text04.SetActive(false);

            i--;
            steps[i]();
        }
        IEnumerator ScaleObject(GameObject O, float S)
        {
            //Debug.LogError("Scale Active für " + O);
            float scaleFactor = 0.01f;
            float scaleMax = S;
            float scaleIncrement = 0.01f;

            while (scaleFactor < scaleMax)
            {
                // Scale the object by the current scale factor
                O.transform.localScale = new Vector3(O.transform.localScale.x, O.transform.localScale.y, scaleFactor);

                // Wait for a short amount of time before scaling again
                yield return new WaitForSeconds(0.05f);

                // Decrement the scale factor
                scaleFactor += scaleIncrement;
            }
        }

        IEnumerator WaitButton()
        {
            //Debug.LogError("WaitButton is aktiv");
            Forward_BT.SetActive(false);
            Backward_BT.SetActive(false);
            yield return new WaitForSeconds(2f);
            if (i > 0 && i < 10)
            {
                Backward_BT.SetActive(true);
            }
        }
        private void ForwardBT()
        {
            Forward_BT.SetActive(true);
        }
    }
}
