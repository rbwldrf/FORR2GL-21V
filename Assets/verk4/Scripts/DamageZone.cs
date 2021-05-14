using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
        //Sko�a hvort leikma�ur hefur snert hlut;
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            //Minnka l�f leikmanns ef svo er;
            controller.ChangeHealth(-1);
        }
    }

}