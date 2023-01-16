using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;


namespace Oculus.Interaction
{
    public class Controller_06 : MonoBehaviour
    {
        [SerializeField] GameObject Holo_Area06;
        [SerializeField] GameObject Holo_Area07;

        [SerializeField] GameObject Text01;
        [SerializeField] GameObject Text02;
        [SerializeField] TMP_Text Text02_B;
        [SerializeField] TMP_Text Text02_P;
        [SerializeField] GameObject Text03;
        [SerializeField] TMP_Text Text03_T;
        [SerializeField] GameObject Text04;
        [SerializeField] GameObject Text05;


        [SerializeField] GameObject Button01;

        [SerializeField] GameObject Forward_BT;
        [SerializeField] GameObject Backward_BT;

        [SerializeField] GameObject Switch01;
        [SerializeField] GameObject Switch02;

        [SerializeField] GameObject PipeHole;
        [SerializeField] GameObject TankFront;

        [SerializeField] GameObject WaterAni06;
        [SerializeField] GameObject Brew06;
        [SerializeField] Material BrewMat06;

        private int i = 0;
        private int Default;
        public int HighlightLayer = 6;

        // Counter Dependence
        private float count = 0;
        private float MaxCount = 2;

        private bool b1 = false;
        private bool s1 = false;
        private bool s2 = false;

        // moveToPosition
        public bool moveTowards = false;
        private bool onFinish = false;
        public float speed = 1f;
        public Vector3 targetPosition;
        private Vector3 startPosition;


        // Material
        Renderer renderer;
        Material materialToAddBack;

        // Start is called before the first frame update
        void Start()
        {
            startPosition = Brew06.transform.position; //for moveToPosition
        }
        

        // Update is called once per frame
        void Update()
        {
            //Debug.LogError("i = "+i);
            //Debug.LogError("count =" + count);

            /*
            if (moveTowards == true && onFinish == false)
            {
                float step = speed * Time.deltaTime;
                Brew06.transform.position = Vector3.MoveTowards(Brew06.transform.position, targetPosition, step);
                if (Vector3.Distance(Brew06.transform.position, startPosition) < 0.001)
                    onFinish = true;
            }*/
        }

        public void StepOutOfHolo()
        {
            Holo_Area06.SetActive(true);
            Text01.SetActive(false);
            Text02.SetActive(false);
            Text03.SetActive(false);
            Text04.SetActive(false);
            Text05.SetActive(false);
        }

        public void Step00()
        {
            //Debug.LogError("Step00 is aktiv");
            Text01.SetActive(true);
            Holo_Area06.SetActive(false);
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
            b1 = false;
            s1 = false;
            Switch01.GetComponent<CheckRotation>().enabled = true;
            Button01.transform.parent.transform.parent.gameObject.GetComponent<InteractableUnityEventWrapper>().enabled = true;
            Text02_B.fontStyle = FontStyles.Normal;
            Text02_P.fontStyle = FontStyles.Normal;
            
            //Button Backward and Index Number
            StartCoroutine(WaitButton());
            i = 1;
        }


        public void Step02() // Button 1 Brauhefe
        {
            if (b1 == false)
            {
                //Debug.LogError("Step02 is aktiv");
                Button01.layer = Default;
                Button01.transform.parent.transform.parent.gameObject.GetComponent<InteractableUnityEventWrapper>().enabled = false;
                PipeHole.SetActive(false);
                WaterAni06.SetActive(true);
                Text02_B.fontStyle = FontStyles.Strikethrough;
                b1 = true;
                Counter();
            }
        }


        public void Step03() // Switch 1 Sud
        {
            if (s1 == false)
            {
                //Debug.LogError("Step03 is aktiv");
                Switch01.GetComponent<CheckRotation>().enabled = false;
                Switch01.layer = Default;
                Text02_P.fontStyle = FontStyles.Strikethrough;
                Brew06.SetActive(true);
                // Remove Material for TankHole
                RemoveMaterial();
                // Scale Brew06
                StartCoroutine(ScaleObject(Brew06, 8f));

                s1 = true;
                Counter();
            }
        }

        public void Step04() // Count01 Finish
        {
            //Debug.LogError("Step04 is aktiv");
            StartCoroutine(WaitButton());
            Invoke("ForwardBT", 2f);
        }

        public void Step05() // 
        {
            //Aus Step03 deaktivieren Ani..

            //Debug.LogError("Step05 is aktiv");
            Text02.SetActive(false);
            Text03.SetActive(true);
            PipeHole.SetActive(true);
            s2 = false;
            Switch02.layer = HighlightLayer;
            Text03_T.fontStyle = FontStyles.Normal;
            Switch02.GetComponent<TempRotConvert>().enabled = true;
            //Index Number
            StartCoroutine(WaitButton());
            i = 2;
        }
        
        public void Step06() // Switch02 Temp
        {
            if(s2 == false)
            {
                //Debug.LogError("Step06 is aktiv");
                Switch02.layer = Default;
                Switch02.GetComponent<TempRotConvert>().enabled = false;
                Text03_T.fontStyle = FontStyles.Strikethrough;
                s2 = true;
                //Button and Index Number
                Invoke("ForwardBT", 2f);
            }

        }
        public void Step07() // 
        {
            //Debug.LogError("Step07 is aktiv");
            Text03.SetActive(false);
            Text04.SetActive(true);
            //Buttons and Index Number
            StartCoroutine(WaitButton());
            Invoke("ForwardBT", 2f);
            i = 3;
        }
        public void Step08()
        {
            // Add Material for TankHole
            AddMaterialBack();
            Brew06.SetActive(false);
            //Debug.LogError("Step08 is aktiv");
            Text04.SetActive(false);
            Text05.SetActive(true);
            Holo_Area07.SetActive(true);
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
                Step07,
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
                Step05,
                Step07
            };

            Text01.SetActive(false);
            Text02.SetActive(false);
            Text03.SetActive(false);
            Text04.SetActive(false);
            Text05.SetActive(false);

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
                O.transform.localScale = new Vector3(O.transform.localScale.x,scaleFactor, O.transform.localScale.z);

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

        void RemoveMaterial()
        {
            renderer = TankFront.GetComponent<Renderer>();
            Material[] materials = renderer.materials;

            //Remove the first material from the materials array
            materialToAddBack = materials[0];
            Material[] newMaterials = new Material[materials.Length - 1];
            for (int i = 1; i < materials.Length; i++)
            {
                newMaterials[i - 1] = materials[i];
            }
            renderer.materials = newMaterials;
        }

        void AddMaterialBack()
        {
            // Add the removed material back to the materials array
            Material[] materialsWithAdded = new Material[renderer.materials.Length + 1];
            materialsWithAdded[0] = materialToAddBack;
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                materialsWithAdded[i + 1] = renderer.materials[i];
            }
            renderer.materials = materialsWithAdded;
        }
    }
}
