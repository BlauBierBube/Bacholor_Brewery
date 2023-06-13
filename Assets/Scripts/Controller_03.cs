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
        [SerializeField] GameObject Holo_Area03_Station;
        [SerializeField] GameObject Holo_Area04;

        [SerializeField] GameObject Text01;
        [SerializeField] GameObject Text02;
        [SerializeField] TMP_Text Text02_H;
        [SerializeField] TMP_Text Text02_T;
        [SerializeField] GameObject Text03;
        [SerializeField] GameObject Text04;
        [SerializeField] GameObject Text05;
        [SerializeField] GameObject Text06;
        [SerializeField] TMP_Text Text06_B;
        [SerializeField] GameObject Text07;
        [SerializeField] TMP_Text Text07_S;
        [SerializeField] GameObject Text08;
        [SerializeField] GameObject Text09;

        //[SerializeField] GameObject Stammwuerze;
        [SerializeField] GameObject Container;
        [SerializeField] GameObject Sample;
        [SerializeField] GameObject Spindel;
        [SerializeField] GameObject Braukessel;

        [SerializeField] GameObject Forward_BT;
        [SerializeField] GameObject Backward_BT;

        [SerializeField] GameObject Forward_BT2;
        [SerializeField] GameObject Backward_BT2;

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

        //Material Swap
        private Material[] originalMaterials;
        [SerializeField] Material transparentMat;

        private int i = 0;
        private int Default;
        public int HighlightLayer = 6;

        // Counter Dependence
        private float count = 0;
        private float MaxCount = 2;
        private bool b1 = false;
        private bool s1 = false;
        private bool h1 = false;
        private bool m1 = false;
        // moveToPosition
        public bool moveTowards = false;
        private bool onFinish = false;
        public float speed = 0.1f;
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

            if (moveTowards == true && onFinish == false) // Move To Positin UP
            {
                float step = speed * Time.deltaTime;
                Brew03.transform.position = Vector3.MoveTowards(Brew03.transform.position, targetPosition, step);
                if (Vector3.Distance(Brew03.transform.position, targetPosition) < 0.001)
                    onFinish = true;
            }
            if (moveTowards == false && onFinish == true) // Move To Positin DOWN
            {
                float step = 0.1f * speed * Time.deltaTime;
                Brew03.transform.position = Vector3.MoveTowards(Brew03.transform.position, startPosition, step);
                if (Vector3.Distance(Brew03.transform.position, startPosition) < 0.001)
                    onFinish = false;
            }
        }
        public void Deaktivate()
        {
            Holo_Area03.SetActive(false);
            Text01.SetActive(false);
            Text02.SetActive(false);
            Text03.SetActive(false);
            Text04.SetActive(false);

        }
        public void Aktivate()
        {
            Holo_Area03.SetActive(true);
            Text01.SetActive(true);
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

            PipeHole03.SetActive(false);
            WaterAni03.SetActive(true);

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
            Text02_T.fontStyle = FontStyles.Normal;
            Text02_H.fontStyle = FontStyles.Normal;
            b1 = false;
            s1 = false;
            count = 0;
            Button01.transform.parent.transform.parent.gameObject.GetComponent<InteractableUnityEventWrapper>().enabled = true;
            Switch01.GetComponent<TempRotConvert>().enabled = true;
            //Button Backward and Index Number
            StartCoroutine(WaitButton());
            i = 1;
            if (b1 && s1 == true)
                Step04();
        }


        public void Step02() // Button 1 Hopfen
        {
            if (b1 == false)
            {
                //Debug.LogError("Step02 is aktiv");
                Button01.layer = Default;
                Button01.transform.parent.transform.parent.gameObject.GetComponent<InteractableUnityEventWrapper>().enabled = false;

                TankHole03.SetActive(false);
                Hopfen.SetActive(true);

                Text02_H.fontStyle = FontStyles.Strikethrough;
                Particels03.SetActive(true);
                b1 = true;
                Counter();
            }
        }


        public void Step03() // Switch01 Temperatur
        {
            if (s1 == false)
            {
                //Debug.LogError("Step03 is aktiv");
                Bubbles03.SetActive(true);
                //TankFront03.SetActive(false);
                SaveAndChangeMaterials(TankFront03);
                Switch01.layer = Default;
                Switch01.GetComponent<TempRotConvert>().enabled = false;
                Text02_T.fontStyle = FontStyles.Strikethrough;
                s1 = true;
                Counter();
            }
        }

        public void Step04() // Couter01 Finish
        {
            //Debug.LogError("Step04 is aktiv");

            Invoke("ForwardBT", 2f);
        }

        public void Step05()
        {
            //Debug.LogError("Step05 is aktiv");
            Text02.SetActive(false);
            Text03.SetActive(true);
            TankHole03.SetActive(true);
            //TankFront03.SetActive(true);
            PipeHole03.SetActive(true);
            WaterAni03.SetActive(false);
            Hopfen.SetActive(false);
            //Button and Index Number
            StartCoroutine(WaitButton());
            Invoke("ForwardBT", 2f);
            i = 2;
        }
        /*
        public void Step06() // Stammwürze
        {

            Stammwuerze.layer = HighlightLayer;
            //Debug.LogError("Step06 is aktiv");
            StartCoroutine(ScaleObject(Stammwuerze, 1f));


            //Button and Index Number
            Invoke("ForwardBT", 2f);
            i = 3;
        }*/
        /*old
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
        }*/
        public void Step07()
        {
            //Debug.LogError("Step07 is aktiv");
            Text03.SetActive(false);
            Text04.SetActive(true);
            Holo_Area03_Station.SetActive(true);
            //Button and Index Number
            StartCoroutine(WaitButton());
            i = 3;
        }
        public void Step08() // Holo Station
        {
            //Debug.LogError("Step08 is aktiv");

            Text04.SetActive(false);
            Text05.SetActive(true);
            Holo_Area03_Station.SetActive(false);
            //Button and Index Number
            StartCoroutine(WaitButton_Station());
            Invoke("ForwardBT_Station", 2f);
            i = 4;
        }
        public void Step09() // Messbehaelter
        {
            //Debug.LogError("Step09 is aktiv");

            Text05.SetActive(false);
            Text06.SetActive(true);

            //behälter Outline
            //Probe Outline
            //Braukessel Outline

            Sample.layer = HighlightLayer;
            Braukessel.layer = HighlightLayer;
            Container.layer = HighlightLayer;
            //Button and Index Number
            StartCoroutine(WaitButton_Station());

            i = 5;
        }
        public void Step10() // Messbehaelter
        {

            Sample.layer = Default;
            Braukessel.layer = Default;
            Invoke("ForwardBT_Station", 2f);
        }

        public void Step11() // Messspindel
        {
            //Debug.LogError("Step10 is aktiv");

            Text06.SetActive(false);
            Text07.SetActive(true);
            // Braukessel Outline off

            //behälter Outline
            //Spindel Outline

            Container.layer = HighlightLayer;
            Spindel.layer = HighlightLayer;

            //Button and Index Number
            StartCoroutine(WaitButton_Station());
            i = 6;
        }
        public void Step12() // Messspindel
        {

            Container.layer = Default;
            Spindel.layer = Default;
            Invoke("ForwardBT_Station", 2f);
        }
        public void Step13() // 
        {
            //Debug.LogError("Step11 is aktiv");

            Text07.SetActive(false);
            Text08.SetActive(true);

            //Button and Index Number
            StartCoroutine(WaitButton_Station());
            Invoke("ForwardBT_Station", 2f);
            i = 7;
        }
        public void Step14() // 
        {
            //Debug.LogError("Step12 is aktiv");
            Text08.SetActive(false);
            Text09.SetActive(true);
            //TankFront03.SetActive(true);
            ResetMaterials(TankFront03);
            Holo_Area04.SetActive(true);
            //Button and Index Number
            StartCoroutine(WaitButton_Station());
            i = 8;
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
                Step07,
                Step08,
                Step09,
                Step11,
                Step13,
                Step14
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
                Step07,
                Step08,
                Step09,
                Step11,
                Step13,
                Step14
            };

            Text01.SetActive(false);
            Text02.SetActive(false);
            Text03.SetActive(false);
            Text04.SetActive(false);
            Text05.SetActive(false);
            Text06.SetActive(false);
            Text07.SetActive(false);
            Text08.SetActive(false);
            Text09.SetActive(false);
            i--;
            steps[i]();
        }
        IEnumerator ScaleObject(GameObject O, float S)
        {
            //Debug.LogError("Scale Active für " + O);
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
        IEnumerator WaitButton_Station()
        {
            //Debug.LogError("WaitButton is aktiv");
            Forward_BT.SetActive(false);
            Backward_BT.SetActive(false);
            Forward_BT2.SetActive(false);
            Backward_BT2.SetActive(false);
            yield return new WaitForSeconds(2f);
            Backward_BT2.SetActive(true);
        }
        private void ForwardBT_Station()
        {
            Forward_BT2.SetActive(true);
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
}
