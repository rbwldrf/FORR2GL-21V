using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public InputActionMap a;
    Rigidbody rb;

    public float speed = 2f;

    // Start is called before the first frame update
    void Awake()
    {
        a.Enable();
        rb = GetComponent<Rigidbody>();

        SceneManager.LoadSceneAsync("level00", LoadSceneMode.Additive);
    }

    public Text treasuryIndicator;

    // Update is called once per frame
    void FixedUpdate() {
        Vector2 move = a["move"].ReadValue<Vector2>();
        rb.MovePosition(transform.position+(transform.right*move.x+transform.forward*move.y)*speed*Time.fixedDeltaTime);
        treasuryIndicator.text = treasury.ToString("0");
        }

    public float treasury;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Coin>()) {
            Destroy(other.gameObject);
            ++treasury;
        }
        if (other.GetComponent<LevelChanger>()) {
            other.GetComponent<LevelChanger>().ChangeLevel();
            transform.position=Vector3.zero;
        }
    }
}
