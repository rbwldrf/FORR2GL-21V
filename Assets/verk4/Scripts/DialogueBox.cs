using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public RectTransform db;

    public bool show;

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = GetComponent<RectTransform>().anchoredPosition;

        GetComponent<RectTransform>().anchoredPosition = 
            Vector2.Lerp(pos, Vector2.right * (show ? 0 : -2500),.2f);
        db.sizeDelta = Vector2.Lerp(db.sizeDelta,new Vector2(
            show&&pos.x>-100?512:0,192),.03f);
    }
}
