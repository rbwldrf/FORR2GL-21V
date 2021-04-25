using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boltThing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public int tick;
    // Update is called once per frame
    void FixedUpdate()
    {
        ++tick; if (tick >= 33) { Destroy(gameObject); }
    }
}
