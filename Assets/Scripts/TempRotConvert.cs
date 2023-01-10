using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Oculus.Interaction;
using TMPro;

public class TempRotConvert : MonoBehaviour
{
    private float currentRotation;

    public bool AlwaysAktive = true;
    private bool isSolved = false;

    public float TagetArea = 100;
    public float TagetRange = 10;

    public UnityEvent onSolved;

    [SerializeField] GameObject Needle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSolved != true && AlwaysAktive == true || gameObject.GetComponent<Grabbable>()._activeTransformer != null) // If GameObject is Grabbed{
        {

            //Zielbereich +-Range/2 fuer Tolleranz
            if (currentRotation <= (TagetArea + (TagetRange / 2)) && currentRotation >= (TagetArea - (TagetRange / 2)) || currentRotation == TagetArea)
            {
                StartCoroutine(WaitSec());
            }
            else
            {
                StopAllCoroutines();
            }


            // 0 = 180
            // -100 = 300  302
            // 100 = 60     58
            // from -0.15 = -1 and 0.15 = 1
            currentRotation = 180 + (135 * (transform.localPosition.z * (100/15)));


            if (Needle != null)
                Needle.transform.localEulerAngles = new Vector3(0f, currentRotation, 0f);
        }

    }
    IEnumerator WaitSec()
    {
        yield return new WaitForSeconds(2);
        Solved();
    }

    private void Solved()
    {
        StopAllCoroutines();
        isSolved = true;
        //Textfield.text = "is Solved";
        onSolved.Invoke();
    }
}
