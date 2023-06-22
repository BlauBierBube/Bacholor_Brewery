using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Liquid_Beer_Glas : MonoBehaviour
{


    public enum UpdateMode { Normal, UnscaledTime }
    public UpdateMode updateMode;

    [SerializeField]
    float MaxWobble = 0.03f;
    [SerializeField]
    float WobbleSpeedMove = 1f;
    [SerializeField]
    float Recovery = 1f;
    [SerializeField]
    float Thickness = 1f;
    [Range(0, 1)]
    public float CompensateShapeAmount;
    [SerializeField]
    Mesh mesh;
    [SerializeField]
    Renderer rend;
    Vector3 pos;
    Vector3 lastPos;
    Vector3 velocity;
    Quaternion lastRot;
    Vector3 angularVelocity;
    float wobbleAmountX;
    float wobbleAmountZ;
    float wobbleAmountToAddX;
    float wobbleAmountToAddZ;
    float pulse;
    float sinewave;
    float time = 0.5f;
    Vector3 comp;
    float fillValue;

    [SerializeField]
    bool empty = false;
    bool isInCollider = false;
    private bool isFillingUp = false;
    [SerializeField] float StartFillAmount = 0.42f;
    [SerializeField] float CurrentFillAmount = 0.42f;
    [SerializeField] float MinFillAmount = 0.42f;
    [SerializeField] float MaxFillAmount = 0.62f;
    [SerializeField] float AngleAdjust = 0.0012f;
    [SerializeField] float StartTiltAngle = 42f;
    [SerializeField] float EmptyAngle = 110f;
    private GameObject Fluid;



    // Use this for initialization
    void Start()
    {
        GetMeshAndRend();
        
        //CurrentFillAmount = fillValue;
        
        //CurrentFillAmount = StartFillAmount;
        //if (CurrentFillAmount > EmptyAngle) empty = true;
        //else empty = false;

    }

    private void OnValidate()
    {
        GetMeshAndRend();
    }

    void GetMeshAndRend()
    {
        if (mesh == null)
        {
            mesh = GetComponent<MeshFilter>().sharedMesh;
        }
        if (rend == null)
        {
            rend = GetComponent<Renderer>();
        }
    }
    void Update()
    {
        CurrentFillAmount = MapAngleToValue();
        
        float deltaTime = 0;
        switch (updateMode)
        {
            case UpdateMode.Normal:
                deltaTime = Time.deltaTime;
                break;

            case UpdateMode.UnscaledTime:
                deltaTime = Time.unscaledDeltaTime;
                break;
        }

        time += deltaTime;

        if (deltaTime != 0)
        {


            // decrease wobble over time
            wobbleAmountToAddX = Mathf.Lerp(wobbleAmountToAddX, 0, (deltaTime * Recovery));
            wobbleAmountToAddZ = Mathf.Lerp(wobbleAmountToAddZ, 0, (deltaTime * Recovery));



            // make a sine wave of the decreasing wobble
            pulse = 2 * Mathf.PI * WobbleSpeedMove;
            sinewave = Mathf.Lerp(sinewave, Mathf.Sin(pulse * time), deltaTime * Mathf.Clamp(velocity.magnitude + angularVelocity.magnitude, Thickness, 10));

            wobbleAmountX = wobbleAmountToAddX * sinewave;
            wobbleAmountZ = wobbleAmountToAddZ * sinewave;



            // velocity
            velocity = (lastPos - transform.position) / deltaTime;

            angularVelocity = GetAngularVelocity(lastRot, transform.rotation);

            // add clamped velocity to wobble
            wobbleAmountToAddX += Mathf.Clamp((velocity.x + (velocity.y * 0.2f) + angularVelocity.z + angularVelocity.y) * MaxWobble, -MaxWobble, MaxWobble);
            wobbleAmountToAddZ += Mathf.Clamp((velocity.z + (velocity.y * 0.2f) + angularVelocity.x + angularVelocity.y) * MaxWobble, -MaxWobble, MaxWobble);
        }

        // send it to the shader
        rend.sharedMaterial.SetFloat("_WobbleX", wobbleAmountX);
        rend.sharedMaterial.SetFloat("_WobbleZ", wobbleAmountZ);

        // set fill amount
        UpdatePos(deltaTime);

        // keep last position
        lastPos = transform.position;
        lastRot = transform.rotation;
    }

    void UpdatePos(float deltaTime)
    {

        Vector3 worldPos = transform.TransformPoint(new Vector3(mesh.bounds.center.x, mesh.bounds.center.y, mesh.bounds.center.z));
        if (CompensateShapeAmount > 0)
        {
            // only lerp if not paused/normal update
            if (deltaTime != 0)
            {
                comp = Vector3.Lerp(comp, (worldPos - new Vector3(0, GetLowestPoint(), 0)), deltaTime * 10);
            }
            else
            {
                comp = (worldPos - new Vector3(0, GetLowestPoint(), 0));
            }

            pos = worldPos - transform.position - new Vector3(0, CurrentFillAmount - (comp.y * CompensateShapeAmount), 0);
        }
        else
        {
            pos = worldPos - transform.position - new Vector3(0, CurrentFillAmount, 0);
        }
        rend.sharedMaterial.SetVector("_FillAmount", pos);
    }

    //https://forum.unity.com/threads/manually-calculate-angular-velocity-of-gameobject.289462/#post-4302796
    Vector3 GetAngularVelocity(Quaternion foreLastFrameRotation, Quaternion lastFrameRotation)
    {
        var q = lastFrameRotation * Quaternion.Inverse(foreLastFrameRotation);
        // no rotation?
        // You may want to increase this closer to 1 if you want to handle very small rotations.
        // Beware, if it is too close to one your answer will be Nan
        if (Mathf.Abs(q.w) > 1023.5f / 1024.0f)
            return Vector3.zero;
        float gain;
        // handle negatives, we could just flip it but this is faster
        if (q.w < 0.0f)
        {
            var angle = Mathf.Acos(-q.w);
            gain = -2.0f * angle / (Mathf.Sin(angle) * Time.deltaTime);
        }
        else
        {
            var angle = Mathf.Acos(q.w);
            gain = 2.0f * angle / (Mathf.Sin(angle) * Time.deltaTime);
        }
        Vector3 angularVelocity = new Vector3(q.x * gain, q.y * gain, q.z * gain);

        if (float.IsNaN(angularVelocity.z))
        {
            angularVelocity = Vector3.zero;
        }
        return angularVelocity;
    }

    float GetLowestPoint()
    {
        float lowestY = float.MaxValue;
        Vector3 lowestVert = Vector3.zero;
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {

            Vector3 position = transform.TransformPoint(vertices[i]);

            if (position.y < lowestY)
            {
                lowestY = position.y;
                lowestVert = position;
            }
        }
        return lowestVert.y;
    }


    float MapAngleToValue()
    {
        /*
        // Scales the CurrentFillAmount "up" so the Liquid goes down when the Bottle is Tilted
        // Uses the Y and Z Angle of the Parent, X Angle gives trouble because of the Gimbal Lock Problem

        float lastValue = CurrentFillAmount;

        Vector3 parentRotation = transform.parent.transform.localRotation.eulerAngles;

        // Adjust the Z rotation to be clamped between 0 and 180 degrees
        float zRotation = Mathf.Clamp(parentRotation.z, 0f, 180f);

        // Apply the adjusted rotation to the child object
        transform.localRotation = Quaternion.Euler(parentRotation.x, parentRotation.y, zRotation);

        // Retrieve the modified Euler angles
        float xRotation = transform.localRotation.eulerAngles.x;
        float yRotation = transform.localRotation.eulerAngles.y;
        zRotation = transform.localRotation.eulerAngles.z;

        //Debug.LogError("x = " + xRotation);
        //Debug.LogError("y = " + yRotation);
        //Debug.LogError("z = " + zRotation);

        // Perform your desired logic for mapping angles to values
        // ...

        float maxValue = Mathf.Max(yRotation, zRotation);
        float StepPerDegree = ((MaxFillAmount - MinFillAmount) / (EmptyAngle - StartTiltAngle) - AngleAdjust);
        float currentValue = (maxValue - StartTiltAngle) * StepPerDegree + MinFillAmount;
        */
        // Scales the CurrentFillAmount "up" so the Liquide goes down when the Bottle is Tilted
        // Uses the Y and Z Angle of the Parent, X Angle gives trouble because of the Gible Lock Problem 

        // Scales the CurrentFillAmount "up" so the Liquide goes down when the Bottle is Tilted
        // Uses the Y and Z Angle of the Parent, X Angle gives trouble because of the Gible Lock Problem 

        //CurrentFillAmount = fillValue;

        float lastValue = CurrentFillAmount;
        //Debug.LogError("fillValue = " + fillValue);
        //Debug.LogError("CuFillAmount = " + CurrentFillAmount);
        //Debug.LogError("lastValue = " + lastValue);
        Vector3 parentRotation = transform.parent.transform.localRotation.eulerAngles;



        float xRotation = Mathf.Repeat(parentRotation.x, 360);
        if (xRotation > 180)
        {
            xRotation -= 360;
        }
        float yRotation = Mathf.Repeat(parentRotation.y, 360);
        if (yRotation > 180)
        {
            yRotation -= 360;
        }
        float zRotation = Mathf.Repeat(parentRotation.z, 360);
        if (zRotation > 180)
        {
            zRotation -= 360;
        }
        //Debug.LogError("x = " + xRotation);
        //Debug.LogError("y = " + yRotation);
        //Debug.LogError("z = " + zRotation);
        xRotation = Mathf.Abs(xRotation);
        yRotation = Mathf.Abs(yRotation);
        zRotation = Mathf.Abs(zRotation);



        // i use y and z angle because if i would use the x angle the Gible Lock Problem would be affect
        // Start by 0 count to 90 by 90 degree back to 0 by 180 degree -> Gible Lock Problem 


        if (yRotation >= EmptyAngle) yRotation = 180;
        if (zRotation >= EmptyAngle) zRotation = 180;

        if (yRotation <= StartTiltAngle) yRotation = StartTiltAngle;
        if (zRotation <= StartTiltAngle) zRotation = StartTiltAngle;


        float maxValue = Mathf.Max(yRotation, zRotation);
        float StepPerDegree = ((MaxFillAmount - MinFillAmount) / (EmptyAngle - StartTiltAngle) - AngleAdjust);
        float currentValue = (maxValue - StartTiltAngle) * StepPerDegree + MinFillAmount;

        lastValue = Mathf.Max(lastValue, currentValue);

        if (lastValue >= MaxFillAmount)
        {
            empty = true;
            lastValue = 1;
        }


        float scale = CurrentFillAmount - 0.001f;
        float highestScale = Mathf.Min(scale, CurrentFillAmount);
        if (isInCollider)
        {
            lastValue = highestScale;
        }
        return lastValue;
    }


    
    public void OnStay()
    {
        //Debug.LogError("Is in Collider Fluid");
        if (!isInCollider)
        {
            empty = false;
            isInCollider = true;
            //CurrentFillAmount = MaxFillAmount;
            if (CurrentFillAmount >= MaxFillAmount)
                CurrentFillAmount = MaxFillAmount-0.01f;
        }

    }
    public void OnExit()
    {
        //Debug.LogError("Is OUT Collider Fluid");
        isInCollider = false;
    }
    
    /*
    //new
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Fluid"))
        {
  
            if (!isInCollider)
            {
                //Debug.LogError("is in Collider");
                if (CurrentFillAmount >= MaxFillAmount)
                    CurrentFillAmount = MaxFillAmount-0.01f;
                isInCollider = true;
                empty = false;
            }

        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fluid"))
        {
            if (CurrentFillAmount >= MaxFillAmount)
                CurrentFillAmount = MaxFillAmount - 0.01f;
            empty = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        //Debug.LogError("is out of Collider");
        isInCollider = false;
        SScale = CurrentFillAmount;
    }
    */
}