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
        //sj� um a� ey�a hlut ef �a� hlj�� sem tengt er hlutnum er h�tt a� spila  
        if(!GetComponent<AudioSource>().isPlaying) Destroy(gameObject);
    }
}
