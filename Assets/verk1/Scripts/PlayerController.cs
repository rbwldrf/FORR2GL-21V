using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        r = GetComponent<Rigidbody>();
        a.Enable();
    }

    Rigidbody r;
    public Mesh mesh;

    public InputActionMap a;
    public float maxSpeed;
    public float turnSpeed;

    [HideInInspector]
    public bool onGround;

    public LayerMask lm;

    float speed; 
    float yaw;

    // Update is called once per frame
    void FixedUpdate()
    {

        RaycastHit ray;

        Vector2 move = a["move"].ReadValue<Vector2>();

        //Debug.Log(((transform.eulerAngles.z - (transform.eulerAngles.z > 180 ? 360 : 0)) * (transform.eulerAngles.z > 180?-1:1)));

        float nomodAngle = (transform.eulerAngles.z - (transform.eulerAngles.z > 180 ? 360 : 0));
        Vector3 p = transform.position - transform.right * mesh.bounds.extents.x * Mathf.Sin(Mathf.Clamp((nomodAngle) * Mathf.Deg2Rad, -.7854f, .7854f)) ;

        //Debug.DrawLine(p1,p2);
                
        //Hraðar á sér einungis ef ákveðin hlið bíls snertir yfirborð.
        //Linear interpolation notað til að gefa einfalda hröðun.
        onGround = Physics.Linecast(p + Vector3.up * .33f, p - Vector3.up * .33f, out ray, lm) && ray.collider.tag == "ground";
        speed = Mathf.Lerp(Mathf.Clamp(speed+(onGround?move.y:0),-maxSpeed,maxSpeed),0,.01f);

        //Debug.Log( "Vehicle is " + (onGround ? "" : "NOT ") + "on ground.");
        //yaw = Mathf.Lerp(move.x * speed * (onGround ? 6f : 0),0, .75f+Mathf.Abs(nomodAngle)/22.5f); Debug.Log(yaw);

        //Færa bifreið áfram.
        transform.Translate(((Vector3.forward * speed) /* / (1 + Mathf.Abs(yaw*.005f)) */)  * Time.fixedDeltaTime);
        //Snýr bifreið til hliðar byggt á hraða bíls, með stillt hámark á snúning.
        transform.Rotate(Vector3.up * move.x * Mathf.Clamp(speed,-turnSpeed,turnSpeed) * (onGround?1:.2f));


        //GetComponent<Rigidbody>().AddRelativeTorque(Vector3.forward * yaw,ForceMode.Acceleration); 
        
        if (a["respawn"].triggered || transform.position.y < -100) { Respawn(); }

    }

    void Respawn() {
        transform.position = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
    }

    void OnGUI()
    {
    }

}
