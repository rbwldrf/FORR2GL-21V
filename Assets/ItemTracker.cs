using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ItemTracker : MonoBehaviour
{

    public TextMeshProUGUI gemCounter;

    int hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }
    public int gems; public int maxHp = 3;
    // Update is called once per frame
    void Update()
    {
        gemCounter.text = gems.ToString();

        if (hp <= 0){
            SceneManager.LoadSceneAsync("ruby");
        }
    }

    public void ChangeHealth(int amount)
    {
        hp+=amount;
    }
}
