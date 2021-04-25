using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityStandardAssets.Characters.ThirdPerson ;

public class V3_Player : MonoBehaviour
{
    private void Start()
    {
        if(!SceneManager.GetSceneByName("world").IsValid())SceneManager.LoadSceneAsync("world",LoadSceneMode.Additive);
    }

    // Start is called before the first frame update
    void Awake()
    {
        a = GetComponent<Animator>();
    }

    public bool aiming;
    Animator a;

    public float charge=20, chargeCap=100;
    public float hp=73, hpCap=100;

    public float score;
    public Transform model, canvas, hand;

    public Image hpIndicator,chargeIndicator;

    public Camera cam;

    public Text scoreIndicator;

    public GameObject gameOver, boltObject;

    // Update is called once per frame
    void Update()
    {

        scoreIndicator.text = score.ToString("0");

        if (hp > 0) { 
            aiming = Input.GetButton("Aim");

            cam.GetComponent<FollowPlayer>().offset = !aiming ? new Vector3(2,1,-3.33f) : new Vector3(.75f,1.33f,-.5f);
            cam.GetComponent<FollowPlayer>().speedMult = !aiming ? 5 : 25;

            //ef leikmaður er að miða, á að bæta animation ofan á vanalega animation
            a.SetLayerWeight(1,Mathf.Lerp(a.GetLayerWeight(1),(aiming?.9f:0),.33f));
            //snúa model leikmanns í þá átt sem myndavélin snýr þegar hann miðar
            if (aiming) { 
                
                model.eulerAngles = Vector3.up * cam.transform.eulerAngles.y;

                if (Input.GetButtonDown("Fire")&& charge >= 5) { charge -= 5;
                    RaycastHit hit; GameObject bolt = Instantiate(boltObject);
                    bolt.GetComponent<LineRenderer>().SetPosition(0,hand.position);
                    if(Physics.Linecast(cam.transform.position,cam.transform.position+cam.transform.forward*999999,out hit)){
                        bolt.GetComponent<LineRenderer>().SetPosition(1,hit.point);
                        if (hit.transform.GetComponent<Enemy>()) { hit.transform.GetComponent<Enemy>().hp-=Random.Range(37.5f,250); }
                    } else
                    {
                        bolt.GetComponent<LineRenderer>().SetPosition(1,cam.transform.position+cam.transform.forward*999999);
                    }

                }

                }

            else { model.localEulerAngles = Vector3.zero;  }

            //uppfæra ui
            hpIndicator.fillAmount = Mathf.Lerp(hpIndicator.fillAmount, hp /hpCap,.33f); 
            chargeIndicator.fillAmount = Mathf.Lerp(chargeIndicator.fillAmount,charge/chargeCap,.33f);
        
            ++score;    
        }
        if (hp <= 0) { // ef leikmaður deyr
            Cursor.lockState = CursorLockMode.None;
            GetComponent<ThirdPersonCharacter>().enabled = false;
            GetComponent<ThirdPersonUserControl>().enabled = false;
            Instantiate(gameOver,canvas); enabled = false;
        }    
    }
    private void OnGUI()
    {

    }
}
