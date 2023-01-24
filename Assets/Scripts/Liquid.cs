using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Liquid : MonoBehaviour
{
    public enum UpdateMode { Normal, UnscaledTime }
    public UpdateMode updateMode;

    [SerializeField]
    float MaxWobble = 0.03f;
    [SerializeField]
    float WobbleSpeedMove = 1f;
    [SerializeField]
    float fillAmount = 0.5f;
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
    [SerializeField]
    GameObject Fluid;


    // Use this for initialization
    void Start()
    {
        GetMeshAndRend();
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
        if (empty == false) // If glas is empty
            fillAmount = MapAngleToValue();
        


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

            pos = worldPos - transform.position - new Vector3(0, fillAmount - (comp.y * CompensateShapeAmount), 0);
        }
        else
        {
            pos = worldPos - transform.position - new Vector3(0, fillAmount, 0);
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
        float lastValue = fillAmount;
        float x = transform.parent.localEulerAngles.x;
        float y = transform.parent.localEulerAngles.y;

        //Debug.LogError("x = "+ transform.parent.localEulerAngles.x);
        //Debug.LogError("y = " + transform.parent.localEulerAngles.y);

        if (x > 90 && x < 270) x = 180;
        if (y > 90 && y < 270) y = 180;

        // not carring
        if (x <= 30 || x >= 330) x = 30;
        if (y <= 30 || y >= 330) y = 30;

        //330 - 270
        if (x > 270 && x < 330) x = 360 - x;
        if (y > 270 && y < 330) y = 360 - y;



        float maxValue = Mathf.Max(x, y);
        //Debug.LogError(maxValue);
        float currentValue = (maxValue - 30) * 0.065f / (90 - 30) + 0.47f;
        //Debug.LogError(currentValue);
        lastValue = Mathf.Max(lastValue, currentValue); //max current 0.57
        if (lastValue >= 0.57)
        {
            empty = true;
            //Debug.LogError("trigger");
        }

        if (isInCollider)
        {
            // Compare Scale of new Fluid and Scale it down so the Fluid is showing
            float lastScale = Fluid.GetComponent<Liquid>().fillAmount;
            float scale = 0.66f - (currentValue-0.47f) * 4f;
            float highestScale = Mathf.Min(scale, lastScale);
            
            Fluid.GetComponent<Liquid>().fillAmount = highestScale;
            
            //0.66 start
            //0.42 Ende

            //0.26 dif

            //0.065 dif
        }

        return lastValue;

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Fluid"))
        {
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
            fillAmount = 0.47f;
        }
    }
    void OnTriggerExit(Collider other)
    {
        isInCollider = false;
    }
        
}