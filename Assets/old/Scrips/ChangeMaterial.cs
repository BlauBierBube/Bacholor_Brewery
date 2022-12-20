using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material material;
    public Texture2D Texture_1;
    public Texture2D Texture_2;


    // Start is called before the first frame update
    void Start()
    {
        material.SetTexture("_BaseMap", Texture_1);
    }

    public void ChangeTexture()
    {
        material.SetTexture("_BaseMap", Texture_2);
    }
}
