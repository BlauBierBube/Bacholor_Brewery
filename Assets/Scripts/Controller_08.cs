using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;



namespace Oculus.Interaction
{
    public class Controller_08 : MonoBehaviour
    {
        [SerializeField] GameObject Holo_Area08;

        [SerializeField] GameObject Text01;
        [SerializeField] GameObject Text02;
        [SerializeField] TMP_Text Text02_F;
        [SerializeField] GameObject Text03;
        [SerializeField] GameObject Text04;

        [SerializeField] GameObject Forward_BT;
        [SerializeField] GameObject Backward_BT;

        [SerializeField] GameObject Switch01;

        [SerializeField] GameObject Finish;

        [SerializeField] Material BrewMat07;

        [SerializeField] AudioSource Machine;

        private int i = 0;
        private int Default;
        public int HighlightLayer = 6;

        private bool s1 = false;



        public void Deaktivate()
        {
            Holo_Area08.SetActive(false);
            Text01.SetActive(false);
            Text02.SetActive(false);
            Text03.SetActive(false);
            Text04.SetActive(false);
        }
        public void Aktivate()
        {
            Holo_Area08.SetActive(true);
            Text01.SetActive(true);
        }
        public void StepOutOfHolo()
        {
            Holo_Area08.SetActive(true);
            Text01.SetActive(false);
            Text02.SetActive(false);
            Text03.SetActive(false);
            Text04.SetActive(false);
        }

        public void Step00()
        {
            //Debug.LogError("Step00 is aktiv");
            Text01.SetActive(true);
            Holo_Area08.SetActive(false);
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
            s1 = false;
            Switch01.layer = HighlightLayer;
            Text02_F.fontStyle = FontStyles.Normal;
            Switch01.GetComponent<CheckRotation>().enabled = true;
            //Button Backward and Index Number
            StartCoroutine(WaitButton());
            i = 1;
        }


        public void Step02() // Switch 1 Filter
        {
            if (s1 == false)
            {
                //Debug.LogError("Step02 is aktiv");
                Switch01.layer = Default;
                Switch01.GetComponent<CheckRotation>().enabled = false;
                Machine.Play();
                Text02_F.fontStyle = FontStyles.Strikethrough;
                s1 = true;
                Invoke("ForwardBT", 2f);
            }

        }


        public void Step03() // 
        {
            Text02.SetActive(false);
            Text03.SetActive(true);
            //Buttons and Index Number
            StartCoroutine(WaitButton());
            Invoke("ForwardBT", 2f);
            i = 2;
        }

        public void Step04() // 
        {
            Text03.SetActive(false);
            Text04.SetActive(true);
            //Debug.LogError("Step04 is aktiv");
            Machine.Stop();
            Finish.SetActive(true);
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
