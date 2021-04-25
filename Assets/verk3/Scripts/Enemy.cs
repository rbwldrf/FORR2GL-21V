using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float hp=100,hpCap=100;
    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) { Destroy(gameObject); }
    }
}
