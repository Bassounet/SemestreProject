using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testCam : MonoBehaviour
{


    // **************************** ///////////////////// DEV ZONE ////////////////// ************************** //
    

    [Header("UI")]
    [Tooltip(" ** ALL_ZONE ** Rentrez ici les éléments UI")]
    [SerializeField] Image[] slots;
    [SerializeField] Image BlurDialogueHippo;
    [SerializeField] Image HippocrateClue;
    [SerializeField] Button QuitBtn;
    


    [Header("This IsGame Object")]
    [Tooltip(" ** DEV_ZONE ** Rentrez ici les games Objects dont vous avez besoins")]
    [SerializeField] Camera mainCam;


    public float dragSpeed = 2;


    private bool itsH;
    private int indexSlots;
    bool camMoving;
    Vector3 endPosition;
    private Vector3 dragOrigin;

    void Start()
    {

        camMoving = false;
        
    }


    void Update()
    {

        // TEST DE GIVE DU PREMIER ELEMENT 
        if (slots[0].GetComponent<Toggle>().isOn ) // on détecte de voir si le bouton est actif ( donc si on l'a séléctionné ) 
        {
            
            if  (itsH ) // si on sélectionne Hippocrate
            {

                Debug.Log("Tu donnes l'objet à Hippo");
                slots[0].GetComponent<Toggle>().isOn = false;

                HippocrateGiveAClue();


            }

        }


        if (Input.touchCount > 0)
        {

            whatIsIt();

        }


   

        #region ControlCam



        // ------------------------------ // CAM CONTROLLER DE SES MORTS // ------------------------------ // 




        if (Input.GetMouseButtonDown(0))
            {

                camMoving = true;
                dragOrigin = Input.mousePosition;

            }

            if (!camMoving)
            {

                dragOrigin = endPosition;

            }

            if (!Input.GetMouseButton(0)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * -dragSpeed, 0);

            if (camMoving)
            {

                transform.Translate(move, Space.World);


            }



            if (Input.mousePosition != endPosition)
            {
                endPosition = Input.mousePosition;

                camMoving = true;
            }
            else
            {

                camMoving = false;
            }




            // **************************** ///////////////////// DEV ZONE ////////////////// ************************** //


            // ------------------------------ // CAM CONTROLLER DE SES MORTS FIN // ------------------------------ // 




            
        }

    #endregion


    // ---------------------------- FONCTION DE COLLECTE ----------------------

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Collectable"))
            {

                Debug.Log("It's Collectable");
                slots[indexSlots].sprite = other.GetComponent<Image>().sprite;
                other.gameObject.SetActive(false);
                indexSlots++;

            }
        }

    // ---------------------------- FONCTION DE COLLECTE ----------------------

    // ---------------------------- FONCTION DE SHOW OBJECT ----------------------

    public void ShowTheObjectCollected()
    {




    }

    // ---------------------------- FONCTION DE SHOW OBJECT FIN ----------------------


    // ---------------------------- FONCTION DE KECECE ?  ----------------------


    public void whatIsIt() 
    {

        Touch touch = Input.GetTouch(0);
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out hit))
        {

            Debug.DrawRay(transform.position, hit.point);
            //Debug.Log("its : " + hit.transform.gameObject);

            if (hit.transform.gameObject.CompareTag("Hippocrate"))
            {

                itsH = true;

            }
            else
            {

                itsH = false;

            }
            
        }        

    }



    // ---------------------------- FONCTION DE KECECE ? FIN  ----------------------

    // ---------------------------- FONCTION APPEL HIPPOCRATE  ----------------------

    public void HippocrateGiveAClue()
    {

        BlurDialogueHippo.gameObject.SetActive(true);
        HippocrateClue.gameObject.SetActive(true);
        StartCoroutine("WaitBeforeQuit");

    }

    IEnumerator WaitBeforeQuit()
    {

        yield return new WaitForSeconds(3);
        QuitBtn.gameObject.SetActive(true);


    }

    // ---------------------------- FONCTION APPEL HIPPOCRATE  ----------------------

    //  --------------------------- Fontcion du bouton quit du dialogue -------------------


    public void QuitDialogue()
    {

        BlurDialogueHippo.gameObject.SetActive(false);
        HippocrateClue.gameObject.SetActive(false);
        QuitBtn.gameObject.SetActive(false);

    }


    //  --------------------------- Fontcion du bouton quit du dialogue -------------------


    #region InputTouches

    //void Update()
    //{

    //    //Debug.Log("Nombre de touches " + Input.touchCount); // nombres de touches sur écran

    //    //if ( Input.touchCount > 0)
    //    //{
    //    //    Touch touch = Input.GetTouch(0); // on crée une variable de touch

    //    //    //touch.position // donne la position des doigts. 

    //    //    //switch (touch.phase)
    //    //    //{
    //    //    //    case TouchPhase.Began: // la touch phase permet de renvoyer l'état du doigt est ce qu'il bouge est ce qu'il se pose ou est ce qu'il s'en va . 
    //    //    //        Debug.Log("Debut");


    //    //    //}

    //    //    //Debug.Log(Input.GetTouch(0)); // position du doigt 0 return une coordonnée en pixel vector 2 
    //    //    //foreach (Touch touch in Input.touches) // dans le tableau des touches on affecte le fingerId
    //    //    //{

    //    //    //    pos[touch.fingerId] = touch.position; // on fout les coordonnées dans le tableau. 

    //    //    //}


    #endregion


}




