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
        // afhlaða senum með þeim hætti að beðið er eftir því að hver einasta sena 
        // sé afhlöðuð að sjálfri sér, svo að vélin hlaði ekki senum yfir aðrar senur
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
