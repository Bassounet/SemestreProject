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
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Fond;
    [SerializeField] Material YES;

    private Vector3 screenPoint;
    private Vector3 offset;

    Vector3 Default;
    float Obj;
    float DefY;
    public float marge=0.2f;

    Camera MaCam;
    Material matDef;
    float frequVolume;

    int arrondi;

    public bool Win;
    public bool Winned;
    public float TimeToStayForWin;
    // Start is called before the first frame update

    private void Awake()
    {
        matDef = Fond.GetComponent<MeshRenderer>().material;
       Default = gameObject.transform.localPosition;
    }
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
        if (Obj - marge < gameObject.transform.localPosition.x && gameObject.transform.localPosition.x < Obj + marge)
        {

            // LA VICTOIRE 
            Debug.Log("YES");
            Fond.GetComponent<MeshRenderer>().material = YES;
            Win = true;
            Invoke("Winning", TimeToStayForWin);

        }
        else 
        {
            Fond.GetComponent<MeshRenderer>().material = matDef;
            Win = false;

            //Debug.Log(gameObject.transform.localPosition.x);
        
        }
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

        //Debug.Log(1-(Mathf.Abs((Obj) - Mathf.Round((gameObject.transform.localPosition.x))) / (Mathf.Abs((Obj)))));
    }

    public void Winning()
    {

        if (Win)
        {

            Debug.Log("Tu es resté OK");
            gameObject.transform.localPosition = Default;
            Obj = Random.Range(-12.0f, 12.0f);
            if (0 <= Obj && Obj < 4) { Obj = 4f; }
            if (-4 < Obj && Obj < 0) { Obj = -4f; }
            Winned = true;
            Player.GetComponent<testCam>().BackFromTalkie();

        }

    }
}
