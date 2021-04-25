using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        dayLength = Mathf.FloorToInt(360/sun.speed.x); 
    }
    public long tick;
    public spin sun;
    long dayLength;
    GameObject go;

    public Light sunLight,moonLight;

    // Update is called once per frame
    void FixedUpdate()
    {
        bool dayDelay = (tick) < (dayLength / 24) || tick > ((dayLength / 2) - (dayLength / 24));
        bool day = tick < (dayLength / 2);
        //f�ra inn e�a �t hluti undir �essum hlut eftir �v� hvort s� dagur e�a n�tt
        transform.GetChild(0).gameObject.SetActive(dayDelay);
        
        sunLight.enabled = (day);
        moonLight.enabled = (!day);
        ++tick; tick %= dayLength;
    }
}
