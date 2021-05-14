using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
        //Skoða hvort leikmaður hefur snert hlut;
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            //Minnka líf leikmanns ef svo er;
            controller.ChangeHealth(-1);
        }
    }

}