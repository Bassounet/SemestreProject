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
    [SerializeField] Button BoutonToBibli;
    [SerializeField] Button BoutonToLabo;
    [SerializeField] Canvas UiCadenas;
    //VINEK VVVV
    [SerializeField] GameObject TPBIBLI;
    [SerializeField] GameObject TPLABO;



    //FIN VINEK
    [SerializeField] Image ObjectCollected;
    [SerializeField] Text HippocrateSentence;
    [SerializeField] Button InspectButton;
    [SerializeField] Text WhatUGot;
    
    [Header("VIRTUAL_CAM")]
    [SerializeField] CinemachineVirtualCamera VirtualCamHall;
    [SerializeField] CinemachineVirtualCamera VirtualCamPortrait;
    [SerializeField] CinemachineVirtualCamera VirtualCamVestiaire;
    [SerializeField] CinemachineVirtualCamera VirtualCamBibli;
    [SerializeField] CinemachineVirtualCamera VirtualCamLabo;
    [SerializeField] CinemachineVirtualCamera VirtualCamCadenas;
    [SerializeField] GameObject dollyHall;
    [SerializeField] GameObject dollyPortrait;
    [SerializeField] GameObject dollyVestiaire;
    [SerializeField] GameObject dollyBibli;
    [SerializeField] GameObject dollyLabo;
    [SerializeField] GameObject dollyCadenas;

    [Header(" LOOK AT ")]
    [SerializeField] GameObject targetvestiaire;
    [SerializeField] GameObject targetHall;
    [SerializeField] GameObject targetDoorPortrait;
    [SerializeField] GameObject LookAtTarget;
    [SerializeField] GameObject Hippocrate;
    [SerializeField] GameObject TargetPortrait;
    [SerializeField] GameObject TargetPortraitRest;
    [SerializeField] GameObject TargetCadenasRest;
    [SerializeField] GameObject TargetCadenas;

    [Header("This IsGame Object")]
    [Tooltip(" ** DEV_ZONE ** Rentrez ici les games Objects dont vous avez besoins")]
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject Player;


    [Header(" INTERACTABLE ZONE ")]
    [Tooltip(" ** ALL_ZONE ** Ici réglez tous les éléments d'interaction ")]
    public float distanceMaxForGrab;
    public float dragSpeed = 2;
    public bool hasSolvedCadenas;


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
    public bool inCadenas;
    public bool holdBib;
    public bool holdLab;


    [Header("KEY ACCESS")]
    public bool AccesVestiaire;
    public bool AccessBibli;
    public bool AccessLabo;
    [SerializeField] GameObject ClefVestiaire;
    [SerializeField] GameObject ClefBibli;
    [SerializeField] GameObject ClefLabo;
    


    [Header("INTERACT PORTRAIT")]
    public bool GoneToLapeyronie; // hippocrate nous a dit d'aller voir lapeyronie
    public bool LapeyronieSpoken; // si lapeyronie nous a parlé     
    public bool HasVestiaire;
    public bool CadenasSolved;
    public bool LapeyronieEnd;
    public bool hasBibli;
    public bool hasBook;
    public bool PidouxGaveClue; // si pidoux nous a déjà filé son indice pour la clef 
    public bool PidouxEnd;
    public bool ChaptalEnd;
    public bool ChaptalGaveClue;
    public bool hasLabo;
    public bool hasPotion;

    bool ButtonBibliisActive = false;
    bool ButtonLaboisActive = false;


    private CinemachineVirtualCamera actualCam;



    //VINEKTP VAR
    Touch touch;
    RaycastHit hit;
    Ray ray;

    Touch permatouch;
    RaycastHit permahit;
    Ray permaray;
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

        if (inCadenas)
        {

            inVestiaire = false;
            UiCadenas.gameObject.SetActive(true);

        }
        else
        {

            UiCadenas.gameObject.SetActive(false);

        }


        if (ObjectCollected.GetComponent<displaceTheItem>().HoldingItem)
        {

            camMoving = false;

        }

        if (Input.touchCount == 0) // zone arrêt raycast
        {

            shoot = 0;
            ShootTPout();
        }

        if (Input.touchCount > 0 && shoot == 0) // fonction lancment de collect quand on appuie 

        {

            Collect();
            ShootTPin();
            ShootTableaux();
            ShootClefs();
            TalkToHippo();
            ShootCadenas();
            Debug.Log("Shoot");

        }
        if (Input.touchCount > 0)
        {
            permatouch = Input.GetTouch(0);
            permaray = mainCam.ScreenPointToRay(touch.position);
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

                inCadenas = false;
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
            else if (inLabo)
            {

                actualCam = VirtualCamLabo;
                MakePositionCam(VirtualCamLabo, pos);
                DontPathOverTheMax(VirtualCamLabo, dollyLabo);

            }
            //else if (inCadenas)
            //{

            //    actualCam = VirtualCamCadenas;
            //    inVestiaire = false;
            //    MakePositionCam(VirtualCamLabo, pos);
            //    DontPathOverTheMax(VirtualCamLabo, dollyLabo);

            //}

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

                Debug.Log("On essaye de parler à Hippo");

                if (GoneToLapeyronie && !LapeyronieSpoken)
                {

                    // ici on a parlé à hippocrate pour voir lapeyronie mais pas encore à lapeyronie
                    Debug.Log("Je n'ai plus rien à dire");

                }

                if (!GoneToLapeyronie && !LapeyronieSpoken)
                {
                    
                    // ici on a pas parlé à hippocrate ni à lapeyronie
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

    public void ShootCadenas()
    {

        Touch touch = Input.GetTouch(0);
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.transform.gameObject.CompareTag("CADENAS"))
            {

                Debug.Log("C'est le cadenas");


            }
        }
    }

    IEnumerator WaitBeforeCadenas()
    {

        yield return new WaitForSeconds(3f);
        //SceneManager.LoadScene("Cadenas");

    }

    //  --------------------------- FONCTION DE CADENAS APPEAR-------------------

    //  --------------------------- FONCTION SHOOT TP -------------------

    public void ShootTPin()
    {
     
        touch = Input.GetTouch(0);
        
        ray = mainCam.ScreenPointToRay(touch.position);
       

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.transform.gameObject.CompareTag("TP") && hit.transform.GetComponent<detectPorteHallPortrait>().ToBibli)
            {
               
                    holdBib = true;
                Debug.Log("tu appuyes et c'est sur la bibli");

            }

            if (hit.transform.gameObject.CompareTag("TP") && hit.transform.GetComponent<detectPorteHallPortrait>().ToLabo)
            {
                holdLab = true;
                Debug.Log("tu appuyes et c'est sur le labo");
            }

        }

    }
    
    public void ShootTPout()
    {

        if (Physics.Raycast(permaray, out permahit))
        {

            if (permahit.transform.gameObject.CompareTag("TP") && permahit.transform.GetComponent<detectPorteHallPortrait>().ToBibli)
            {
                if (holdBib)
                {
                    if (AccessBibli)
                    {
                        Debug.Log("GoTiLib");
                        TPBIBLI.gameObject.GetComponent<detectPorteHallPortrait>().
                           SendMeToNextWay(
                           TPBIBLI.GetComponent<detectPorteHallPortrait>().ScriptTestCam,
                           TPBIBLI.GetComponent<detectPorteHallPortrait>().CamToEnable,
                           TPBIBLI.GetComponent<detectPorteHallPortrait>().CamToDisable,
                           TPBIBLI.GetComponent<detectPorteHallPortrait>().GoToPortrait);
                        Debug.Log("Go To Bib");
                        TPBIBLI.GetComponent<detectPorteHallPortrait>().ScriptTestCam.gameObject.GetComponent<testCam>().hasBibli = true;
                        TPBIBLI.GetComponent<detectPorteHallPortrait>().ScriptTestCam.gameObject.GetComponent<testCam>().inBibli = true;
                        //ButtonLaboisActive = !ButtonLaboisActive;
                        holdBib = false;
                    }
                    else { Debug.Log("pas la clé poiru la bilbi"); holdBib = false; }
                }
            }

            if (permahit.transform.gameObject.CompareTag("TP") && permahit.transform.GetComponent<detectPorteHallPortrait>().ToLabo)
            {
                if (holdLab)
                {
                    if (AccessLabo)
                    {
                        Debug.Log("Go To Labo");
                        TPLABO.gameObject.GetComponent<detectPorteHallPortrait>().
                           SendMeToNextWay(
                           TPLABO.GetComponent<detectPorteHallPortrait>().ScriptTestCam,
                           TPLABO.GetComponent<detectPorteHallPortrait>().CamToEnable,
                           TPLABO.GetComponent<detectPorteHallPortrait>().CamToDisable,
                           TPLABO.GetComponent<detectPorteHallPortrait>().GoToPortrait);
                        Debug.Log("Go To Lab");
                        TPBIBLI.GetComponent<detectPorteHallPortrait>().ScriptTestCam.gameObject.GetComponent<testCam>().hasLabo = true;
                        TPBIBLI.GetComponent<detectPorteHallPortrait>().ScriptTestCam.gameObject.GetComponent<testCam>().inLabo = true;
                        //ButtonLaboisActive = !ButtonLaboisActive;
                        holdLab = false;
                    }
                    else { Debug.Log("yolopasdeclédulabo"); holdLab = false; }

                }
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
                    ClefVestiaire.SetActive(false);

                }

                if (hit.transform.GetComponent<Clef>().KeyBibli)
                {

                    AccessBibli = true;
                    var textUI = hit.transform.GetComponent<Clef>().WhatUGat;
                    Debug.Log("Its a Key Bibli");
                    WhatUGot.text = textUI;
                    WhatUGot.gameObject.SetActive(true);
                    ClefBibli.SetActive(false);

                }

                if (hit.transform.GetComponent<Clef>().KeyLabo)
                {

                    AccessLabo = true;
                    var textUI = hit.transform.GetComponent<Clef>().WhatUGat;
                    Debug.Log("Its a Key Bibli");
                    WhatUGot.text = textUI;
                    WhatUGot.gameObject.SetActive(true);
                    ClefLabo.SetActive(false);

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

                       Debug.Log("Ronpiche");
                        HippocrateDialogue.gameObject.SetActive(true);

                    }

                    else
                    {

                        // ici hippocrate nous a dit d'aller voir lapeyronie mais on a pas encore parlé à lapeyronie                        
                        ClefVestiaire.gameObject.SetActive(true);

                        if (LapeyronieSpoken)
                        {

                            // ici hippocrate nous a dit d'aller voir lapeyronie, et on a déjà parlé à lapeyronie
                            

                            if ( !AccesVestiaire && LapeyronieSpoken)
                            {

                                // ici hippocrate nous a dit d'aller voir lapeyronie, et on a déjà parlé à lapeyronie et on a PAS récup la clef
                                Debug.Log("Faudrait ptet récup la clef");

                            }
                            if (AccesVestiaire)
                            {
                                
                                if (HasVestiaire)
                                {
                                    if (CadenasSolved)
                                    {

                                        Debug.Log("Le cadenas a bien été déverouillé, voilà le prochain indice");
                                        LapeyronieEnd = true;

                                    }
                                    else
                                    {

                                        // ici hippocrate nous a dit d'aller voir lapeyronie, et on a déjà parlé à lapeyronie et on a récup la clef et on est entrés dans les vestiaires, et on a pas déverouillé la cadenas
                                        Debug.Log("Faut déverouiller le cadenas mtn");

                                    }
                                }
                                else
                                {

                                    // ici hippocrate nous a dit d'aller voir lapeyronie, et on a déjà parlé à lapeyronie et on a récup la clef
                                    Debug.Log("Tu ferai bien d'aller aux vestiaires");

                                }
                            }
                        }
                        if (!LapeyronieSpoken)
                        {

                            // ici on a hippocrate nous a dit d'aller voir lapeyronie et nous n'avons pas parlé à lapeyronie
                            LapeyronieSpoken = true;
                            Debug.Log("BLa Bla Bla voici la première Clef");

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
                Debug.Log("Vous tentez de parler à pidoux");

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

                            // ici lapeyronie nous a filé l'indice pour pidoux et on ne lui a pas encore parlé
                            Debug.Log("Comment va ton ami ? Ok voici ce que tu dois faire ");
                            PidouxGaveClue = true;
                            ClefBibli.gameObject.SetActive(true);

                        }
                        else
                        {
                            // ici pidoux nous a filé son indice pour la clef mais on a pas la clef
                            if (AccessBibli)
                            {
                                if (!hasBibli)
                                {

                                    // ici on a la clef mais on a pas été à la bibliothèque
                                    Debug.Log("Faut Aller à la bibli mtn");

                                }
                                else
                                {
                                    if (!hasBook)
                                    {

                                        // ici on a récup la clef et on a été à la bibli mais on a pas récup les bouquins.
                                        Debug.Log("Faudrait récup les bouquins/ regarder");

                                    }
                                    else
                                    {

                                        // ici on a récup le bouquin 
                                        Debug.Log("Merci pour le book voici le prochain indice");
                                        PidouxEnd = true;

                                    }


                                }

                            }
                            else
                            {

                                // ici on a reçu l'indice mais nous ne sommes pas allés chercher la clef
                                Debug.Log("Et si t'allais chercher la clef");

                            }
                        }
                    }
                }
                else
                {

                    Debug.Log("Azy laissez moi dormir à zeubi");

                }
            }

            #endregion

            #region CHAPTAL

            if (hit.transform.gameObject.CompareTag("TableauChaptal"))
            {

                Debug.Log(" vous tentez fde parler à CHAPTAL");

                if (LapeyronieEnd)
                {
                    if (ChaptalEnd)
                    {

                        // ici nous parlons à chaptal et la quête est finie
                        Debug.Log("Oh quel plaisir cette ptite potion d'amour ! ");

                    }
                    else
                    {

                        if (!ChaptalGaveClue)
                        {

                            // ici nous parlons à chaptal il nous a pas donné l'indice
                            Debug.Log("Hello mon ptit pote, voici ton indice pour la prochaine salle");
                            ChaptalGaveClue = true;
                            ClefLabo.gameObject.SetActive(true);

                        }
                        else
                        {

                            // ici on parle à chaptal et il nous a déjà donné un indice
                            if (AccessLabo)
                            {

                                if (!hasLabo)
                                {

                                    // ici on la clef mais nous ne sommes pas allés au labo
                                    Debug.Log("faudrait ptet aller au labo non ?");

                                }
                                else
                                {

                                    // ici nous avons la clef et nous sommes allés au Labo
                                    if (hasPotion)
                                    {

                                        // ici on est allés au labo et nous avons la fiole/ potion 
                                        Debug.Log("Tiens la potion file là à ton frerot");
                                        ChaptalEnd = true;

                                    }
                                    else
                                    {

                                        // ici on est allés au labo mais on a pas récup la potion 
                                        Debug.Log("Faudrait ptet récup la potion ");

                                    }
                                }
                            }
                            else
                            {

                                // ici on a parlé à chaptal mais nous n'avons pas choppé la clef
                                Debug.Log("Faudrait ptet que tu récup la clef du labo");

                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Laisse moi dormir zeubi");

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

    // ---------------------------- BACK FROM CADENAS ---------------------------

    public void backFromCadenas()
    {

        VirtualCamCadenas.gameObject.SetActive(false);
        VirtualCamVestiaire.gameObject.SetActive(true);
        inVestiaire = true;
        inCadenas = false;
        inHall = false;
        UiCadenas.gameObject.SetActive(false);

    }

    // ---------------------------- BACK FROM CADENAS ---------------------------

    // ---------------------------- CADENAS SOLVED ---------------------------

    public void CadenasSolevd()
    {
        if (!hasSolvedCadenas)
        {
            Debug.Log("le cadenas a été résolu");

            hasSolvedCadenas = true;
        }
        else
        {

            Debug.Log("le cadenas a déjà été résolu");

        }

        
    }

    // ---------------------------- CADENAS SOLVED ---------------------------

}







