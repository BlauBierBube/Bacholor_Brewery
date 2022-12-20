using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Velocity : MonoBehaviour
{
    public Rigidbody tool;
    public MeshRenderer rend;

    private float targetRotation = 5;
    private float numberRotation = 0;
    public Material Led_1;
    public Material Led_2;
    public Material Led_3;
    public Material Led_4;
    public Material Led_5;


    void Start()
    {
        tool = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log(tool.angularVelocity);
    }
}
