using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;


namespace Oculus.Interaction { 
    public class Controller_01 : MonoBehaviour
    {
        [SerializeField] GameObject Holo_Area01;
        [SerializeField] GameObject Holo_Area02;

        [SerializeField] GameObject Text01;
        [SerializeField] GameObject Text02;
        [SerializeField] TMP_Text Text02_W;
        [SerializeField] TMP_Text Text02_G;
        [SerializeField] TMP_Text Text02_T;
        [SerializeField] GameObject Text03;
        [SerializeField] GameObject Text04;
        [SerializeField] TMP_Text Text04_M;
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


        // Counter Dependence
        private float count = 0;
        private float MaxCount = 3;
        private bool b1 = false;
        private bool b2 = false;
        private bool s1 = false;
        private bool s2 = false;
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
            //Debug.LogError("i = "+i);
            //Debug.LogError("count =" + count);
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
            //Debug.LogError("Step00 is aktiv");
            Text01.SetActive(true);
            Holo_Area01.SetActive(false);
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
            Button02.layer = HighlightLayer;
            Switch01.layer = HighlightLayer;
            Text02_W.fontStyle = FontStyles.Normal;
            Text02_G.fontStyle = FontStyles.Normal;
            Text02_T.fontStyle = FontStyles.Normal;
            s1 = false;
            b1 = false;
            b2 = false;
            count = 0;
            Button01.transform.parent.transform.parent.gameObject.GetComponent<InteractableUnityEventWrapper>().enabled= true;
            Button02.transform.parent.transform.parent.gameObject.GetComponent<InteractableUnityEventWrapper>().enabled = true;
            Switch01.GetComponent<CheckRotation>().enabled = true;
            //Button Backward and Index Number
            StartCoroutine(WaitButton());
            i = 1;
        }


        public void Step02() // Button 1 Water
        {
            if (b1 == false)
            {
                //Debug.LogError("Step02 is aktiv");
                Button01.layer = Default;
                Button01.transform.parent.transform.parent.gameObject.GetComponent<InteractableUnityEventWrapper>().enabled = false;
                PipeHole.SetActive(false);
                WaterAni.SetActive(true);
                Text02_W.fontStyle = FontStyles.Strikethrough;

                Brew01.SetActive(true);
                moveTowards = true;
                StartCoroutine(ScaleObject(LiquideBot, 2f));
                //BrewMat Blue/Green #3A5B95
                //BrewMat.SetColor("DeepColor", new Color(10,140,140,50));
                //BrewMat.SetColor("_WaterColorShallow", new Color(10, 140, 140, 50));
                //BrewMat.SetColor("_WaterColorDeep", new Color(10, 140, 140, 50));
                b1 = true;
                Counter();
            }
        }
    

        public void Step03() // Button 2
        {
            if (b2 == false)
            {
                //Debug.LogError("Step03 is aktiv");
                Button02.layer = Default;
                Button02.transform.parent.transform.parent.gameObject.GetComponent<InteractableUnityEventWrapper>().enabled = false;
                TankHole.SetActive(false);
                Text02_G.fontStyle = FontStyles.Strikethrough;
                StartCoroutine(ScaleObject(LiquideTop, 0.5f));
                Particels.SetActive(true);
                b2 = true;
                Counter();
            }
        }

        public void Step04() // Switch 1
        {
            if (s1 == false)
            {
                //Debug.LogError("Step04 is aktiv");
                Switch01.GetComponent<CheckRotation>().enabled = false;
                Switch01.layer = Default;
                Text02_T.fontStyle = FontStyles.Strikethrough;
                Bubbles.SetActive(true);
                s1 = true;
                Counter();
            }
        }
        public void Step05() // Counter Finish
        {
            //Debug.LogError("Step05 is aktiv");
            Text02.SetActive(false);
            Text03.SetActive(true);
            PipeHole.SetActive(true);
            TankHole.SetActive(true);
            //Button and Index Number
            Invoke("ForwardBT", 2f);
        }
        public void Step06() 
        {
            //Debug.LogError("Step06 is aktiv");
            Text03.SetActive(false);
            Text04.SetActive(true);
            Text04_M.fontStyle = FontStyles.Normal;
            s2 = false;
            Switch02.layer = HighlightLayer;
            Switch02.GetComponent<CheckRotation>().enabled = true;
            //Button and Index Number
            StartCoroutine(WaitButton());
            i = 2;
        }
        public void Step07() // Switch 2
        {
            if (s2 == false)
            {
                //Debug.LogError("Step07 is aktiv");
                Switch02.GetComponent<CheckRotation>().enabled = false;
                Switch02.layer = Default;
                Text04.SetActive(false);
                Text05.SetActive(true);
                TankFront.SetActive(false);
                Text04_M.fontStyle = FontStyles.Strikethrough;
                rotate = true;
                //Buttons and Index Number
                s2 = true;
                Invoke("ForwardBT", 2f);
            }
        }
        public void Step08()
        {
            //Debug.LogError("Step08 is aktiv");
            Text05.SetActive(false);
            Text06.SetActive(true);
            Holo_Area02.SetActive(true);
            TankFront.SetActive(true);

            Bubbles.SetActive(false);
            Brew01.SetActive(false);
            rotate = false;
            //Button and Index Number
            StartCoroutine(WaitButton());
            i = 3;
        }
        public void Counter()
        {
            count++;
            if (count == MaxCount)
            {
                Step05();
            }
        }


        public void forward()
        {
            Action[] steps = new Action[]{
                Step00,
                Step01,
                Step06,
                Step08
            };
            i++;
            steps[i]();
        }
        public void backward()
        {
            Action[] steps = new Action[]{
                Step00,
                Step01,
                Step06
            };
        
            Text01.SetActive(false);
            Text02.SetActive(false);
            Text03.SetActive(false);
            Text04.SetActive(false);
            Text05.SetActive(false);
            Text06.SetActive(false);
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
