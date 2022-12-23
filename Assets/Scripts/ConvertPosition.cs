using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConvertPosition : MonoBehaviour
{
    public GameObject TopVisual;
    public GameObject BotVisual;
    public GameObject TextTop;
    public GameObject TextBot;
    public GameObject ParentText;
    private TMP_Text TTop;
    private TMP_Text TBot;
    public float ScaleTop = 2;
    public float ScaleBot = 2;
    public float TextTopScale;
    public float TextBotScale;
    public Vector3 Offset;



    private bool isOnFinish = false;
 
    // Start is called before the first frame update
    void Start()
    {
        TextTop.SetActive(false);
        TextBot.SetActive(false);
        TTop = TextTop.GetComponent<TMP_Text>();
        TBot = TextBot.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnFinish == false)
        {

            TTop.text = ((TopVisual.transform.localScale.z*100) / (ScaleTop + ScaleBot)) +"%";
            TBot.text = ((BotVisual.transform.localScale.z*100) / (ScaleTop + ScaleBot)) + "%";
            TopVisual.transform.position = transform.position;
            ParentText.transform.position = transform.position;


            if (BotVisual.transform.localScale.z >= TextBotScale)
            {
                TextBot.SetActive(true);
            }
            if (TopVisual.transform.localScale.z >= TextTopScale)
            {
                TextTop.SetActive(true);
            }


            if (BotVisual.transform.localScale.z == ScaleBot)
            {
                isOnFinish = true;
            }
        }
    }
}
