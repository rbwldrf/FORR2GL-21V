using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //sjá um að eyða hlut ef það hljóð sem tengt er hlutnum er hætt að spila  
        if(!GetComponent<AudioSource>().isPlaying) Destroy(gameObject);
    }
}
