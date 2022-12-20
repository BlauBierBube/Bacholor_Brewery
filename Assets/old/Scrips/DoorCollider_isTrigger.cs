using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider_isTrigger : MonoBehaviour
{
    private Animator anim;
    private AudioSource door;

    private void Start()
    {
        anim = GetComponent<Animator>();
        door = GetComponentInChildren<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.gameObject + "Collision");
        if (other.gameObject.CompareTag("Player"))
        {
            //anim.enabled = true;
            anim.SetBool("Collider_Inside", true);
            door.Play();
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log(this.gameObject + "No Collision");
        if (other.gameObject.CompareTag("Player"))
        {
            //anim.enabled = false;
            anim.SetBool("Collider_Inside", false);
        }
    }
}

