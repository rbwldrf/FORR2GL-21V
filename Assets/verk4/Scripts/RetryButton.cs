using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Restart()
    {
        //skipta um senu ef ýtt er á takka
        SceneManager.LoadSceneAsync("ruby");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
