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
		//spila hlj�� sem � a� heyrast �egar v�lmenni er laga�
		Instantiate(fixSound);
		//v�lmenni er laga�, breyta bool-inu sem 
		//gefur til kynna hvort v�lmenni er bila�
		broken = false;
		//kyrrsetja v�lmenni
		rigidbody2D.simulated = false;
		//h�tta a� spila particle effect �egar v�lmenni er laga�
		smoke.GetComponent<ParticleSystem>().Stop(false,ParticleSystemStopBehavior.StopEmitting);
	}

	void Update()
	{
		//ekki f�ra v�lmenni sem er laga�
		if (!broken) return;
		
		//minnka gefinn t�ma um �a� t�mabil sem hefur li�i� milli ramma
		timer -= Time.deltaTime;

		//ef v�lmenni hefur gengi� n�gu lengi � �kve�na �tt
		if (timer < 0)
		{
			//sn�a vi� og byrja t�ma upp � n�tt
			direction = -direction;
			timer = changeTime;
		}
	}

	void FixedUpdate()
	{
		//spila hlj�� sem � a� spilast ef v�lmenni er bila�
		transform.GetChild(1).GetComponent<AudioSource>().enabled=broken;

		//finna sta�setningu � heimi
		Vector2 position = rigidbody2D.position;

		//f�ra v�lmenni � tilgefna �tt og breyta � vi�-
		//eigandi animation fyrir tilgefna �tt.

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

		//breyta animation eftir �v� hvort v�lmenni s� bila� e�a ekki
		animator.SetFloat("Moving", broken ? 1 : 0);

		//hreyfa v�lmenni
		rigidbody2D.MovePosition(position);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		//sj� hvort leikma�ur hefur snert v�lmenni
		RubyController player = other.gameObject.GetComponent<RubyController>();

		//ef svo er, er dregi� fr� l�fi leikmanns.
		if (player != null) {
			player.ChangeHealth(-1);
		}
	}
}