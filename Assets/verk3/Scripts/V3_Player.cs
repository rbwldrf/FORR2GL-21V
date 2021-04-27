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
        if(SceneManager.GetSceneByName("init").isLoaded)
        {   SceneManager.UnloadSceneAsync("init"); 
            transform.position = Vector3.zero;    
        }
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

    public Text scoreIndicator, chargeTextOuter, chargeTextInner;

    public GameObject gameOver, boltObject;

    // Update is called once per frame
    void FixedUpdate()
    {

        scoreIndicator.text = score.ToString("0");

        if (hp > 0) { 
            aiming = Input.GetButton("Aim");

            cam.GetComponent<FollowPlayer>().offset = !aiming ? new Vector3(2,1,-3.33f) : new Vector3(.75f,1.33f,-.5f);
            cam.GetComponent<FollowPlayer>().speedMult = !aiming ? 5 : 25;

            //ef leikma�ur er a� mi�a, � a� b�ta animation ofan � vanalega animation
            a.SetLayerWeight(1,Mathf.Lerp(a.GetLayerWeight(1),(aiming?.9f:0),.33f));
            if (aiming) { 
                //sn�a model leikmanns � �� �tt sem myndav�lin sn�r �egar hann mi�ar
                model.eulerAngles = Vector3.up * cam.transform.eulerAngles.y;

                if (Input.GetButtonDown("Fire")&& charge >= 5) { charge -= 5;
                    
                    //b�ta line renderer � heim og setja byrjunarpunkt � hendi leikmanns
                    RaycastHit hit; GameObject bolt = Instantiate(boltObject);
                    bolt.GetComponent<LineRenderer>().SetPosition(0,hand.position);

                    //f�ra endapunkt �anga� sem a� skoti� lendir og draga fr� l�fi �vina ef skot hittir
                    if(Physics.Linecast(cam.transform.position,cam.transform.position+cam.transform.forward*999999,out hit)){
                        bolt.GetComponent<LineRenderer>().SetPosition(1,hit.point);
                        if (hit.transform.GetComponent<Enemy>()) { hit.transform.GetComponent<Enemy>().hp-=Random.Range(37.5f,250); }
                    } else
                    {
                        //setja endapunkt �anga� sem skoti� fer �n �ess a� lenda nokkurs sta�ar
                        bolt.GetComponent<LineRenderer>().SetPosition(1,cam.transform.position+cam.transform.forward*999999);
                    }

                }

                }

            else { model.localEulerAngles = Vector3.zero;  }

            //uppf�ra ui
            hpIndicator.fillAmount = Mathf.Lerp(hpIndicator.fillAmount, hp /hpCap,.33f); 
            chargeIndicator.fillAmount = Mathf.Lerp(chargeIndicator.fillAmount,charge/chargeCap,.33f);

            chargeTextOuter.text = (charge/chargeCap*100).ToString("0.0"); chargeTextInner.text = chargeTextOuter.text;
        
            ++score; 
            
            charge = Mathf.Clamp(charge+.011f, 0, chargeCap);
            hp = Mathf.Clamp(hp+.002f,0,hpCap);

        }
        if (hp <= 0) { // ef leikma�ur deyr
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
