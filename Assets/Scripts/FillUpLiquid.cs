using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FillUpLiquid : MonoBehaviour
{
    [SerializeField]
    public UnityEvent OnEnter;


    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogError(MapAngleToValue());
    }

    void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.CompareTag("Fluid"))
        {
            OnEnter.Invoke();
        }
        MapAngleToValue();
        Debug.LogError(MapAngleToValue());
    }
    float MapAngleToValue()
    {

        float x = transform.localEulerAngles.x;
        float y = transform.localEulerAngles.y;
        if (x > 90) x = 90;
        if (x < -90) x = -90;
        if (y > 90) y = 90;
        if (y < -90) y = -90;
        if (x <= 30 && x >= -30) x = 30;
        if (y <= 30 && y >= -30) y = 30;
        float maxValue = Mathf.Max(x, y);
        return (maxValue - 30) * 0.1f / (90 - 30) + 0.47f;
    }
}
