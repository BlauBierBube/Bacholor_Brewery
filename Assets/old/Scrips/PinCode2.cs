using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PinCode2 : MonoBehaviour
{
    public string RightPinCode;
    public TextMeshPro CodeTextMesh;
    public Material material;
    public UnityEvent onSolved;

    // Start is called before the first frame update
    void Start()
    {
        CodeTextMesh.color = new Color32(254, 9, 0, 255);
    }

    private string FourOnly(string s)
    {
        while (s.Length > 4) s = s.Substring(1);
        return s;
    }

    [System.NonSerialized] public string EnteredPinCode = "";

    public void UserClickedButtonNumbered(int digitNumber)
    {
        Debug.Log("that button name is " + digitNumber);
        EnteredPinCode = EnteredPinCode + digitNumber.ToString();
        EnteredPinCode = FourOnly(EnteredPinCode);
        Debug.Log("So far, the user entered: " + EnteredPinCode);
    }

    // Update is called once per frame
    void Update()
    {
        CodeTextMesh.text = EnteredPinCode;

        if (EnteredPinCode == RightPinCode)
        {
            Debug.Log("Unlocked!");

            CodeTextMesh.color = new Color32(0, 254, 111, 255);
            if(material != null)
                material.SetColor("_BaseColor", new Color(0.3361072f, 0.7830188f, 0.3612165f, 1));
            onSolved.Invoke();

            this.enabled = false;
        }
    }  
}
