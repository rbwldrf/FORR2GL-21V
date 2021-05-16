using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public GameObject pickupSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object that entered the trigger : " + other);

        //sjá hvort leikmaður hefur snert hlut
        RubyController controller = other.GetComponent<RubyController>();

        //ef svo er, er bætt við líf leikmanns.
        if (controller != null)
        {
            //en aðeins ef að hann er undir lífshámarki.
            if (controller.health < controller.maxHealth)
            {
                //spila pickup hljóð
                Instantiate(pickupSound);
                
                //bæta lífi við líf leikmanns
                controller.ChangeHealth(1);

                //eyða hlut úr heimi.
                Destroy(gameObject);
            }
        }
    }
}
