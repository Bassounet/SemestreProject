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
    [SerializeField] CinemachineVirtualCamera VirtualCamHall;
    [SerializeField] CinemachineVirtualCamera VirtualCamPortrait;
    [SerializeField] CinemachineVirtualCamera VirtualCamVestiaire;
    [SerializeField] CinemachineVirtualCamera VirtualCamBibli;
    [SerializeField] GameObject dollyHall;
    [SerializeField] GameObject dollyPortrait;
    [SerializeField] GameObject dollyVestiaire;


    [SerializeField]
    [Range(0, 10)] public float pathCam;

    [Header("This IsGame Object")]
    [Tooltip(" ** DEV_ZONE ** Rentrez ici les games Objects dont vous avez besoins")]
    [SerializeField] Camera mainCam;

    [Header(" INTERACTABLE ZONE ")]
    [Tooltip(" ** ALL_ZONE ** Ici réglez tous les éléments d'interaction ")]
    public float distanceMaxForGrab;

    [Header(" LOOK AT ")]
    [SerializeField] GameObject targetvestiaire;
    [SerializeField] GameObject targetHall;
    [SerializeField] GameObject targetDoorPortrait;
    [SerializeField] GameObject LookAtTarget;
    [SerializeField] GameObject Hippocrate;
    [SerializeField] GameObject TargetPortrait;
    [SerializeField] GameObject TargetPortraitRest;






    public float dragSpeed = 2;
   

    public bool itsH;
    bool camMoving;
    Vector3 endPosition;
    private Vector3 dragOrigin;

    private bool hasTP;

    public bool inMenu; // ce bool permet de figer la caméra A UTILISER AVEC PRUDENCE 
    int shoot = 0; // pour ne tirer qu'un seul raycast

    Transform TargetForTp;

    [Header("POSITION > SPACEWORLD")]
    public bool inHall ;
    public bool inLabo ;
    public bool inVestiaire;
    public bool inPortrait ;
    public bool inBibli;

    private CinemachineVirtualCamera actualCam;

    void Start()
    {

        camMoving = false;
        inHall = true;
        
    }


    void Update()
    {


        // ------------------------------ DEBUG ----------------------- // 

        // ------------------------------ DEBUG ----------------------- // 


        if (inHall)
        {

            actualCam = VirtualCamHall;

        }

        if (inPortrait)
        {

            actualCam = VirtualCamPortrait;

        }

        if (inVestiaire)
        {

            actualCam = VirtualCamVestiaire;

        }

        if (inBibli)
        {

            actualCam = VirtualCamBibli;

        }


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
        Vector3 pos = mainCam.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        //Vector3 movex = new Vector3(pos.x * -dragSpeed, 0);

        ////Vector3 movey = new Vector3(0,0, pos.y * -dragSpeed);

        if (camMoving && !inMenu)
        {
            if (inHall)
            {

                MakePositionCam(VirtualCamHall, pos);
                DontPathOverTheMax(VirtualCamHall, dollyHall);

            }
            else if (inPortrait)
            {

                MakePositionCam(VirtualCamPortrait, pos);
                DontPathOverTheMax(VirtualCamPortrait, dollyPortrait);

            }
            else if (inVestiaire)
            {

                MakePositionCam(VirtualCamVestiaire, pos);
                DontPathOverTheMax(VirtualCamVestiaire, dollyVestiaire);

            }

            //transform.Translate(movex, Space.Self); // passer en world au besoin pour changer le point de ref // .world si effet rail.
            //transform.Translate(movey, Space.Self);
            

            //float positionsurlerail = currentPos / maxPos;
            //Debug.Log(positionsurlerail);
            //if (positionsurlerail<0.5f) LookAtTarget.transform.position = Vector3.Lerp(targetvestiaire.transform.position, targetHall.transform.position, positionsurlerail * 2f);
            //else LookAtTarget.transform.position = Vector3.Lerp(targetHall.transform.position, targetDoorPortrait.transform.position, (positionsurlerail-0.5f)*2f);

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

    // ---------------------------- LOOK AT ------------------------

    private void OnTriggerEnter(Collider other)
    {

        // ------------------- TARGET HALL ---------------------- //

        if (other.gameObject.CompareTag("PorteVestiaire"))
        {
            Debug.Log("its vestiaire");
            actualCam.LookAt.SetPositionAndRotation(targetvestiaire.transform.position, Quaternion.identity);

        }

        if (other.gameObject.CompareTag("PortePortrait"))
        {

            Debug.Log("its portrait");
            actualCam.LookAt.SetPositionAndRotation(targetDoorPortrait.transform.position, Quaternion.identity);

        }

        // ------------------- TARGET HALL ---------------------- //

        // ------------------- TARGET PORTRAIT ---------------------- //

        if (other.gameObject.CompareTag("Hippocrate"))
        {

            actualCam.LookAt.SetPositionAndRotation(Hippocrate.transform.position, Quaternion.identity);

        }

        // ------------------- TARGET PORTRAIT ---------------------- //
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PorteVestiaire") || other.gameObject.CompareTag("PortePortrait"))
        {

            actualCam.LookAt.SetPositionAndRotation(targetHall.transform.position, Quaternion.identity);

        }

        if (other.gameObject.CompareTag("Hippocrate") )
        {
            Debug.Log("We lache hippo");
            actualCam.LookAt.SetPositionAndRotation(TargetPortraitRest.transform.position, Quaternion.identity);

        }
    }


    // ---------------------------- LOOK AT ------------------------

    // ---------------------------- DON'T Pass over the max Path -------------------------

    float currentPos;
    float maxPos; 
    public void DontPathOverTheMax(CinemachineVirtualCamera VirtualCam, GameObject DollyCam)
    {
        maxPos = DollyCam.GetComponent<CinemachineSmoothPath>().MaxPos;
        currentPos = VirtualCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition;

        VirtualCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = Mathf.Clamp(currentPos,0, maxPos);

    }


    // ---------------------------- DON'T Pass over the max Path -------------------------

    // ---------------------------- MOVE THE CAM -------------------------

    public void MakePositionCam(CinemachineVirtualCamera VirtualCam, Vector3 pos)
    {

        VirtualCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += pos.x * -dragSpeed;

    }

    // ---------------------------- MOVE THE CAM -------------------------






}







