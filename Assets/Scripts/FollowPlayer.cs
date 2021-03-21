using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform player;
    public Vector3 offset;
    // Update is called once per frame
    void FixedUpdate()
    {
        //uppfærir staðsetningu með .
        transform.position = Vector3.Lerp(transform.position, player.position+offset, .01f * Vector3.Distance(transform.position,player.position));
    }
}
