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

        // finna fjölda lína sem texti þarf að vera til þess að passa í ákveðna stærð textarammans
        float lines = Mathf.Clamp( (float)Mathf.FloorToInt((dialogue.preferredWidth) / (540)), 0, 999); 

        // uppfæra stærð eftir lengd texta og fjölda lína sem sá texti á að vera
        GetComponent<RectTransform>().sizeDelta = new Vector2(
            Mathf.Clamp(dialogue.preferredWidth+24, 24f ,540f + 24f),
            24f+(24f)*(lines+1)
            );
    }
}
