using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Liquid_Beer1: MonoBehaviour
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


    [SerializeField]
    bool empty = false;
    bool isInCollider = false;

    private GameObject Fluid;
    [SerializeField] float StartFillAmount = 0.42f;
    [SerializeField] float CurrentFillAmount = 0.42f;
    [SerializeField] float MinFillAmount = 0.42f;
    [SerializeField] float MaxFillAmount = 0.62f;
    [SerializeField] float AngleAdjust = 0.0012f;
    [SerializeField] float StartTiltAngle = 42f;
    [SerializeField] float EmptyAngle = 110f;

    private bool cup = true;
    // Use this for initialization
    void Start()
    {
        GetMeshAndRend();
        CurrentFillAmount = StartFillAmount;
        if (CurrentFillAmount > EmptyAngle) empty = true;
        else empty = false;
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
        if (transform.Find("Cup") == null)
            cup = false;

        if (empty == false && cup == false)
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
        float lastValue = CurrentFillAmount;
        float x = 0;
        float z = 0;
        Quaternion rotation = transform.parent.localRotation;
        rotation.ToAngleAxis(out float angle, out Vector3 axis);
        if (axis.x > axis.z)
        {
            x = angle * Mathf.Sign(axis.x);
            if (x < 0)
            {
                x = -x;
            }
            if (x > 180)
            {
                x = 360 - x;
            }
        }
        else
        {
            z = angle * Mathf.Sign(axis.z);
            if (z < 0)
            {
                z = -z;
            }
            if (z > 180)
            {
                z = 360 - z;
            }
        }
        //Debug.LogError("X = " + x);
        //Debug.LogError("Z = " + z);
        if (x >= EmptyAngle) x=180;
        if (z >= EmptyAngle) z=180;

        if (x <= StartTiltAngle) x = StartTiltAngle;
        if (z <= StartTiltAngle) z = StartTiltAngle;


        float maxValue = Mathf.Max(x, z);
        //Debug.LogError("maxValue = " + maxValue);
        float StepPerDegree = ((MaxFillAmount - MinFillAmount) / (EmptyAngle - StartTiltAngle)-AngleAdjust);
        //Debug.LogError("Steps = " + StepPerDegree);
        float currentValue = (maxValue- StartTiltAngle) * StepPerDegree + MinFillAmount;

        lastValue = Mathf.Max(lastValue, currentValue);
        if (lastValue >= MaxFillAmount)
        {
            empty = true;
        }

        if (isInCollider) // Scale for other Fluid to fill up
        {
            // Compare Scale of new Fluid and Scale it down so the Fluid is showing
            float lastScale = Fluid.GetComponent<Liquid_Beer1>().CurrentFillAmount;
            float scale = 0.66f - (currentValue - MinFillAmount) * 4f;
            float highestScale = Mathf.Min(scale, lastScale);


            Fluid.GetComponent<Liquid_Beer1>().CurrentFillAmount = highestScale;

        }

        return lastValue;

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Fluid"))
        {
            Fluid = other.gameObject;
            //Debug.LogError("Is in Collider");
            isInCollider = true;
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brew03"))
        {
            //Debug.LogError("Is in Collider");
            empty = false;
            CurrentFillAmount = MinFillAmount;
        }
    }
    void OnTriggerExit(Collider other)
    {
        isInCollider = false;
    }
}