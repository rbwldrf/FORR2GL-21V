using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBoxScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        dialogue = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    TextMeshProUGUI dialogue; public float sizeMult;

    // Update is called once per frame
    void Update()
    {

        string text = dialogue.text;
        int count  = text.Replace(" ",string.Empty).Length;
        
        float marginX = dialogue.margin.y + dialogue.margin.w;
        float marginY = dialogue.margin.x + dialogue.margin.z;

        // finna fjölda lína sem texti þarf að vera til þess að passa í ákveðna stærð textarammans
        float lines = Mathf.Clamp( (float)Mathf.FloorToInt((dialogue.preferredWidth) / (540+ marginX)), 0, 999); 

        GetComponent<RectTransform>().sizeDelta = new Vector2(
            Mathf.Clamp(dialogue.preferredWidth, marginX ,540f + marginX),
            marginY*2f+(dialogue.fontSize*1.33f)*lines
            );
    }
}
