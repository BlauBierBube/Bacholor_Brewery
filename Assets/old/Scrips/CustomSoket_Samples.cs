using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Oculus.Interaction;


public class CustomSoket_Samples : MonoBehaviour
{
    public LayerMask Layer;
    public GameObject Attach;
    public Material HoverMat;
    private Material[] mat;
    private Rigidbody rig;
    public bool Freeze = false;
    public bool wasInSoket = false;

    public bool Sample_green = false;
    public bool Sample_blue = false;
    public bool Sample_violet = false;

    private string SampleName1 = "Sample_1";
    private string SampleName2 = "Sample_2";
    private string SampleName3 = "Sample_3";

    public UnityEvent SelectEnter;
    public UnityEvent SelectExit;

    private int count = 0;
    private GameObject Target;
    private GameObject hoverObject;
    private GameObject realObject;


    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    { 

    }

    private void OnTriggerStay(Collider other)
    {
        // Check Layer 
        if ((Layer.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            //Debug.LogError(other.gameObject.name +" Hit with Layermask");
            Target = other.gameObject;
            HoverObject();
            if (Target.GetComponentInParent<Grabbable>()._activeTransformer != null && wasInSoket == true)
            {
                count = 0;

                if (Sample_blue)
                {
                    if (SampleName3 == Target.name)
                    {

                        SelectExit.Invoke();
                        Debug.Log("Orange Sample Removed");

                    }
                }

                if (Sample_green)
                {
                    if (SampleName1 == Target.name)
                    {

                        SelectExit.Invoke();
                        Debug.Log("Orange Sample Removed");

                    }
                }

                if (Sample_violet)
                {
                    if (SampleName2 == Target.name)
                    {

                        SelectExit.Invoke();
                        Debug.Log("Orange Sample Removed");

                    }
                }


                if (Freeze == true)
                {
                    rig.constraints = RigidbodyConstraints.None;
                }

                wasInSoket = false;
            }

            if (Target.GetComponentInParent<Grabbable>()._activeTransformer == null)
            {
                PlaceAtSoket();
            }
        }
    }
    private void PlaceAtSoket()
    {

        if (count == 0)
        {
            DestroyHoverObject();

            Target.transform.parent = Attach.transform;
            Target.transform.rotation = Attach.transform.rotation;
            Target.transform.position = Attach.transform.position;
            if (Freeze == true)
            {
                rig = Target.GetComponent<Rigidbody>();
                rig.constraints = RigidbodyConstraints.FreezeAll;
            }

            if (Sample_blue)
            {
                if (SampleName3 == Target.name)
                {

                    SelectEnter.Invoke();
                    Debug.Log("Orange Sample Correct ");

                }
            }

            if (Sample_green)
            {
                if (SampleName1 == Target.name)
                {

                    SelectEnter.Invoke();
                    Debug.Log("Orange Sample Correct ");

                }
            }

            if (Sample_violet)
            {
                if (SampleName2 == Target.name)
                {

                    SelectEnter.Invoke();
                    Debug.Log("Orange Sample Correct ");

                }
            }

            wasInSoket = true;
            count = 1;
        }
    }



    private void HoverObject()
    {
        if (hoverObject == null && wasInSoket == false)
        {
            //Debug.LogError("Hover Active");
            hoverObject = Instantiate(Target, Attach.transform.position, Attach.transform.rotation);
            hoverObject.transform.parent = Attach.transform;
            hoverObject.layer = 0;
            hoverObject.GetComponent<Collider>().enabled = false;

            //hoverObject.GetComponent<MeshRenderer>().material = HoverMat;

            MeshRenderer[] ren;
            ren = hoverObject.GetComponents<MeshRenderer>();
            foreach (MeshRenderer rend in ren)
            {
                var mats = new Material[rend.materials.Length];
                for (var j = 0; j < rend.materials.Length; j++)
                {
                    mats[j] = HoverMat;
                }
                rend.materials = mats;
            }
        }

    }


    private void DestroyHoverObject()
    {
        if (hoverObject)
        {
            //Debug.LogError("Hover Inactive");
            Destroy(hoverObject);
        }
    }




    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if ((Layer.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            DestroyHoverObject();
            
        }
    }
}