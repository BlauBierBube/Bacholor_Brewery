using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;


namespace Oculus.Interaction
{
    public class Controller_05 : MonoBehaviour
    {
        [SerializeField] GameObject Holo_Area05;
        [SerializeField] GameObject Holo_Area06;

        [SerializeField] GameObject Text01;
        [SerializeField] GameObject Text02;
        [SerializeField] TMP_Text Text02_T;
        [SerializeField] GameObject Text03;
        [SerializeField] GameObject Text04;

        [SerializeField] Transform Needle01;
        [SerializeField] Transform Needle02;

        [SerializeField] GameObject Forward_BT;
        [SerializeField] GameObject Backward_BT;

        [SerializeField] GameObject Switch01;

        [SerializeField] AudioSource Machine;

        private bool NRotate = false;

        private int i = 0;
        private int Default;
        public int HighlightLayer = 6;
        
        private bool moveTowards = false;
        public float N1Angle = 57;
        public float N2Angle = 161;
        private bool s1 = false;
        private void Start()
        {
            
        }
        private void Update()
        {
            
            if (moveTowards == true)
            {
                Needle01.transform.localEulerAngles = new Vector3(0,N1Angle,0);
                Needle02.transform.localEulerAngles = new Vector3(0, N2Angle, 0);
            }
    }


    public void StepOutOfHolo()
        {
            Holo_Area05.SetActive(true);
            Text01.SetActive(false);
            Text02.SetActive(false);
            Text03.SetActive(false);
            Text04.SetActive(false);
        }

        public void Step00()
        {
            //Debug.LogError("Step00 is aktiv");
            Text01.SetActive(true);
            Holo_Area05.SetActive(false);

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

            Switch01.layer = HighlightLayer;
            Text02_T.fontStyle = FontStyles.Normal;
            Switch01.GetComponent<CheckRotation>().enabled = true;
            //Button Backward and Index Number
            StartCoroutine(WaitButton());
            i = 1;
        }


        public void Step02() // Switch01 Temp
        {
            if(s1 == false)
            {
                Machine.Play();
                //Debug.LogError("Step02 is aktiv");
                Switch01.layer = Default;
                Switch01.GetComponent<CheckRotation>().enabled = false;

                // Rotation der Temp auf ein Wert
                moveTowards = true;

                Text02_T.fontStyle = FontStyles.Strikethrough;
                s1 = true;
                //Button Backward and Index Number
                Invoke("ForwardBT", 2f);
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
            Holo_Area06.SetActive(true);
            Machine.Stop();
            //Button Backward and Index Number
            StartCoroutine(WaitButton());
            i = 3;
        }


        public void forward()
        {
            Action[] steps = new Action[]{
                Step00,
                Step01,
                Step03,
                Step04
            };
            i++;
            steps[i]();
        }
        public void backward()
        {
            Action[] steps = new Action[]{
                Step00,
                Step01,
                Step03
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
