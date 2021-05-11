using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public RectTransform db;

    public bool show;

    [Multiline]
    public string dialogue;

    public TextMeshProUGUI dText;

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = GetComponent<RectTransform>().anchoredPosition;

        dText.text = dialogue;

        GetComponent<RectTransform>().anchoredPosition = 
            Vector2.Lerp(pos, Vector2.right * (show ? 0 : -2500),.2f);
        db.sizeDelta = Vector2.Lerp(db.sizeDelta,new Vector2(
            show&&pos.x>-100&&!string.IsNullOrEmpty(dialogue)?512:0,192),.03f);
    }
}
