using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneResetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject sceneResetter2;
    public void ResetScene()
    { 
        transform.parent = null;
        SceneManager.LoadSceneAsync("empty",LoadSceneMode.Additive);
        SceneManager.MoveGameObjectToScene(gameObject, 
            SceneManager.GetSceneByName("empty"));
        Instantiate(sceneResetter2);
        
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
