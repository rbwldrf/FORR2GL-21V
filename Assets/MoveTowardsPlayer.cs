using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("player").transform;
    }
    public Transform player;

    public float hp=100,hpCap=100;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(player); GetComponent<Rigidbody>().AddForce(transform.forward*.15f,ForceMode.VelocityChange);
    }
}
