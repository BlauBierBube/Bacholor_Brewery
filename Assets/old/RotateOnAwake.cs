using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnAwake : MonoBehaviour
{
    public float rotationSpeed = 1;

    // Start is called before the first frame update


    // Update is called once per frame
    public void StartRotate()
    {
        transform.Rotate(0, rotationSpeed, 0, Space.World);
    }
}
