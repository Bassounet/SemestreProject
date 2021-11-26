using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testCam : MonoBehaviour
{
    public InventoryObject inventory;

    // **************************** ///////////////////// DEV ZONE ////////////////// ************************** //
    

    [Header("UI")]
    [Tooltip(" ** ALL_ZONE ** Rentrez ici les �l�ments UI")]
    [SerializeField] Image[] slots;
    [SerializeField] Image BlurDialogueHippo;
    [SerializeField] Image HippocrateClue;
    [SerializeField] Button QuitBtn;
    [SerializeField] Image ObjectCollected;
    


    [Header("This IsGame Object")]
    [Tooltip(" ** DEV_ZONE ** Rentrez ici les games Objects dont vous avez besoins")]
    [SerializeField] Camera mainCam;

    [Header(" INTERACTABLE ZONE ")]
    [Tooltip(" ** ALL_ZONE ** Ici r�glez tous les �l�ments d'interaction ")]
    public float distanceMaxForGrab;


    public float dragSpeed = 2;


    private bool itsH;
    bool camMoving;
    Vector3 endPosition;
    private Vector3 dragOrigin;

    private bool inMenu; // ce bool permet de figer la cam�ra A UTILISER AVEC PRUDENCE 
    //int shoot = 0; // pour ne tirer qu'un seul raycast


    void Start()
    {

        camMoving = false;
        
    }


    void Update()
    {
        
        // TEST DE GIVE DU PREMIER ELEMENT 

        if (slots[0].GetComponent<Toggle>().isOn ) // on d�tecte de voir si le bouton est actif ( donc si on l'a s�l�ctionn� ) 
        {
            
            if  (itsH ) // si on s�lectionne Hippocrate
            {

                Debug.Log("Tu donnes l'objet � Hippo");
                slots[0].GetComponent<Toggle>().isOn = false;

                HippocrateGiveAClue();


            }

        }


        if (Input.touchCount > 0  )
        {
            
            whatIsIt();
            Collect();
            
            
        }


   

        #region ControlCam



        // ------------------------------ // CAM CONTROLLER DE SES MORTS // ------------------------------ // 




        if ( Input.GetMouseButtonDown(0) )
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

            if (camMoving && !inMenu)
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
        inMenu = true;

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

    //  --------------------------- Fonction du bouton quit du dialogue -------------------


    public void QuitDialogue()
    {

        BlurDialogueHippo.gameObject.SetActive(false);
        HippocrateClue.gameObject.SetActive(false);
        QuitBtn.gameObject.SetActive(false);
        inMenu = false;

    }


    //  --------------------------- Fontcion du bouton quit du dialogue -------------------


    //  --------------------------- FONCTION DE COLLECTE-------------------

    public void Collect()
    {
        Touch touch = Input.GetTouch(0);
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(touch.position );
        

        if (Physics.Raycast(ray, out hit) )
        {
            
            Debug.DrawRay(transform.position, hit.point, Color.red);
            
            if (hit.transform.gameObject.GetComponent<item>() /*&& hit.distance <= distanceMaxForGrab*/) // pn v�rifie que l'objet n'est pas � l'autre bout de la MAP avec une disatnce max de grab
            {
                Debug.Log("It's In ");
                var TargetItemScript = hit.transform.gameObject.GetComponent<item>();
                inventory.AddItem(TargetItemScript.TheItem, 1);
                hit.transform.gameObject.SetActive(false);
                ObjectCollected.gameObject.SetActive(true);
                ObjectCollected.sprite = TargetItemScript.TheItem.logo;
                //slots[indexSlots].sprite = ObjectToCollect.GetComponent<Image>().sprite;
                //ObjectToCollect.gameObject.SetActive(false);
                //indexSlots++;

            }

            
        }
    }

    //  --------------------------- FONCTION DE COLLECTE-------------------


    //  --------------------------- FONCTION DE CLEAR ON APPLICATION QUIT -------------------

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }


    //  --------------------------- FONCTION DE CLEAR ON APPLICATION QUIT -------------------




    #region InputTouches

    //void Update()
    //{

    //    //Debug.Log("Nombre de touches " + Input.touchCount); // nombres de touches sur �cran

    //    //if ( Input.touchCount > 0)
    //    //{
    //    //    Touch touch = Input.GetTouch(0); // on cr�e une variable de touch

    //    //    //touch.position // donne la position des doigts. 

    //    //    //switch (touch.phase)
    //    //    //{
    //    //    //    case TouchPhase.Began: // la touch phase permet de renvoyer l'�tat du doigt est ce qu'il bouge est ce qu'il se pose ou est ce qu'il s'en va . 
    //    //    //        Debug.Log("Debut");


    //    //    //}

    //    //    //Debug.Log(Input.GetTouch(0)); // position du doigt 0 return une coordonn�e en pixel vector 2 
    //    //    //foreach (Touch touch in Input.touches) // dans le tableau des touches on affecte le fingerId
    //    //    //{

    //    //    //    pos[touch.fingerId] = touch.position; // on fout les coordonn�es dans le tableau. 

    //    //    //}


    #endregion


}




