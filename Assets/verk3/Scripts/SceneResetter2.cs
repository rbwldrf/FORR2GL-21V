using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneResetter2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("loading scenes");
        StartCoroutine(waitForUnload());
    }

    public bool init;

    IEnumerator waitForUnload()
    {
        // afhla�a senum me� �eim h�tti a� be�i� er eftir �v� a� hver einasta sena 
        // s� afhl��u� a� sj�lfri s�r, svo a� v�lin hla�i ekki senum yfir a�rar senur
        AsyncOperation ao;

        if(!init){
            ao = SceneManager.UnloadSceneAsync("gameplay");
            yield return ao; 

            ao = SceneManager.UnloadSceneAsync("world");
            yield return ao; }       
              
        if (init){
            ao = SceneManager.UnloadSceneAsync("init");
            yield return ao; }       

        

        ao = SceneManager.LoadSceneAsync("gameplay",LoadSceneMode.Additive);
        yield return ao;
        
        if (!SceneManager.GetSceneByName("world").isLoaded) { 
            ao = SceneManager.LoadSceneAsync("world",LoadSceneMode.Additive);
            yield return ao; }

        SceneManager.UnloadSceneAsync("empty");        
        yield return ao;

    }

    // Update is called once per frame
    void Update()
    {
        //

    }
}
