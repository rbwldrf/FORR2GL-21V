using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public Transform player;
    public Vector3 offset;
    public float speedMult = 1;

    public V3_Player p;

    float yaw;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (p.hp > 0) { 
            yaw=Mathf.Clamp(yaw-Input.GetAxis("Mouse Y"),-90,90);
            transform.eulerAngles=Vector3.up*(transform.eulerAngles.y+Input.GetAxis("Mouse X"))+Vector3.right*yaw;
        }
        //uppfærir staðsetningu með .
        transform.position = Vector3.Lerp(transform.position, player.position+
            transform.right*offset.x+Vector3.up*offset.y+transform.forward*offset.z
            , .01f * Mathf.Pow(Vector3.Distance(transform.position,player.position),2)*speedMult);
    }
}
