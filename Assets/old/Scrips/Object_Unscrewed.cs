using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Object_Unscrewed : MonoBehaviour
{

    private int LooseScrews = 0;

    public UnityEvent onSolved;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Screw_Loose_Add()
    {
        LooseScrews = LooseScrews + 1;
    }

    // Update is called once per frame
    void Update()
    {
       if(LooseScrews == 4)
        {
            onSolved.Invoke();
        } 
    }

  


    
}
