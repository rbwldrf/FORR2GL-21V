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


        Debug.DrawLine(
            transform.position - transform.right * mesh.bounds.extents.x * Mathf.Sin(Mathf.Clamp(((transform.eulerAngles.z - (transform.eulerAngles.z > 180 ? 360 : 0)))*Mathf.Deg2Rad,-.7854f,.7854f)) + Vector3.up * .33f, 
            transform.position - transform.right * mesh.bounds.extents.x * Mathf.Sin(Mathf.Clamp(((transform.eulerAngles.z - (transform.eulerAngles.z > 180 ? 360 : 0)))*Mathf.Deg2Rad,-.7854f,.7854f)) - Vector3.up * .33f);

        bool colAtNadir = Physics.Linecast(transform.position + transform.up * .05f, transform.position - transform.up * .05f, out ray, lm);
        onGround = colAtNadir && ray.collider.tag == "ground";
        speed = Mathf.Lerp(Mathf.Clamp(speed+(onGround?move.y:0),-maxSpeed,maxSpeed),0,.01f);

        Debug.Log( "Vehicle is " + (onGround ? "" : "NOT ") + "on ground.");

        yaw = Mathf.Lerp(move.x * speed * (onGround ? 8f : 0),0, .1f+Mathf.Abs(transform.eulerAngles.z)/45);

        Debug.Log(yaw);

        //Færa bifreið áfram.
        transform.Translate(((Vector3.forward * speed) / (1 + Mathf.Abs(yaw*.005f))) * Time.fixedDeltaTime);
        //Snýr bifreið til hliðar.
        transform.Rotate(Vector3.up * move.x * Mathf.Clamp(speed,-turnSpeed,turnSpeed) * (onGround?1:.2f));


        GetComponent<Rigidbody>().AddRelativeTorque(Vector3.forward * yaw,ForceMode.Acceleration); 
        
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
