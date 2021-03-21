using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float verticalInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get the user's vertical input
        // sækja input notanda
        verticalInput = Input.GetAxis("Vertical");

        // move the plane forward at a constant rate
        // færa flaug áfram stanslaust
        transform.Translate(Vector3.forward * speed);

        // tilt the plane up/down based on up/down arrow keys
        // stýra flaug með input notanda
        transform.Rotate(Vector3.right * rotationSpeed * verticalInput * Time.deltaTime);
    }
}
