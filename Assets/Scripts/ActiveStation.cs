using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oculus.Interaction
{
    public class ActiveStation : MonoBehaviour
    {
        public GameObject[] DisplayObjects;
        public MonoBehaviour[] ControllerList;

        //Material Swap
        private Material[] originalMaterials;
        [SerializeField] Material activeMat;

        [SerializeField] GameObject Cont_01;
        [SerializeField] GameObject Cont_02;
        [SerializeField] GameObject Cont_03;
        [SerializeField] GameObject Cont_04;
        [SerializeField] GameObject Cont_05;
        [SerializeField] GameObject Cont_06;
        [SerializeField] GameObject Cont_07;
        [SerializeField] GameObject Cont_08;


        // Start is called before the first frame update
        void Start()
        {
            // Initialize the array with a size of 8
            DisplayObjects = new GameObject[8];

            // Assign GameObject references to the array elements
            DisplayObjects[0] = GameObject.Find("Display_St_01");
            DisplayObjects[1] = GameObject.Find("Display_St_02");
            DisplayObjects[2] = GameObject.Find("Display_St_03");
            DisplayObjects[3] = GameObject.Find("Display_St_04");
            DisplayObjects[4] = GameObject.Find("Display_St_05");
            DisplayObjects[5] = GameObject.Find("Display_St_06");
            DisplayObjects[6] = GameObject.Find("Display_St_07");
            DisplayObjects[7] = GameObject.Find("Display_St_08");



            ControllerList = new MonoBehaviour[8];

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

        // Update is called once per frame
        void Update()
        {

        }
        void DeaktivateAll()
        {
            foreach (MonoBehaviour m in ControllerList)
            {
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
                /*
                foreach (GameObject obj in DisplayObjects)
                {
                    ResetMaterials(obj);
                }
                
                SaveAndChangeMaterials(DisplayObjects[index]);
                */
                DeaktivateAll();

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

        void SaveAndChangeMaterials(GameObject obj)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            originalMaterials = new Material[renderers.Length];
            for (int i = 0; i < renderers.Length; i++)
            {
                originalMaterials[i] = renderers[i].material;
                renderers[i].material = activeMat;
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