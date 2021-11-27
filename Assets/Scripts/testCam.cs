using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testCam : MonoBehaviour
{
    public InventoryObject inventory;

    // **************************** ///////////////////// DEV ZONE ////////////////// ************************** //
    

    [Header("UI")]
    [Tooltip(" ** ALL_ZONE ** Rentrez ici les éléments UI")]
    [SerializeField] Image BlurDialogueHippo;
    [SerializeField] Image HippocrateClue;
    [SerializeField] Button QuitBtn;
    [SerializeField] Image ObjectCollected;
    


    [Header("This IsGame Object")]
    [Tooltip(" ** DEV_ZONE ** Rentrez ici les games Objects dont vous avez besoins")]
    [SerializeField] Camera mainCam;

    [Header(" INTERACTABLE ZONE ")]
    [Tooltip(" ** ALL_ZONE ** Ici réglez tous les éléments d'interaction ")]
    public float distanceMaxForGrab;


    public float dragSpeed = 2;


    public bool itsH;
    bool camMoving;
    Vector3 endPosition;
    private Vector3 dragOrigin;

    public bool inMenu; // ce bool permet de figer la caméra A UTILISER AVEC PRUDENCE 
    int shoot = 0; // pour ne tirer qu'un seul raycast


    void Start()
    {

        camMoving = false;
        
    }


    void Update()
    {

        // TEST DE GIVE DU PREMIER ELEMENT 

        

        if (Input.touchCount == 0)
        {

            shoot = 0;

        }

        if (Input.touchCount > 0 && shoot == 0 )
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
        shoot++;
        Touch touch = Input.GetTouch(0);
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(touch.position );
        

        if (Physics.Raycast(ray, out hit) )
        {
                      
            
            if (hit.transform.gameObject.GetComponent<item>() && hit.distance <= distanceMaxForGrab) // pn vérifie que l'objet n'est pas à l'autre bout de la MAP avec une disatnce max de grab
            {
                Debug.Log("It's In ");
                var TargetItemScript = hit.transform.gameObject.GetComponent<item>();
                //inventory.AddItem(TargetItemScript.TheItem, 1); // ancien système d'inventaire                 
                //hit.transform.gameObject.SetActive(false);
                //ObjectCollected.gameObject.SetActive(true);
                //ObjectCollected.sprite = TargetItemScript.TheItem.logo;               

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



}




