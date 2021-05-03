using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    bool isInvincible;
    float invincibleTimer;
    public float timeInvincible = 2.0f;

    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;

    Rigidbody2D rb; Vector2 move;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position += speed * move * Time.deltaTime;
        rb.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
