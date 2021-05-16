using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+=Vector3.right*Mathf.Sin(Time.time)*.1f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<ItemTracker>()) { 
            collision.collider.GetComponent<ItemTracker>().ChangeHealth(-1);
        }
    }
}
