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

        // finna fj�lda l�na sem texti �arf a� vera til �ess a� passa � �kve�na st�r� textarammans
        float lines = Mathf.Clamp( (float)Mathf.FloorToInt((dialogue.preferredWidth) / (540)), 0, 999); 

        // uppf�ra st�r� eftir lengd texta og fj�lda l�na sem s� texti � a� vera
        GetComponent<RectTransform>().sizeDelta = new Vector2(
            Mathf.Clamp(dialogue.preferredWidth+24, 24f ,540f + 24f),
            24f+(24f)*(lines+1)
            );
    }
}
