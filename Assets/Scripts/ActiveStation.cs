using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction
{
    public class ActiveStation : MonoBehaviour
    {
        public GameObject[] DisplayObjects;
        public MonoBehaviour[] ControllerList;

        [SerializeField] Material deactivateMat;
        [SerializeField] Material activeMat;

        [SerializeField] GameObject Cont_01;
        [SerializeField] GameObject Cont_02;
        [SerializeField] GameObject Cont_03;
        [SerializeField] GameObject Cont_04;
        [SerializeField] GameObject Cont_05;
        [SerializeField] GameObject Cont_06;
        [SerializeField] GameObject Cont_07;
        [SerializeField] GameObject Cont_08;

        void Start()
        {
            DisplayObjects = new GameObject[8];
            ControllerList = new MonoBehaviour[8];

            // Assign GameObject references to the array elements
            DisplayObjects[0] = GameObject.Find("Display_St_01");
            DisplayObjects[1] = GameObject.Find("Display_St_02");
            DisplayObjects[2] = GameObject.Find("Display_St_03");
            DisplayObjects[3] = GameObject.Find("Display_St_04");
            DisplayObjects[4] = GameObject.Find("Display_St_05");
            DisplayObjects[5] = GameObject.Find("Display_St_06");
            DisplayObjects[6] = GameObject.Find("Display_St_07");
            DisplayObjects[7] = GameObject.Find("Display_St_08");

            // Assign GameObject references to the array elements
            ControllerList[0] = Cont_01.GetComponent<Controller_01>();
            ControllerList[1] = Cont_02.GetComponent<Controller_02>();
            ControllerList[2] = Cont_03.GetComponent<Controller_03>();
            ControllerList[3] = Cont_04.GetComponent<Controller_04>();
            ControllerList[4] = Cont_05.GetComponent<Controller_05>();
            ControllerList[5] = Cont_06.GetComponent<Controller_06>();
            ControllerList[6] = Cont_07.GetComponent<Controller_07>();
            ControllerList[7] = Cont_08.GetComponent<Controller_08>();
        }



        void DeaktivateAll()
        {
            foreach (MonoBehaviour m in ControllerList)
            {
                foreach (GameObject obj in DisplayObjects)
                {
                    DeactivateMat(obj);
                }
                if (m != null)
                {
                    if (m is Controller_01 controller01)
                    {
                        controller01.Deaktivate();
                    }
                    else if (m is Controller_02 controller02)
                    {
                        controller02.Deaktivate();
                    }
                    else if (m is Controller_03 controller03)
                    {
                        controller03.Deaktivate();
                    }
                    else if (m is Controller_04 controller04)
                    {
                        controller04.Deaktivate();
                    }
                    else if (m is Controller_05 controller05)
                    {
                        controller05.Deaktivate();
                    }
                    else if (m is Controller_06 controller06)
                    {
                        controller06.Deaktivate();
                    }
                    else if (m is Controller_07 controller07)
                    {
                        controller07.Deaktivate();
                    }
                    else if (m is Controller_08 controller08)
                    {
                        controller08.Deaktivate();
                    }
                }
            }
        }

        public void activeState(int index)
        {
            if (index >= 0 && index < DisplayObjects.Length)
            {
                DeaktivateAll();
                ActivateMat(DisplayObjects[index]);

                if (ControllerList[index] is Controller_01 controller01)
                {
                    controller01.Aktivate();
                }
                else if (ControllerList[index] is Controller_02 controller02)
                {
                    controller02.Aktivate();
                }
                else if (ControllerList[index] is Controller_03 controller03)
                {
                    controller03.Aktivate();
                }
                else if (ControllerList[index] is Controller_04 controller04)
                {
                    controller04.Aktivate();
                }
                else if (ControllerList[index] is Controller_05 controller05)
                {
                    controller05.Aktivate();
                }
                else if (ControllerList[index] is Controller_06 controller06)
                {
                    controller06.Aktivate();
                }
                else if (ControllerList[index] is Controller_07 controller07)
                {
                    controller07.Aktivate();
                }
                else if (ControllerList[index] is Controller_08 controller08)
                {
                    controller08.Aktivate();
                }
            }
        }
         
        void DeactivateMat(GameObject obj)
        {
            Renderer[] renderers = obj.GetComponents<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.material = deactivateMat;
            }
            //Debug.Log(obj.name + " deactivated Mat");
        }

        void ActivateMat(GameObject obj)
        {
            Renderer[] renderers = obj.GetComponents<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.material = activeMat;
            }
            //Debug.Log(obj.name + " activated Mat");
        }
    }
}
