using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneResetter2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(waitForUnload());
    }

    IEnumerator waitForUnload()
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync("gameplay");
        yield return ao;

        ao = SceneManager.UnloadSceneAsync("world");
        yield return ao;        
                
        ao = SceneManager.LoadSceneAsync("world",LoadSceneMode.Additive);
        yield return ao;

        ao = SceneManager.LoadSceneAsync("gameplay",LoadSceneMode.Additive);
        yield return ao;

        SceneManager.UnloadSceneAsync("empty");
    }

    // Update is called once per frame
    void Update()
    {
        //

    }
}
