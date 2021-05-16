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
		
		gearIndicator.text = gears.ToString();

		botsBroken=0;
		
		EnemyController[] bots = botCarrier.GetComponentsInChildren<EnemyController>();
		foreach(EnemyController bot in bots){if(bot.broken)++botsBroken;}

		if(botsBroken == 0 && !missionComplete) { Instantiate(missionCompleteAudio); missionComplete=true; }

		move = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

		GetComponent<AudioSource>().enabled = !Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f);

		if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
		{
			lookDirection.Set(move.x, move.y);
			lookDirection.Normalize();
		}
				
		if (anim.GetBool("Hit")&&invincibleTimer<timeInvincible-.5f) anim.SetBool("Hit",false);

		anim.SetFloat("Look X", lookDirection.x);
		anim.SetFloat("Look Y", lookDirection.y);
		anim.SetFloat("Speed", move.magnitude);

		if (isInvincible)
		{
			invincibleTimer -= Time.deltaTime;
			if (invincibleTimer < 0) {
				isInvincible = false;
			}
		}
		
		if (Input.GetButtonDown("Fire"))
		{
			if(gears>0) Launch();
		}

		if (Input.GetKeyDown(KeyCode.X))
		{
			RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
			if (hit.collider != null)
			{

				if (worldCanvas.GetComponentsInChildren<DialogueBoxWorld>().Length == 0)
				{
					GameObject db = Instantiate(dbw,worldCanvas.transform); DialogueBoxWorld d = db.GetComponent<DialogueBoxWorld>();

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
                        if (hasTalkedToJambiPostFix) { whatToSay = 
							"Beautiful day out, isn't it?";}
                        else {
                            if (botsBroken > 0) { whatToSay =
								"Diagnostics still show a couple of bots still unfixed. " +
								"They can't have gone too far, seeing as we're on an island. ";	
							} else { whatToSay =
								"You did it! You fixed all the robots. Last time I try to " +
								"fix those blasted things by myself. Thank you, Ruby!";
								hasTalkedToJambiPostFix = true;
							}
                        }

					}

					d.dialogue = whatToSay;
				}

			}
		}

		hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, (float)currentHealth / (float)maxHealth,.02f);
	}

	void FixedUpdate()
	{
		Vector2 position = transform.position;
		position += speed * move * Time.deltaTime;
		rb.MovePosition(position);
		GetComponent<SpriteRenderer>().enabled = isInvincible ? 
			!GetComponent<SpriteRenderer>().enabled : true;
	}

	public void Launch()
	{
		GameObject projectileObject = Instantiate(projectilePrefab, 
			rb.position + Vector2.up * 0.5f, Quaternion.identity);

		Projectile projectile = projectileObject.GetComponent<Projectile>();
		projectile.Launch(lookDirection, 300);

		Instantiate(cogAudio);

		//anim.SetTrigger("Launch");

		--gears;
	}

	public void ChangeHealth(int amount)
	{
		if (amount < 0)
		{
			anim.SetBool("Hit",true);
			if (isInvincible)
				return;

			Instantiate(hurtSound);
			isInvincible = true;
			invincibleTimer = timeInvincible;
		}

		currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
		if (currentHealth == 0) { SceneManager.LoadSceneAsync("ruby_gameover"); }
		
		Debug.Log(currentHealth + "/" + maxHealth);
	}
}
