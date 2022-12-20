using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookChildRig : MonoBehaviour
{
    private int i = 0;
    private Rigidbody rig;

    public void Start()
    {
        FreezeChild();
    }

    public void FreezeChild()
    {
        GameObject[] allChildren = new GameObject[transform.childCount];

        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }
        foreach (GameObject child in allChildren)
        {
            rig = child.gameObject.GetComponent<Rigidbody>();
            rig.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void unFreezeChild()
    {

        GameObject[] allChildren = new GameObject[transform.childCount];

        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }
        foreach (GameObject child in allChildren)
        {
            rig = child.gameObject.GetComponent<Rigidbody>();
            rig.constraints = RigidbodyConstraints.None;
        }
    }


}
