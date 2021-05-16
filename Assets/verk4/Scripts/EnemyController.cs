using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public float speed;
	public bool vertical;
	public float changeTime = 3.0f;

	Rigidbody2D rigidbody2D;
	float timer;
	int direction = 1;

	public GameObject smoke;

	Animator animator;

	public GameObject fixSound;

	public bool broken = true;

	// Start is called before the first frame update
	void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		
		timer = changeTime;
	}

	public void Fix()
	{
		//spila hljóð sem á að heyrast þegar vélmenni er lagað
		Instantiate(fixSound);
		//vélmenni er lagað, breyta bool-inu sem 
		//gefur til kynna hvort vélmenni er bilað
		broken = false;
		//kyrrsetja vélmenni
		rigidbody2D.simulated = false;
		//hætta að spila particle effect þegar vélmenni er lagað
		smoke.GetComponent<ParticleSystem>().Stop(false,ParticleSystemStopBehavior.StopEmitting);
	}

	void Update()
	{
		//ekki færa vélmenni sem er lagað
		if (!broken) return;
		
		//minnka gefinn tíma um það tímabil sem hefur liðið milli ramma
		timer -= Time.deltaTime;

		//ef vélmenni hefur gengið nógu lengi í ákveðna átt
		if (timer < 0)
		{
			//snúa við og byrja tíma upp á nýtt
			direction = -direction;
			timer = changeTime;
		}
	}

	void FixedUpdate()
	{
		//spila hljóð sem á að spilast ef vélmenni er bilað
		transform.GetChild(1).GetComponent<AudioSource>().enabled=broken;

		//finna staðsetningu í heimi
		Vector2 position = rigidbody2D.position;

		//færa vélmenni í tilgefna átt og breyta í við-
		//eigandi animation fyrir tilgefna átt.

		if (vertical)
		{
			position.y = position.y + Time.deltaTime * speed * direction;
			animator.SetFloat("Move X", 0);
			animator.SetFloat("Move Y", direction);
		}
		else
		{
			position.x = position.x + Time.deltaTime * speed * direction;
			animator.SetFloat("Move X", direction);
			animator.SetFloat("Move Y", 0);
		}

		//breyta animation eftir því hvort vélmenni sé bilað eða ekki
		animator.SetFloat("Moving", broken ? 1 : 0);

		//hreyfa vélmenni
		rigidbody2D.MovePosition(position);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		//sjá hvort leikmaður hefur snert vélmenni
		RubyController player = other.gameObject.GetComponent<RubyController>();

		//ef svo er, er dregið frá lífi leikmanns.
		if (player != null) {
			player.ChangeHealth(-1);
		}
	}
}