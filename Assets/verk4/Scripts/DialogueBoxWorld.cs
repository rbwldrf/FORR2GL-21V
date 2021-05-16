using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBoxWorld : MonoBehaviour
{
	int i, length, tick;
	// Start is called before the first frame update
	void Awake()
	{
		lifetime = timeBeforeDestroy;
		length = dialogue.Length;
	}

	[Multiline]
	public string dialogue;

	public int ticksPerCharacter;

	string displayedText;

	float timeBeforeDestroy = 5, lifetime;

	public TextMeshProUGUI text;

	// Update is called once per frame
	void FixedUpdate()
	{
		//ef ekki er settur t�mi til a� uppf�ra stafi �
		//a� breyta textanum beint um lei� og be�i� er um
		if (ticksPerCharacter < 0) { 
			text.text = dialogue; 
			
		} else { // ef settur er t�mi til a� uppf�ra texta staf fyrir staf

            if (i<dialogue.Length) { ++tick; // uppf�ra t�ma

				//ef t�mi er til kominn a� uppf�ra texta
				if(tick%ticksPerCharacter==0){
				
					//uppf�ra texta ef enn vantar upp �
					if(i<length){displayedText+=dialogue[i];}
				}

				//uppf�ra textann � boxinu
				text.text = displayedText;
			}
		}

		//n� � lengd textans � boxinu
		i=text.text.Length;

		//ef textinn er allur kominn, er byrju� ni�urtalning
		//sem � a� sj� um a� ey�a textaboxi �egar talningu l�kur
        if (i == dialogue.Length) { 
			lifetime-=Time.fixedDeltaTime;
		}

        if (lifetime <= 0) { Destroy(gameObject); }

	}
}
