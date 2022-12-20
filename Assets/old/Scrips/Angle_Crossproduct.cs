using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle_Crossproduct : MonoBehaviour
{
    public enum RotationDirection
    {
        NoRotation,
        Leftward,
        Rightward
    }

    Vector3 oldForward;
    RotationDirection rotationDirection;

    void Start()
    {
        oldForward = transform.forward;
    }

    void Update()
    {
        Vector3 cross = Vector3.Cross(oldForward, transform.forward);

        if(cross.y > 0f)
        {
            rotationDirection = RotationDirection.Rightward;
        }

        else if(cross.y < 0f)
        {
            rotationDirection = RotationDirection.Leftward;
        }

        else
        {
            rotationDirection = RotationDirection.NoRotation;
        }

        oldForward = transform.forward;
    }
    



}
