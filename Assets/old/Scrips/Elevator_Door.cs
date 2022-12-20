using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Door : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
   private void Start()
    {
        anim = GetComponent<Animator>();
    }

   public void CallElevator()
    {
        anim.SetBool("OnPressedCall", true);

        Debug.Log("Called Elevator");

    }


   public void CommandElevator()
    {
        anim.SetBool("OnPressedCommand", true);
        Debug.Log("Commanded Elevator");

    }
}
