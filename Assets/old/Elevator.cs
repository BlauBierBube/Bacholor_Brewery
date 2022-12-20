using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public bool isOnTargetPos;
    public float speed = 1f;
    public Vector3 target;
    public GameObject Rig;
    ///public AudioSource Audio;

    public void Start()
    {
    }

    public void Update()
    {
        if (isOnTargetPos == true && transform.position != target)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            Rig.transform.position = Vector3.MoveTowards(Rig.transform.position, target, step);
        }
    }
    public void Down()
    {
        isOnTargetPos = true;
        Invoke("Speech", 6);
    }

    /*private void Speech()
    {
        Audio.Play();
    }*/
}
