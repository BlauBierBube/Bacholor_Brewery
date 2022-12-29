using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;


namespace Oculus.Interaction
{
    public class Controller_03 : MonoBehaviour
    {
        [SerializeField] GameObject Holo_Area03;
        [SerializeField] GameObject Holo_Area04;

        [SerializeField] GameObject Text01;
        [SerializeField] GameObject Text02;
        [SerializeField] TMP_Text Text02_H;
        [SerializeField] TMP_Text Text02_T;
        [SerializeField] GameObject Text03;
        [SerializeField] GameObject Text04;

        [SerializeField] GameObject Stammwuerze;

        [SerializeField] GameObject Forward_BT;
        [SerializeField] GameObject Backward_BT;

        [SerializeField] GameObject Button01;
        [SerializeField] GameObject Switch01;

        [SerializeField] GameObject PipeHole03;
        [SerializeField] GameObject TankHole03;
        [SerializeField] GameObject TankFront03;

        [SerializeField] GameObject Hopfen;
        [SerializeField] GameObject WaterAni03;
        [SerializeField] GameObject Brew03;
        [SerializeField] GameObject Bubbles03;
        [SerializeField] GameObject Particels03;
        [SerializeField] Material BrewMat03;

        private int i = 0;
        private int Default;
        public int HighlightLayer = 6;

        // Counter Dependence
        private float count = 0;
        private float MaxCount = 2;
        private bool b1 = false;
        private bool s1 = false;
        // moveToPosition
        public bool moveTowards = false;
        private bool onFinish = false;
        public float speed = 1f;
        public Vector3 targetPosition;
        private Vector3 startPosition;



        private void Start()
        {
            startPosition = Brew03.transform.position;
        }
        // Update is called once per frame
        void Update()
        {
            //Debug.LogError("i = "+i);
            //Debug.LogError("count =" + count);

            if (moveTowards == true && onFinish == false)
            {
                float step = speed * Time.deltaTime;
                Brew03.transform.position = Vector3.MoveTowards(Brew03.transform.position, targetPosition, step);
                if (Vector3.Distance(Brew03.transform.position, startPosition) < 0.001)
                    onFinish = true;
            }
        }

        public void StepOutOfHolo()
        {
            Holo_Area03.SetActive(true);
            Text01.SetActive(false);
            Text02.SetActive(false);
            Text03.SetActive(false);
            Text04.SetActive(false);
        }

        public void Step00()
        {
            //Debug.LogError("Step00 is aktiv");
            Text01.SetActive(true);
            Holo_Area03.SetActive(false);

            Brew03.SetActive(true);
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

            Button01.layer = HighlightLayer;
            Switch01.layer = HighlightLayer;
            Button01.transform.parent.transform.parent.gameObject.GetComponent<InteractableUnityEventWrapper>().enabled = true;
            Switch01.GetComponent<CheckRotation>().enabled = true;
            //Button Backward and Index Number
            StartCoroutine(WaitButton());
            i = 1;
            if (b1 && s1 == true)
                Step04();
        }


        public void Step02() // Button 1 Hopfen
        {
            //Debug.LogError("Step02 is aktiv");
            Button01.layer = Default;
            Button01.transform.parent.transform.parent.gameObject.GetComponent<InteractableUnityEventWrapper>().enabled = false;

            TankHole03.SetActive(false);
            Hopfen.SetActive(true);

            Text02_H.fontStyle = FontStyles.Strikethrough;
            Particels03.SetActive(true);

            if (b1 == false)
            {
                b1 = true;
                Counter();
            }
        }


        public void Step03() // Switch01 Temperatur
        {
            //Debug.LogError("Step03 is aktiv");
            Bubbles03.SetActive(true);
            TankFront03.SetActive(false);
            Switch01.layer = Default;
            Switch01.GetComponent<CheckRotation>().enabled = false;
            Text02_T.fontStyle = FontStyles.Strikethrough;


            if (s1 == false)
            {
                s1 = true;
                Counter();
            }
        }

        public void Step04() // Couter01 Finish
        {
            //Debug.LogError("Step04 is aktiv");

            Invoke("ForwardBT",2f);
        }

        public void Step05()
        {
            //Debug.LogError("Step05 is aktiv");
            Text02.SetActive(false);
            Text03.SetActive(true);
            TankHole03.SetActive(true);
            TankFront03.SetActive(true);
            Hopfen.SetActive(false);
            //Button and Index Number
            StartCoroutine(WaitButton());
            Invoke("ForwardBT", 2f);
            i = 2;
        }

        public void Step06() // Stammw�rze
        {

            Stammwuerze.layer = HighlightLayer;
            //Debug.LogError("Step06 is aktiv");
            StartCoroutine(ScaleObject(Stammwuerze, 1f));


            //Button and Index Number
            Invoke("ForwardBT", 2f);
            i = 3;
        }
        public void Step07()
        {
            Stammwuerze.layer = Default;
            Holo_Area04.SetActive(true);
            //Debug.LogError("Step07 is aktiv");
            Text03.SetActive(false);
            Text04.SetActive(true);
            TankFront03.SetActive(true);
            Brew03.SetActive(false);
            //Button and Index Number
            StartCoroutine(WaitButton());
            i = 4;
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
                Step05,
                Step06,
                Step07
            };
            i++;
            steps[i]();
        }
        public void backward()
        {
            Action[] steps = new Action[]{
                Step00,
                Step01,
                Step05,
                Step06,
                Step07
            };

            Text01.SetActive(false);
            Text02.SetActive(false);
            Text03.SetActive(false);
            Text04.SetActive(false);
            if (i > 5)
            {
                PipeHole03.SetActive(true);
                TankHole03.SetActive(true);
                Particels03.SetActive(false);
                WaterAni03.SetActive(false);
                Bubbles03.SetActive(false);
                b1 = false;
                s1 = false;
            }
            if (i == 8)
            {
                TankFront03.SetActive(true);
            }
            i--;
            steps[i]();
        }
        IEnumerator ScaleObject(GameObject O, float S)
        {
            //Debug.LogError("Scale Active f�r " + O);
            float scaleFactor = 2.5f; //2.5
            float scaleMax = S; //1
            float scaleIncrement = 0.01f;

            while (scaleFactor > scaleMax)
            {
                // Scale the object by the current scale factor
                O.transform.localScale = new Vector3(O.transform.localScale.x, O.transform.localScale.y, scaleFactor);

                // Wait for a short amount of time before scaling again
                yield return new WaitForSeconds(0.05f);

                // Decrement the scale factor
                scaleFactor -= scaleIncrement;
            }
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
    }
}
