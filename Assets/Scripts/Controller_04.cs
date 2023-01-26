using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;


namespace Oculus.Interaction
{
    public class Controller_04 : MonoBehaviour
    {
        [SerializeField] GameObject Holo_Area04;
        [SerializeField] GameObject Holo_Area05;

        [SerializeField] GameObject Text01;
        [SerializeField] GameObject Text02;
        [SerializeField] TMP_Text Text02_A;
        [SerializeField] GameObject Text03;
        [SerializeField] GameObject Text04;
        [SerializeField] TMP_Text Text04_P;
        [SerializeField] GameObject Text05;
        [SerializeField] GameObject Text06;

        [SerializeField] GameObject Forward_BT;
        [SerializeField] GameObject Backward_BT;

        [SerializeField] GameObject Switch01;
        [SerializeField] GameObject Switch02;

        [SerializeField] GameObject PipeHole;
        [SerializeField] GameObject TankFront;

        [SerializeField] GameObject WaterAni04;
        [SerializeField] GameObject Brew04;
        [SerializeField] GameObject Kegel_1;
        [SerializeField] GameObject Kegel_2;
        [SerializeField] GameObject Partikel;
        [SerializeField] Material BrewMat04;

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
        // moveToPosition
        public bool moveTowards = false;
        private bool onFinish = false;
        public float speed = 0.1f;
        public Vector3 targetPosition;
        private Vector3 startPosition;

        // Rotation Object
        public float rotationSpeed = 1f;
        private bool rotate = false;


        private void Start()
        {
            startPosition = Brew04.transform.position;
        }
        // Update is called once per frame
        void Update()
        {
            if (rotate == true)
            {
                Kegel_1.transform.Rotate(0, -rotationSpeed, 0, Space.World);
                Kegel_2.transform.Rotate(0, -rotationSpeed, 0, Space.World);
            }
            //Debug.LogError("i = "+i);
            //Debug.LogError("count =" + count);

            if (moveTowards == true && onFinish == false) // Move To Positin UP
            {
                float step = speed * Time.deltaTime;
                Brew04.transform.position = Vector3.MoveTowards(Brew04.transform.position, targetPosition, step);
                if (Vector3.Distance(Brew04.transform.position, targetPosition) < 0.001)
                    onFinish = true;
            }
            if (moveTowards == false && onFinish == true) // Move To Positin DOWN
            {
                float step = speed * Time.deltaTime;
                Brew04.transform.position = Vector3.MoveTowards(Brew04.transform.position, startPosition, step);
                if (Vector3.Distance(Brew04.transform.position, startPosition) < 0.001)
                    onFinish = false;
            }
        }

        public void StepOutOfHolo()
        {
            Holo_Area04.SetActive(true);
            Text01.SetActive(false);
            Text02.SetActive(false);
            Text03.SetActive(false);
            Text04.SetActive(false);
        }

        public void Step00()
        {
            //Debug.LogError("Step00 is aktiv");
            Text01.SetActive(true);
            Holo_Area04.SetActive(false);


            //Buttons and Index Number
            StartCoroutine(WaitButton());
            Invoke("ForwardBT", 2f);
            i = 0;
        }
        public void Step01() // 
        {
            //Debug.LogError("Step01 is aktiv");
            Text01.SetActive(false);
            Text02.SetActive(true);
            Text02_A.fontStyle = FontStyles.Normal;
            s1 = false;
            Switch01.layer = HighlightLayer;
            Switch01.GetComponent<CheckRotation>().enabled = true;
            //Button Backward and Index Number
            StartCoroutine(WaitButton());
            i = 1;
        }


        public void Step02() // Switch01 Pumpe
        {
            if (s1 == false)
            {
                //Debug.LogError("Step02 is aktiv");
                //TankFront.SetActive(false);
                SaveAndChangeMaterials(TankFront);
                Switch01.layer = Default;
                Switch01.GetComponent<CheckRotation>().enabled = false;
                // 
                moveTowards = true;
                Brew04.SetActive(true);
                Machine.Play();
                rotate = true;


                Text02_A.fontStyle = FontStyles.Strikethrough;
                //Button Backward and Index Number
                Invoke("ForwardBT", 2f);
                s1 = true;
            }

        }
        public void Step03()
        {
            Text02.SetActive(false);
            Text03.SetActive(true);
            //Button Backward and Index Number
            StartCoroutine(WaitButton());
            Invoke("ForwardBT", 2f);
            i = 2;
        }

        public void Step04()
        {
            Text03.SetActive(false);
            Text04.SetActive(true);
            s2 = false;
            Switch02.layer = HighlightLayer;
            Text04_P.fontStyle = FontStyles.Normal;
            Switch02.GetComponent<CheckRotation>().enabled = true;
            //Button Backward and Index Number
            StartCoroutine(WaitButton());
            i = 3;
        }


        public void Step05() // Switch02 Abpumpen
        {
            if (s2 == false)
            {
                //Debug.LogError("Step03 is aktiv");
                Switch02.layer = Default;
                Switch02.GetComponent<CheckRotation>().enabled = false;
                Text04_P.fontStyle = FontStyles.Strikethrough;

                //Brew go Down, Kegel go down 
                moveTowards = false;

                s2 = true;
                //Button Backward and Index Number
                Invoke("ForwardBT", 2f);
            }


        }

        public void Step06() // 
        {
            Brew04.SetActive(false);
            rotate = false;
            Machine.Stop();
            // Deaktivate Brew, Kegel
            //TankFront.SetActive(true);
            ResetMaterials(TankFront);
            Brew04.SetActive(false);
            Kegel_1.SetActive(false);
            Kegel_2.SetActive(false);
            //Debug.LogError("Step04 is aktiv");
            Text04.SetActive(false);
            Text05.SetActive(true);

            //Button Backward and Index Number
            StartCoroutine(WaitButton());
            Invoke("ForwardBT", 2f);
            i = 4;
        }
        public void Step07() // Fertig
        {
            //Debug.LogError("Step04 is aktiv");
            Text05.SetActive(false);
            Text06.SetActive(true);

            Holo_Area05.SetActive(true);

            //Button Backward and Index Number
            StartCoroutine(WaitButton());
            i = 5;
        }

        public void forward()
        {
            Action[] steps = new Action[]{
                Step00,
                Step01,
                Step03,
                Step04,
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
                Step03,
                Step04,
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
