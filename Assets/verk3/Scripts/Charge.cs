using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]

public class Charge : MonoBehaviour
{

    float intensity;
    Light light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        intensity = light.intensity;
    }

    public float charge = 100, maxCharge = 100, rechargeRate = .5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        charge+=(rechargeRate*Time.fixedDeltaTime);
        charge = Mathf.Clamp(charge,0,maxCharge);
        light.intensity = intensity * (charge / maxCharge);
    }
}
