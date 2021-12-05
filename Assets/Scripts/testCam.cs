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
    [Tooltip(" ** ALL_ZONE ** Rentrez ici les �l�ments UI")]
    [SerializeField] Image BlurDialogueHippo;
    [SerializeField] Image HippocrateDialogue;
    [SerializeField] Button QuitBtn;
    [SerializeField] Button BoutonToBibli;
    [SerializeField] Image ObjectCollected;
    [SerializeField] Text HippocrateSentence;
    [SerializeField] Button InspectButton;
    [SerializeField] Text WhatUGot;
    
    [Header("VIRTUAL_CAM")]
    [SerializeField] CinemachineVirtualCamera VirtualCamHall;
    [SerializeField] CinemachineVirtualCamera VirtualCamPortrait;
    [SerializeField] CinemachineVirtualCamera VirtualCamVestiaire;
    [SerializeField] CinemachineVirtualCamera VirtualCamBibli;
    [SerializeField] GameObject dollyHall;
    [SerializeField] GameObject dollyPortrait;
    [SerializeField] GameObject dollyVestiaire;
    [SerializeField] GameObject dollyBibli;

    [Header(" LOOK AT ")]
    [SerializeField] GameObject targetvestiaire;
    [SerializeField] GameObject targetHall;
    [SerializeField] GameObject targetDoorPortrait;
    [SerializeField] GameObject LookAtTarget;
    [SerializeField] GameObject Hippocrate;
    [SerializeField] GameObject TargetPortrait;
    [SerializeField] GameObject TargetPortraitRest;

    [Header("This IsGame Object")]
    [Tooltip(" ** DEV_ZONE ** Rentrez ici les games Objects dont vous avez besoins")]
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject Player;


    [Header(" INTERACTABLE ZONE ")]
    [Tooltip(" ** ALL_ZONE ** Ici r�glez tous les �l�ments d'interaction ")]
    public float distanceMaxForGrab;
    public float dragSpeed = 2;
   

    public bool itsH;
    bool camMoving;
    Vector3 endPosition;
    private Vector3 dragOrigin;

    private bool hasTP;

    public bool inMenu; // ce bool permet de figer la cam�ra A UTILISER AVEC PRUDENCE 
    int shoot = 0; // pour ne tirer qu'un seul raycast

    Transform TargetForTp;

    [Header("POSITION > SPACEWORLD")]
    public bool inHall ;
    public bool inLabo ;
    public bool inVestiaire;
    public bool inPortrait ;
    public bool inBibli;


    [Header("KEY ACCESS")]
    public bool AccesVestiaire;
    public bool AccessBibli;
    public bool AccessLabo;


    [Header("INTERACT PORTRAIT")]
    public bool GoneToLapeyronie; // hippocrate nous a dit d'aller voir lapeyronie
    public bool LapeyronieSpoken; // si lapeyronie nous a parl�     
    public bool HasVestiaire;
    public bool CadenasSolved;
    public bool LapeyronieEnd;
    public bool hasBibli;
    public bool hasBook;
    public bool PidouxGaveClue; // si pidoux nous a d�j� fil� son indice pour la clef 
    public bool PidouxEnd;


    private CinemachineVirtualCamera actualCam;

    void Start()
    {

        camMoving = false;
        inHall = true;
        GoneToLapeyronie = false;
        
    }


    void Update()
    {

        // ------------------------------ DEBUG ----------------------- // 

        // ------------------------------ DEBUG ----------------------- // 

        GiveToHippocrateTheClue();


        if (ObjectCollected.GetComponent<displaceTheItem>().HoldingItem)
        {

            camMoving = false;

        }

        if (Input.touchCount == 0) // zone arr�t raycast
        {

            shoot = 0;

        }

        if (Input.touchCount > 0 && shoot == 0) // fonction lancment de collect quand on appuie 

        {

            Collect();
            ShootTP();
            ShootTableaux();
            ShootClefs();
            TalkToHippo();
            Debug.Log("Shoot");

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
        
        Vector3 pos = mainCam.ScreenToViewportPoint(Input.mousePosition - dragOrigin);


        #region Where Are We ?
        

        if (camMoving && !inMenu)
        {

            if (inHall)
            {

                actualCam = VirtualCamHall;
                MakePositionCam(VirtualCamHall, pos);
                DontPathOverTheMax(VirtualCamHall, dollyHall);                

            }
            else if (inPortrait)
            {

                actualCam = VirtualCamPortrait;
                MakePositionCam(VirtualCamPortrait, pos);
                DontPathOverTheMax(VirtualCamPortrait, dollyPortrait);                

            }
            else if (inVestiaire)
            {

                actualCam = VirtualCamVestiaire;
                MakePositionCam(VirtualCamVestiaire, pos);
                DontPathOverTheMax(VirtualCamVestiaire, dollyVestiaire);                

            }

            else if (inBibli)
            {

                actualCam = VirtualCamBibli;
                MakePositionCam(VirtualCamBibli, pos);
                DontPathOverTheMax(VirtualCamBibli, dollyBibli);                

            }            

        }

        #endregion

        if (Input.mousePosition != endPosition)
        {
            endPosition = Input.mousePosition;

            camMoving = true;
        }
        else
        {

            camMoving = false;
        }

        // ----- 

        //transform.Translate(movex, Space.Self); // passer en world au besoin pour changer le point de ref // .world si effet rail.
        //transform.Translate(movey, Space.Self);

        //Vector3 movex = new Vector3(pos.x * -dragSpeed, 0);
        ////Vector3 movey = new Vector3(0,0, pos.y * -dragSpeed);
        /////Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);


        //float positionsurlerail = currentPos / maxPos;
        //Debug.Log(positionsurlerail);
        //if (positionsurlerail<0.5f) LookAtTarget.transform.position = Vector3.Lerp(targetvestiaire.transform.position, targetHall.transform.position, positionsurlerail * 2f);
        //else LookAtTarget.transform.position = Vector3.Lerp(targetHall.transform.position, targetDoorPortrait.transform.position, (positionsurlerail-0.5f)*2f);

        // ----- 
    }

    #endregion

    // ---------------------------- FONCTION DE SHOW OBJECT ----------------------

    public void ShowTheObjectCollected()
    {




    }

    // ---------------------------- FONCTION DE SHOW OBJECT FIN ----------------------


    // ---------------------------- FONCTION DE KECECE ?  ----------------------rf


    public void GiveToHippocrateTheClue()
    {


        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Hippocrate") && ObjectCollected.GetComponent<displaceTheItem>().HoldingItem)
            {
                
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

    // ---------------------------- Talk To HIPPO  ----------------------

    public void TalkToHippo()
    {

        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Hippocrate"))
            {

                Debug.Log("On essaye de parler � Hippo");

                if (GoneToLapeyronie && !LapeyronieSpoken)
                {

                    // ici on a parl� � hippocrate pour voir lapeyronie mais pas encore � lapeyronie
                    Debug.Log("Je n'ai plus rien � dire");

                }

                if (!GoneToLapeyronie && !LapeyronieSpoken)
                {
                    
                    // ici on a pas parl� � hippocrate ni � lapeyronie
                    Debug.Log("BlaBlaBla Va voir Lapyrouze");
                    GoneToLapeyronie = true;

                }

               

            }
        }
    }


    // ---------------------------- Talk To HIPPO  ----------------------

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


            if (hit.transform.gameObject.GetComponent<item>() /*&& hit.distance <= distanceMaxForGrab*/) // pn v�rifie que l'objet n'est pas � l'autre bout de la MAP avec une disatnce max de grab
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

            if (hit.transform.gameObject.CompareTag("TP") )
            {

                Debug.Log("GoTiLib");
                BoutonToBibli.gameObject.SetActive(true);
                
            }

        }

    }

    //  --------------------------- FONCTION SHOOT TP -------------------

    //  --------------------------- FONCTION SHOOT CLEF -------------------

    public void ShootClefs()
    {

        Touch touch = Input.GetTouch(0);
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(touch.position);        

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("CLEF"))
            {
                if (hit.transform.GetComponent<Clef>().KeyVestaire)
                {

                    AccesVestiaire = true;
                    var textUI = hit.transform.GetComponent<Clef>().WhatUGat;
                    Debug.Log("Its a Key Vestiaire");
                    WhatUGot.text = textUI;
                    WhatUGot.gameObject.SetActive(true);

                }

                if (hit.transform.GetComponent<Clef>().KeyBibli)
                {

                    AccessBibli = true;
                    var textUI = hit.transform.GetComponent<Clef>().WhatUGat;
                    Debug.Log("Its a Key Bibli");
                    WhatUGot.text = textUI;
                    WhatUGot.gameObject.SetActive(true);

                }

                if (hit.transform.GetComponent<Clef>().KeyLabo)
                {

                    AccessLabo = true;
                    var textUI = hit.transform.GetComponent<Clef>().WhatUGat;
                    Debug.Log("Its a Key Bibli");
                    WhatUGot.text = textUI;
                    WhatUGot.gameObject.SetActive(true);

                }
            }
        }
    }

    //  --------------------------- FONCTION SHOOT CLEF FIN -------------------

    //  --------------------------- FONCTION SHOOT Tableaux -------------------

    public void ShootTableaux()
    {

        Touch touch = Input.GetTouch(0);
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out hit))
        {
            #region LAPEYRONIE 
            if (hit.transform.gameObject.CompareTag("TableauLapeyronie"))
            {
                if (!LapeyronieEnd)
                {

                    if (!GoneToLapeyronie)
                    {

                       Debug.Log("Va parler � Hippo");

                    }

                    else
                    {

                        // ici hippocrate nous a dit d'aller voir lapeyronie mais on a pas encore parl� � lapeyronie                        

                        if (LapeyronieSpoken)
                        {

                            // ici hippocrate nous a dit d'aller voir lapeyronie, et on a d�j� parl� � lapeyronie
                            if ( !AccesVestiaire && LapeyronieSpoken)
                            {

                                // ici hippocrate nous a dit d'aller voir lapeyronie, et on a d�j� parl� � lapeyronie et on a PAS r�cup la clef
                                Debug.Log("Faudrait ptet r�cup la clef");

                            }
                            if (AccesVestiaire)
                            {

                                // ici hippocrate nous a dit d'aller voir lapeyronie, et on a d�j� parl� � lapeyronie et on a r�cup la clef
                                Debug.Log("Tu ferai bien d'aller aux vestiaires");
                                if (HasVestiaire)
                                {

                                    // ici hippocrate nous a dit d'aller voir lapeyronie, et on a d�j� parl� � lapeyronie et on a r�cup la clef et on est entr�s dans les vestiaires

                                    if (CadenasSolved)
                                    {

                                        Debug.Log("Le cadenas a bien �t� d�verouill�, voil� le prochain indice");
                                        LapeyronieEnd = true;

                                    }
                                    else
                                    {

                                        // ici hippocrate nous a dit d'aller voir lapeyronie, et on a d�j� parl� � lapeyronie et on a r�cup la clef et on est entr�s dans les vestiaires, et on a pas d�verouill� la cadenas
                                        Debug.Log("Faut d�verouiller le cadenas mtn");

                                    }


                                }


                            }                            

                        }

                        if (!LapeyronieSpoken)
                        {

                            // ici on a hippocrate nous a dit d'aller voir lapeyronie et nous n'avons pas parl� � lapeyronie
                            LapeyronieSpoken = true;
                            Debug.Log("BLa Bla Bla voici la premi�re Clef");


                        }

                    }
                }
                else
                {
                    
                    Debug.Log("Ho mon beau scalpel, grave refait wola");

                }

            }

            #endregion

            #region PIDOUX

            if (hit.transform.gameObject.CompareTag("TableauPidoux"))
            {
                Debug.Log("Vous tentez de parler � pidoux");

                if (LapeyronieEnd)
                {
                    if (PidouxEnd)
                    {

                        // ici on a fini avec pidoux et on lui parle
                        Debug.Log("Je suis si fier de mon ptit fils");

                    }
                    else
                    {


                        if (!PidouxGaveClue)
                        {

                            // ici lapeyronie nous a fil� l'indice pour pidoux et on ne lui a pas encore parl�
                            Debug.Log("Comment va ton ami ? Ok voici ce que tu dois faire ");
                            PidouxGaveClue = true;
                        }
                        else
                        {
                            // ici pidoux nous a fil� son indice pour la clef mais on a pas la clef
                            if (AccessBibli)
                            {
                                if (!hasBibli)
                                {

                                    // ici on a la clef mais on a pas �t� � la biblioth�que
                                    Debug.Log("Faut Aller � la bibli mtn");

                                }
                                else
                                {
                                    if (!hasBook)
                                    {

                                        // ici on a r�cup la clef et on a �t� � la bibli mais on a pas r�cup les bouquins.
                                        Debug.Log("Faudrait r�cup les bouquins/ regarder");

                                    }
                                    else
                                    {

                                        // ici on a r�cup le bouquin 
                                        Debug.Log("Merci pour le book voici le prochain indice");
                                        PidouxEnd = true;

                                    }


                                }

                            }
                            else
                            {

                                // ici on a re�u l'indice mais nous ne sommes pas all�s chercher la clef
                                Debug.Log("Et si t'allais chercher la clef");

                            }

                        }
                    }                 

                }
                else
                {

                    Debug.Log("Azy laissez moi dormir � zeubi");

                }

            }

            #endregion

        }

    }

    //  --------------------------- FONCTION SHOOT Tableaux -------------------

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
            Debug.Log("It's Hippo");
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







