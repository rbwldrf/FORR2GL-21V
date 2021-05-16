using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class RubyController : MonoBehaviour
{
	public float speed = 3.0f;

	bool isInvincible;
	float invincibleTimer;
	public float timeInvincible = 2.0f;

	public int maxHealth = 5;
	public int health { get { return currentHealth; } }
	int currentHealth;

	public int gears;

	Animator anim;

	Rigidbody2D rb; Vector2 move;
	Vector2 lookDirection = new Vector2(1, 0);
	public GameObject projectilePrefab;

	public Image hpBar; public TextMeshProUGUI gearIndicator;

	// Start is called before the first frame update
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		
		currentHealth = maxHealth;
	}

	bool hasTalkedToJambi=false,
		hasTalkedToJambiPostFix=false,
		missionComplete=false; 
	
	public GameObject dbw; int botsBroken; public GameObject botCarrier;
	public Canvas worldCanvas;

	public GameObject cogAudio,missionCompleteAudio,hurtSound;

	// Update is called once per frame
	void Update()
	{
		//uppf�ra g�rafj�lda � UI
		gearIndicator.text = gears.ToString();

		//n�llstilla magn bila�a v�lmenna
		botsBroken=0;
		
		//finna �ll v�lmenni
		EnemyController[] bots = botCarrier.GetComponentsInChildren<EnemyController>();

		//telja �au sem enn eru bilu�
		foreach(EnemyController bot in bots){if(bot.broken)++botsBroken;}
		
		//ef engin v�lmenni eru lengur bilu� er verkefni� b�i�, spila hlj�� �egar teki� er fyrst eftir 
		if(botsBroken == 0 && !missionComplete) { Instantiate(missionCompleteAudio); missionComplete=true; }

		move = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

		//spila f�tspor �egar leikma�ur hreyfir sig
		GetComponent<AudioSource>().enabled = !Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f);

		//uppf�ra �tt sem leikma�ur horfir � �egar hann hreyfir sig.
		if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)){
			lookDirection.Set(move.x, move.y); lookDirection.Normalize();}
		
		//uppf�ra animation ef n�gu langt er s��an a� leikma�ur var ska�a�ur.
		if (anim.GetBool("Hit")&&invincibleTimer<timeInvincible-.5f) anim.SetBool("Hit",false);

		//uppf�ra animation eftir �tt og hra�a
		anim.SetFloat("Look X", lookDirection.x);
		anim.SetFloat("Look Y", lookDirection.y);
		anim.SetFloat("Speed", move.magnitude);

		//ef leikma�ur hefur veri� ska�a�ur
		if (isInvincible)
		{
			//minnka t�ma sem leikma�ur hefur til a� for�ast frekari mei�sl  
			invincibleTimer -= Time.deltaTime;
			//sl�kkva � �essu ef t�mi er li�inn
			if (invincibleTimer < 0) {
				isInvincible = false;
			}
		}
		
		//ef leikma�ur hefur g�ra, skal skj�ta �egar be�i� er um
		if (Input.GetButtonDown("Fire"))
		{
			if(gears>0) Launch();
		}

		//�egar tal-takki er �tt �
		if (Input.GetKeyDown(KeyCode.X))
		{
			//sj� hvort einhver s� fyrir framan leikmann
			RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
			
			//ef svo er skal halda �fram
			if (hit.collider != null)
			{
				//ef engin textabox eru fundin � heimi
				if (worldCanvas.GetComponentsInChildren<DialogueBoxWorld>().Length == 0)
				{
					//skapa textabox
					GameObject db = Instantiate(dbw,worldCanvas.transform); DialogueBoxWorld d = db.GetComponent<DialogueBoxWorld>();

					//svo eitthva� s� � textaboxi: �etta s�st ekki og � ekki a� sj�st � leiknum
					string whatToSay = "UNASSIGNED DIALOGUE";

					if (!hasTalkedToJambi)
					{
						if (botsBroken > 0) { whatToSay = // �egar leikma�ur talar vi� Jambi � fyrsta sinn
							"Blessings to you, dear Ruby. It seems the robots have lost "+
							"their marbles again! Since you seem to know how to configure " +
							"them <i>correctly,</i> I'll leave it to you. Good luck!"; 
						} else { whatToSay = // ef leikma�ur lagar �ll v�lmenni ��ur en hann talar vi� Jambi
							"Ever resourceful, Ruby! You wasted no time resolving the task " +
							"at hand! Then again, an engineer of your caliber, I suppose " +
							"you would be pretty used to this by now!";
							hasTalkedToJambiPostFix = true;
						}
						hasTalkedToJambi=true;
                    } else {
                        if (hasTalkedToJambiPostFix) { whatToSay = // ef b�i� er a� tala vi� Jambi eftir a� hafa laga� v�lmenni
							"Beautiful day out, isn't it?";}
                        else {
                            if (botsBroken > 0) { whatToSay = // ef b�i� er a� tala vi� Jambi, en verkefni �loki�.
								"Diagnostics still show a couple of bots still unfixed. " +
								"They can't have gone too far, seeing as we're on an island. ";	
							} else { whatToSay = // ef b�i� er a� tala vi� Jambi og verkefni er loki�.
								"You did it! You fixed all the robots. Last time I try to " +
								"fix those blasted things by myself. Thank you, Ruby!";
								hasTalkedToJambiPostFix = true;
							}
                        }

					}

					//uppf�ra texta sem � a� birtast � textaboxi
					d.dialogue = whatToSay;
				}

			}
		}

		//st�kka/minnka l�fbar me� linear interpolation
		hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, (float)currentHealth / (float)maxHealth,.02f);
	}

	void FixedUpdate()
	{

		Vector2 position = transform.position;
		
		//hreyfa leikmann
		position += speed * move * Time.deltaTime;
		rb.MovePosition(position);
		
		//effect sem � a� spila �egar leikma�ur hefur veri� ska�a�ur.
		GetComponent<SpriteRenderer>().enabled = isInvincible ? 
			!GetComponent<SpriteRenderer>().enabled : true;
	}

	public void Launch()
	{
		//skapa hlut � sta�setningu leikmanns
		GameObject projectileObject = Instantiate(projectilePrefab, 
			rb.position + Vector2.up * 0.5f, Quaternion.identity);

		Projectile projectile = projectileObject.GetComponent<Projectile>();
		
		//f�ra g�r � �tt leikmanns
		projectile.Launch(lookDirection, 300);

		//spila g�ra hlj��
		Instantiate(cogAudio);

		//anim.SetTrigger("Launch");

		//minnka g�rafj�lda
		--gears;
	}

	public void ChangeHealth(int amount)
	{
		//ef � a� minnka l�f
		if (amount < 0)
		{
			//breyta animation
			anim.SetBool("Hit",true);
			//ekki halda �fram ef leikma�ur hefur fyrir sk�mmu veri� ska�a�ur
			if (isInvincible)
				return;

			//spila hlj��
			Instantiate(hurtSound);

			//koma � veg fyrir frekari mei�sl
			isInvincible = true;
			invincibleTimer = timeInvincible;
		}

		//halda l�fi � skefjum
		currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
		
		//ef leikma�ur missir allt l�f skal breyta um senu.
		if (currentHealth == 0) { SceneManager.LoadSceneAsync("ruby_gameover"); }
		
	}
}
