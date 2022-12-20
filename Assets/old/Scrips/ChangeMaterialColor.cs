using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialColor : MonoBehaviour
{

    public Material material;
    public Color32 ColorStart;
    public Color32 ColorUpdate;


    // Start is called before the first frame update
    void Start()
    {
        material.SetColor("_EmissionColor", ColorStart);
    }

    public void ChangeMaterial()
    {
        material.SetColor("_EmissionColor", ColorUpdate);
    }
}
