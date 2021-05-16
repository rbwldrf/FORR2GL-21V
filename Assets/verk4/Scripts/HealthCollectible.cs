using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public GameObject pickupSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object that entered the trigger : " + other);

        //sj� hvort leikma�ur hefur snert hlut
        RubyController controller = other.GetComponent<RubyController>();

        //ef svo er, er b�tt vi� l�f leikmanns.
        if (controller != null)
        {
            //en a�eins ef a� hann er undir l�fsh�marki.
            if (controller.health < controller.maxHealth)
            {
                //spila pickup hlj��
                Instantiate(pickupSound);
                
                //b�ta l�fi vi� l�f leikmanns
                controller.ChangeHealth(1);

                //ey�a hlut �r heimi.
                Destroy(gameObject);
            }
        }
    }
}
