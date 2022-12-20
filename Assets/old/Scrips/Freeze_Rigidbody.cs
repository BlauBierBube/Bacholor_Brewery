using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Freeze_Rigidbody : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FreezeAll()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; 
    }

    public void UnfreezeAll()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.None;
    }




}
