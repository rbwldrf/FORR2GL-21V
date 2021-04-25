using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sér um að bæta óvinum í heim á ákveðnu tímabili
public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    int tick = 0; public GameObject enemyToSpawn, coin;
    // Update is called once per frame
    void FixedUpdate()
    {
        ++tick; if (tick >= 150) { tick = 0; Instantiate(enemyToSpawn,new Vector3(Random.Range(-250,270),30,Random.Range(-350,400)),Quaternion.identity,transform); }
        if (tick % 16 == 0) {Instantiate(coin,new Vector3(Random.Range(-250,270),30,Random.Range(-350,400)),Quaternion.identity,transform); }

    }
}
