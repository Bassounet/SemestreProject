using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TalkieManager : MonoBehaviour
{
    [SerializeField] GameObject Talkie;
    [SerializeField] AudioSource radio;
    [SerializeField] AudioSource talk;

    float val;
    float Obj;
    public float marge = 0.2f;
    
    [Range(0f, 0.99f)]
    public float reduc;
    public bool voiceAffected;

    float frequVolume;
    //--------------------------//
    [SerializeField] GameObject Player;

    public bool Win;
    public bool Winned;
    public float TimeToStayForWin=2;
    public bool Intro;
    public bool AldyWin;
    //---------------------------//

    [SerializeField] GameObject Fond;
    [SerializeField] Material YES;
    [SerializeField] Material matDef;


    private void Awake()
    {
        matDef = Fond.GetComponent<MeshRenderer>().material;
    }

        // Start is called before the first frame update
        void Start()
    {
        Obj = Random.Range(0f, 10.0f);
        if (5 <= Obj && Obj < 6) { Obj = 6f; }
        if (4 < Obj && Obj < 5) { Obj = 4f; }
        Debug.Log("valeur:" + Obj.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        val = Talkie.GetComponent<Slider>().value*10;
        Debug.Log("Valeur:"+val);
        if (Talkie.GetComponent<Slider>().value <= 0)
        {
            Talkie.GetComponent<Slider>().value=0.01f;
        }
        detect();
        Son();

    }
    void detect()
    {
        if (Obj - marge < val && val < Obj + marge)
        {
            if (Intro)
            {
                if (!AldyWin)
                {

                    // LA VICTOIRE 
                    Debug.Log("YesIntro");
                    Fond.GetComponent<MeshRenderer>().material = YES;
                    Win = true;
                    Winning();
                    AldyWin = true;

                }
            }
            else
            {

                // LA VICTOIRE 
                Debug.Log("YES");
                Fond.GetComponent<MeshRenderer>().material = YES;
                Win = true;
                Invoke("Winning", TimeToStayForWin);

            }

        }
        else
        {
            Fond.GetComponent<MeshRenderer>().material = matDef;
            Win = false;

            //Debug.Log(gameObject.transform.localPosition.x);

        }
    }
    void Son() {
        if (frequVolume < 0)
        {
            frequVolume = 0;
        }

        frequVolume = ((Mathf.Abs((Obj) - Mathf.Round(val)) / (Mathf.Abs((Obj)))));
        Debug.Log("frequ:"+frequVolume);
        if (!voiceAffected) { talk.GetComponent<AudioSource>().volume = (1 - frequVolume); }
        else {
            if ((1 - frequVolume) - reduc < 0) { talk.GetComponent<AudioSource>().volume = 0; }
            else { talk.GetComponent<AudioSource>().volume = (1 - frequVolume) - reduc; }

        }
        if (frequVolume - reduc < 0) { radio.GetComponent<AudioSource>().volume = 0; }
        else{radio.GetComponent<AudioSource>().volume = (frequVolume) - reduc; }


        if (radio.GetComponent<AudioSource>().volume > 1 - reduc) { radio.GetComponent<AudioSource>().volume = 1 - reduc; }

        //DEBUG
        Debug.Log("Voice:" + talk.GetComponent<AudioSource>().volume);
        Debug.Log("Radio:" + radio.GetComponent<AudioSource>().volume);
    }

    public void Winning()
    {

        if (Win)
        {
            if (Intro)
            {
                Debug.Log("C'est parti");
                SceneManager.LoadScene("SampleScene");
            }
            else
            {

                Debug.Log("Tu es resté OK");
                Talkie.GetComponent<Slider>().value = 0.5f;
                Obj = Random.Range(0f, 10.0f);
                if (5 <= Obj && Obj < 6) { Obj = 6f; }
                if (4 < Obj && Obj < 5) { Obj = 4f; }
                Winned = true;
                Win = false;
                Player.GetComponent<testCam>().BackFromTalkie();


            }

        }

    }
}

