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
		//uppfæra gírafjölda í UI
		gearIndicator.text = gears.ToString();

		//núllstilla magn bilaða vélmenna
		botsBroken=0;
		
		//finna öll vélmenni
		EnemyController[] bots = botCarrier.GetComponentsInChildren<EnemyController>();

		//telja þau sem enn eru biluð
		foreach(EnemyController bot in bots){if(bot.broken)++botsBroken;}
		
		//ef engin vélmenni eru lengur biluð er verkefnið búið, spila hljóð þegar tekið er fyrst eftir 
		if(botsBroken == 0 && !missionComplete) { Instantiate(missionCompleteAudio); missionComplete=true; }

		move = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

		//spila fótspor þegar leikmaður hreyfir sig
		GetComponent<AudioSource>().enabled = !Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f);

		//uppfæra átt sem leikmaður horfir í þegar hann hreyfir sig.
		if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)){
			lookDirection.Set(move.x, move.y); lookDirection.Normalize();}
		
		//uppfæra animation ef nógu langt er síðan að leikmaður var skaðaður.
		if (anim.GetBool("Hit")&&invincibleTimer<timeInvincible-.5f) anim.SetBool("Hit",false);

		//uppfæra animation eftir átt og hraða
		anim.SetFloat("Look X", lookDirection.x);
		anim.SetFloat("Look Y", lookDirection.y);
		anim.SetFloat("Speed", move.magnitude);

		//ef leikmaður hefur verið skaðaður
		if (isInvincible)
		{
			//minnka tíma sem leikmaður hefur til að forðast frekari meiðsl  
			invincibleTimer -= Time.deltaTime;
			//slökkva á þessu ef tími er liðinn
			if (invincibleTimer < 0) {
				isInvincible = false;
			}
		}
		
		//ef leikmaður hefur gíra, skal skjóta þegar beðið er um
		if (Input.GetButtonDown("Fire"))
		{
			if(gears>0) Launch();
		}

		//þegar tal-takki er ýtt á
		if (Input.GetKeyDown(KeyCode.X))
		{
			//sjá hvort einhver sé fyrir framan leikmann
			RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
			
			//ef svo er skal halda áfram
			if (hit.collider != null)
			{
				//ef engin textabox eru fundin í heimi
				if (worldCanvas.GetComponentsInChildren<DialogueBoxWorld>().Length == 0)
				{
					//skapa textabox
					GameObject db = Instantiate(dbw,worldCanvas.transform); DialogueBoxWorld d = db.GetComponent<DialogueBoxWorld>();

					//svo eitthvað sé í textaboxi: þetta sést ekki og á ekki að sjást í leiknum
					string whatToSay = "UNASSIGNED DIALOGUE";

					if (!hasTalkedToJambi)
					{
						if (botsBroken > 0) { whatToSay = // þegar leikmaður talar við Jambi í fyrsta sinn
							"Blessings to you, dear Ruby. It seems the robots have lost "+
							"their marbles again! Since you seem to know how to configure " +
							"them <i>correctly,</i> I'll leave it to you. Good luck!"; 
						} else { whatToSay = // ef leikmaður lagar öll vélmenni áður en hann talar við Jambi
							"Ever resourceful, Ruby! You wasted no time resolving the task " +
							"at hand! Then again, an engineer of your caliber, I suppose " +
							"you would be pretty used to this by now!";
							hasTalkedToJambiPostFix = true;
						}
						hasTalkedToJambi=true;
                    } else {
                        if (hasTalkedToJambiPostFix) { whatToSay = // ef búið er að tala við Jambi eftir að hafa lagað vélmenni
							"Beautiful day out, isn't it?";}
                        else {
                            if (botsBroken > 0) { whatToSay = // ef búið er að tala við Jambi, en verkefni ólokið.
								"Diagnostics still show a couple of bots still unfixed. " +
								"They can't have gone too far, seeing as we're on an island. ";	
							} else { whatToSay = // ef búið er að tala við Jambi og verkefni er lokið.
								"You did it! You fixed all the robots. Last time I try to " +
								"fix those blasted things by myself. Thank you, Ruby!";
								hasTalkedToJambiPostFix = true;
							}
                        }

					}

					//uppfæra texta sem á að birtast í textaboxi
					d.dialogue = whatToSay;
				}

			}
		}

		//stækka/minnka lífbar með linear interpolation
		hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, (float)currentHealth / (float)maxHealth,.02f);
	}

	void FixedUpdate()
	{

		Vector2 position = transform.position;
		
		//hreyfa leikmann
		position += speed * move * Time.deltaTime;
		rb.MovePosition(position);
		
		//effect sem á að spila þegar leikmaður hefur verið skaðaður.
		GetComponent<SpriteRenderer>().enabled = isInvincible ? 
			!GetComponent<SpriteRenderer>().enabled : true;
	}

	public void Launch()
	{
		//skapa hlut á staðsetningu leikmanns
		GameObject projectileObject = Instantiate(projectilePrefab, 
			rb.position + Vector2.up * 0.5f, Quaternion.identity);

		Projectile projectile = projectileObject.GetComponent<Projectile>();
		
		//færa gír í átt leikmanns
		projectile.Launch(lookDirection, 300);

		//spila gíra hljóð
		Instantiate(cogAudio);

		//anim.SetTrigger("Launch");

		//minnka gírafjölda
		--gears;
	}

	public void ChangeHealth(int amount)
	{
		//ef á að minnka líf
		if (amount < 0)
		{
			//breyta animation
			anim.SetBool("Hit",true);
			//ekki halda áfram ef leikmaður hefur fyrir skömmu verið skaðaður
			if (isInvincible)
				return;

			//spila hljóð
			Instantiate(hurtSound);

			//koma í veg fyrir frekari meiðsl
			isInvincible = true;
			invincibleTimer = timeInvincible;
		}

		//halda lífi í skefjum
		currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
		
		//ef leikmaður missir allt líf skal breyta um senu.
		if (currentHealth == 0) { SceneManager.LoadSceneAsync("ruby_gameover"); }
		
	}
}
