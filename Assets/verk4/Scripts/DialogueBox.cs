using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// �ESSI K��I ER EKKI NOTA�UR

// texti talmanns s�ndur � HUD � sta� �ess a� birtast � heiminum 


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
        //f� sta�setningu hlutar
        Vector2 pos = GetComponent<RectTransform>().anchoredPosition;

        //uppf�ra texta
        dText.text = dialogue;

        //f�ra dialogue box inn � skj� ef be�i� er um �a�
        GetComponent<RectTransform>().anchoredPosition = 
            Vector2.Lerp(pos, Vector2.right * (show ? 0 : -2500),.2f);

        //st�kka box ef be�i� er um �a�, og ef �a� er texti sem � a� birta
        db.sizeDelta = Vector2.Lerp(db.sizeDelta,new Vector2(
            show&&pos.x>-100&&!string.IsNullOrEmpty(dialogue)?512:0,192),.03f);
    }
}
