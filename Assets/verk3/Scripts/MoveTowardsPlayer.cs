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

    // Update is called once per frame
    void FixedUpdate()
    {
        // f?rir sig a? leikmanni horfir ? ?tt leikmanns, og f?rir sig ?fram eftir local sn?ning
        transform.LookAt(player); GetComponent<Rigidbody>().AddForce(transform.forward*.15f,ForceMode.VelocityChange);
    }
}
