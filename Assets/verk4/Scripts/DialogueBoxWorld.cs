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
		//ef ekki er settur tími til að uppfæra stafi á
		//að breyta textanum beint um leið og beðið er um
		if (ticksPerCharacter < 0) { 
			text.text = dialogue; 
			
		} else { // ef settur er tími til að uppfæra texta staf fyrir staf

            if (i<dialogue.Length) { ++tick; // uppfæra tíma

				//ef tími er til kominn að uppfæra texta
				if(tick%ticksPerCharacter==0){
				
					//uppfæra texta ef enn vantar upp á
					if(i<length){displayedText+=dialogue[i];}
				}

				//uppfæra textann í boxinu
				text.text = displayedText;
			}
		}

		//ná í lengd textans í boxinu
		i=text.text.Length;

		//ef textinn er allur kominn, er byrjuð niðurtalning
		//sem á að sjá um að eyða textaboxi þegar talningu lýkur
        if (i == dialogue.Length) { 
			lifetime-=Time.fixedDeltaTime;
		}

        if (lifetime <= 0) { Destroy(gameObject); }

	}
}
