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
    
    public bool initLoader;
    
    public void ResetScene()
    {
        // færa þennan hlut yfir í nýja senu svo að hægt sé að 
        // skapa hlut í senu sem er óháð vallar eða spil-senum

        if (SceneManager.GetSceneByName("empty").isLoaded) { return; }

        transform.parent = null;
        SceneManager.LoadSceneAsync("empty",LoadSceneMode.Additive);
        GameObject reset = Instantiate(sceneResetter2);
        reset.GetComponent<SceneResetter2>().init = initLoader;
        SceneManager.MoveGameObjectToScene(reset, 
            SceneManager.GetSceneByName("empty"));
        reset.SetActive(true);

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
