using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<V3_Player>()) { other.GetComponent<V3_Player>().hp--; other.GetComponent<V3_Player>().score-=10; }
    }
}
