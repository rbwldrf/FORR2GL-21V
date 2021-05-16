using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{

    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        //f�ra skot �fram
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //sj� hvort skot hefur snert v�lmenni
        EnemyController e = other.collider.GetComponent<EnemyController>();
        //ef svo er, er �a� v�lmenni laga�.
        if (e != null) e.Fix();

        if (other.collider.GetComponent<Arcade>()) { SceneManager.LoadSceneAsync("Main"); }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //ey�a hlut ef hann hefur fari� of langt
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }
}
