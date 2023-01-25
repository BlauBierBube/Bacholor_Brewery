using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupScript : MonoBehaviour
{
    public GameObject Liquid;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Opener"))
        {
            if (other.transform.eulerAngles.x >= 20 && other.transform.eulerAngles.x <= 90)
            {
                Transform currentParent = transform.parent;
                Transform grandParent = currentParent.parent;
                currentParent.parent = grandParent.parent;
                Rigidbody rb = transform.parent.gameObject.AddComponent<Rigidbody>();
                Liquid.GetComponent<Liquid_Beer_Bottle>().CheckCup();
                Destroy(gameObject);
            }
            
        }
    }
}
