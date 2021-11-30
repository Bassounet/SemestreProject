using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class testCam : MonoBehaviour
{


    // **************************** ///////////////////// DEV ZONE ////////////////// ************************** //


    [Header("UI")]
    [Tooltip(" ** ALL_ZONE ** Rentrez ici les éléments UI")]
    [SerializeField] Image BlurDialogueHippo;
    [SerializeField] Image HippocrateDialogue;
    [SerializeField] Button QuitBtn;
    [SerializeField] Image ObjectCollected;
    [SerializeField] Text HippocrateSentence;
    [SerializeField] Button InspectButton;
    [SerializeField] GameObject Player;
    [SerializeField] CinemachineVirtualCamera VirtualCam;

    [SerializeField]
    [Range(0, 1)] float pathCam;

    [Header("This IsGame Object")]
    [Tooltip(" ** DEV_ZONE ** Rentrez ici les games Objects dont vous avez besoins")]
    [SerializeField] Camera mainCam;

    [Header(" INTERACTABLE ZONE ")]
    [Tooltip(" ** ALL_ZONE ** Ici réglez tous les éléments d'interaction ")]
    public float distanceMaxForGrab;


    public float dragSpeed = 2;
    //public CinemachinePathBase pathbase;


    public bool itsH;
    bool camMoving;
    Vector3 endPosition;
    private Vector3 dragOrigin;

    private bool hasTP;

    public bool inMenu; // ce bool permet de figer la caméra A UTILISER AVEC PRUDENCE 
    int shoot = 0; // pour ne tirer qu'un seul raycast

    Transform TargetForTp;


    void Start()
    {

        camMoving = false;
        
    }


    void Update()
    {

        VirtualCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = pathCam;
        //Camera.current.GetComponentInChildren<CinemachineTrackedDolly>().m_PathPosition = pathCam;
        //mainCam.GetComponent<CinemachineTrackedDolly>().m_PathPosition = pathCam;


        // ------------------------------ DEBUG ----------------------- // 

        // ------------------------------ DEBUG ----------------------- // 

        whatIsItAgain();

        if (ObjectCollected.GetComponent<displaceTheItem>().HoldingItem)
        {

            camMoving = false;

        }


        if (Input.touchCount == 0) // zone arrêt raycast
        {

            shoot = 0;

        }

        if (Input.touchCount > 0 && shoot == 0) // fonction lancment de collect quand on appuie 

        {

            Collect();
            ShootTP();

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

        //Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        //Vector3 movex = new Vector3(pos.x * -dragSpeed, 0);
        //Vector3 movey = new Vector3(0,0, pos.y * -dragSpeed);

        if (camMoving && !inMenu)
        {

            //transform.Translate(movex, Space.Self); // passer en world au besoin pour changer le point de ref // .world si effet rail.
            //transform.Translate(movey, Space.Self);

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


    // ---------------------------- FONCTION DE KECECE ?  ----------------------rf


    public void whatIsItAgain()
    {


        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Hippocrate") && ObjectCollected.GetComponent<displaceTheItem>().HoldingItem)
            {
                //camMoving = false;
                ObjectCollected.GetComponent<Image>().sprite = null;
                Debug.Log("La tu gives et c bon enfait ");
                if (ObjectCollected.GetComponent<displaceTheItem>().itemIndex == 0)
                {

                    Debug.Log("ca c'est la popo");
                    HippocrateGiveAClue();

                }

                if (ObjectCollected.GetComponent<displaceTheItem>().itemIndex == 1)
                {

                    Debug.Log("ca c'est la scalpel et tou");
                    HippocrateGiveAClue();

                }

                if (ObjectCollected.GetComponent<displaceTheItem>().itemIndex == 2)
                {

                    Debug.Log("cadenas tu connais");
                    HippocrateGiveAClue();

                }

            }

            if (hit.transform.gameObject.CompareTag("CADENAS"))
            {

                InspectButton.gameObject.SetActive(true);

            }


        }

    }


    // ---------------------------- FONCTION DE KECECE ? FIN  ----------------------

    // ---------------------------- FONCTION APPEL HIPPOCRATE  ----------------------

    public void HippocrateGiveAClue()
    {
        inMenu = true;

        BlurDialogueHippo.gameObject.SetActive(true);
        HippocrateDialogue.gameObject.SetActive(true);
        StartCoroutine("WaitBeforeQuit");


    }

    IEnumerator WaitBeforeQuit()
    {

        yield return new WaitForSeconds(3f);
        QuitBtn.gameObject.SetActive(true);


    }

    // ---------------------------- FONCTION APPEL HIPPOCRATE  ----------------------


    //  --------------------------- Fonction du bouton quit du dialogue -------------------


    public void QuitDialogue()
    {

        BlurDialogueHippo.gameObject.SetActive(false);
        HippocrateDialogue.gameObject.SetActive(false);
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
        Ray ray = mainCam.ScreenPointToRay(touch.position);


        if (Physics.Raycast(ray, out hit))
        {


            if (hit.transform.gameObject.GetComponent<item>() /*&& hit.distance <= distanceMaxForGrab*/) // pn vérifie que l'objet n'est pas à l'autre bout de la MAP avec une disatnce max de grab
            {
                Debug.Log("It's In ");
                var TargetItemScript = hit.transform.gameObject.GetComponent<item>();

                ObjectCollected.sprite = TargetItemScript.picto;
                ObjectCollected.GetComponent<displaceTheItem>().itemIndex = TargetItemScript.itemIndex;
                HippocrateSentence.text = TargetItemScript.SentenceH;


            }
        }
    }

    //  --------------------------- FONCTION DE COLLECTE-------------------


    //  --------------------------- FONCTION DE CADENAS APPEAR-------------------

    public void Cadenas()
    {

        InspectButton.gameObject.SetActive(false);
        StartCoroutine("WaitBeforeCadenas");              

    }

    IEnumerator WaitBeforeCadenas()
    {

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Cadenas");

    }

    //  --------------------------- FONCTION DE CADENAS APPEAR-------------------

    //  --------------------------- FONCTION SHOOT TP -------------------

    public void ShootTP()
    {

        Touch touch = Input.GetTouch(0);
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(touch.position);


        if (Physics.Raycast(ray, out hit))
        {

            if (hit.transform.gameObject.CompareTag("TP") && !hasTP)
            {
                TargetForTp = hit.transform.gameObject.GetComponent<TP>().TargetZone.transform;
                Player.transform.position = TargetForTp.position;

            }

        }

    }

    //  --------------------------- FONCTION SHOOT TP -------------------




}







