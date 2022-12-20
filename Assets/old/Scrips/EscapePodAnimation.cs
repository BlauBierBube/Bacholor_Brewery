using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePodAnimation : MonoBehaviour
{
    private Animator anim;

    public GameObject Rig;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AniCloseDoor()
    {
        anim.SetBool("ButtonPressed", true);
    }

    public void AniEscapePodUndock()
    {
        anim.SetBool("LeverPulled", true);
    }

    public void PlaceRigEscapePod()
    {
        Rig.transform.parent = this.transform;
    }

  
}
