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
        float lines = Mathf.Clamp((float)Mathf.FloorToInt(dialogue.preferredWidth / (540 + dialogue.margin.x + dialogue.margin.z)), 0, 999);

        GetComponent<RectTransform>().sizeDelta = new Vector2(
            Mathf.Clamp(dialogue.preferredWidth,64,540f),
            (dialogue.fontSize*1.5f+dialogue.margin.y+dialogue.margin.w)
            +(dialogue.fontSize*1.5f+dialogue.lineSpacing*dialogue.fontSize*.01f)*lines
            );
    }
}
