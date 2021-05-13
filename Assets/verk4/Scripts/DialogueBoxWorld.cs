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

		if (ticksPerCharacter < 0) { 
			
			text.text = dialogue; 
			
		} else {

            if (i<dialogue.Length) { 

				if(tick%ticksPerCharacter==0){
			
					if(i<length){displayedText+=dialogue[i];}

					tick = 0;
			
				}

				text.text = displayedText;

			}
		}

		i=text.text.Length;

        if (i == dialogue.Length) { 
			lifetime-=Time.fixedDeltaTime;
		}

        if (lifetime <= 0) { Destroy(gameObject); }

		++tick;
	}
}
