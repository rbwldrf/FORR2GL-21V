using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    bool isInvincible;
    float invincibleTimer;
    public float timeInvincible = 2.0f;

    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;

    Animator anim;

    Rigidbody2D rb; Vector2 move;
    Vector2 lookDirection = new Vector2(1, 0);
    public GameObject projectilePrefab;

    public Image hpBar;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

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
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
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

        //anim.SetTrigger("Launch");
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            anim.SetBool("Hit",true);
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
