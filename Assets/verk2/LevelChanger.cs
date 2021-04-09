using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
    }


    public string levelName;
    public void ChangeLevel() {
        SceneManager.LoadSceneAsync(levelName,LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
