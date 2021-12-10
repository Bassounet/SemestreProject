using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Cinemachine;

public class WalkieSlide : MonoBehaviour
{
    [SerializeField] GameObject SAM;
    [SerializeField] GameObject NOISE;
    [SerializeField] CinemachineVirtualCamera camTalkie;

    private Vector3 screenPoint;
    private Vector3 offset;
    float Obj;
    float DefY;

    Camera MaCam;

    float frequVolume;

    int arrondi;
    // Start is called before the first frame update
    void Start()
    {

            Obj = Random.Range(-12.0f, 12.0f);
        if (0 <= Obj && Obj < 4) { Obj = 4f; }
        if (-4 < Obj && Obj < 0) { Obj = -4f; }
            Debug.Log("valeur:" + Obj.ToString());
       SAM.GetComponent<AudioSource>().volume = 0;
        MaCam = GameObject.FindObjectOfType<Camera>();

        DefY=gameObject.transform.localPosition.y;

    }

    // Update is called once per frame
    void Update()
    {
        Block();
        DetectWalk();
        Son();

    }
    void OnMouseDown()
    {

        
            screenPoint = MaCam.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - MaCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, screenPoint.z));
        
    }

    void OnMouseDrag()
    {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, 0, screenPoint.z);
            Vector3 curPosition = MaCam.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;  
    }
    void Block()
    {
        if (gameObject.transform.localPosition.x > 12)
        {
            gameObject.transform.localPosition = new Vector3(12,gameObject.transform.localPosition.y, 0);
        }
        if (gameObject.transform.localPosition.x < -12)
        {
            gameObject.transform.localPosition = new Vector3(-12, gameObject.transform.localPosition.y, 0);
        }
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, DefY, 0);
    }
    void DetectWalk() {
        if (Obj - 0.2 < gameObject.transform.localPosition.x && gameObject.transform.localPosition.x < Obj + 0.2)
        {

            // LA VICTOIRE 
            Debug.Log("YES");


        }
        else { Debug.Log(gameObject.transform.localPosition.x); }
    }

    void Son()
    {
        //if (gameObject.transform.localPosition.x != 0) {
        //    if (Mathf.Abs(Obj) / Mathf.Abs(gameObject.transform.localPosition.x) < 1)
        //    {
        //Debug.Log(Mathf.Abs(Obj) / Mathf.Round((gameObject.transform.localPosition.x)));
        //   }
        //   if (Mathf.Abs(Obj) / Mathf.Abs(gameObject.transform.localPosition.x) > 1)
        //   {
        //       Debug.Log(Mathf.Abs((1 - Mathf.Abs(Obj)) / Mathf.Round(Mathf.Abs(gameObject.transform.localPosition.x))));
        //   }
        //}
        if (frequVolume < 0)
        {
            frequVolume = 0;
        }
        frequVolume = (Mathf.Abs((Obj) - Mathf.Round((gameObject.transform.localPosition.x))) / (Mathf.Abs((Obj))));
        SAM.GetComponent<AudioSource>().volume = 1-frequVolume;
        NOISE.GetComponent<AudioSource>().volume = frequVolume;

        Debug.Log(1-(Mathf.Abs((Obj) - Mathf.Round((gameObject.transform.localPosition.x))) / (Mathf.Abs((Obj)))));
    }
}
