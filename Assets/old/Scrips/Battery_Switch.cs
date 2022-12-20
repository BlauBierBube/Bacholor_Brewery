using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Battery_Switch : MonoBehaviour
{
    public float lockedRotation = 0;
    public float unlockedRotation = 110;

    public GameObject Soket;
    //private bool isActiv = false;
    public UnityEvent locked;
    public UnityEvent unlocked;
    public UnityEvent BatteryLock;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (transform.localEulerAngles.z <= lockedRotation)
        {
            Locked();
            if (Soket.GetComponent<CustomSoket>().wasInSoket == true)
            {
                StartCoroutine(WaitForSeconds());
                BatteryLock.Invoke();
            }
        }

        if (transform.localEulerAngles.z >= unlockedRotation)
        {
            Unlocked();

        }
    }
    private void Locked()
    {
        locked.Invoke();

    }
    private void Unlocked()
    {
        unlocked.Invoke();

    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(5);

    }

}
