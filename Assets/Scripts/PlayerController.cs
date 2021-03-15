using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        r = GetComponent<Rigidbody>();
        a.Enable();
    }

    Rigidbody r;

    public InputActionMap a;
    public float speed;
    public float turnSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 move = a["move"].ReadValue<Vector2>();
        //Færa bifreið áfram.
        transform.Translate(((transform.forward * speed * move.y) / (1 + r.velocity.sqrMagnitude)) * Time.fixedDeltaTime);
        //Snýr bifreið til hliðar.
        transform.Rotate(transform.up * turnSpeed * move.x);

    }


}
