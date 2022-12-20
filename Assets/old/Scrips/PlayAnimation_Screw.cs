using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation_Screw : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

   public void Screwdriver_Socket_Enter()
    {
        anim.SetBool("Screwdriver_Socket", true);
    }
}
