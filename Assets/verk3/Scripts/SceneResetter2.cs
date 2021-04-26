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
        // afhla�a senum me� �eim h�tti a� be�i� er eftir �v� a� hver einasta sena 
        // s� afhl��u� a� sj�lfri s�r, svo a� v�lin hla�i ekki senum yfir a�rar senur

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
