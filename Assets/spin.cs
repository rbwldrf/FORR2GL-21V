﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Vector3 speed;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(speed, Space.Self);
    }
}
