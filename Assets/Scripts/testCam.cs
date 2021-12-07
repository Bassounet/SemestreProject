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
    [SerializeField] Image BlurDialogue;
    [SerializeField] Image Dialogue;
    [SerializeField] Button QuitBtn;
    [SerializeField] Canvas UiCadenas;
    [SerializeField] Text TxtDialogue;
    [SerializeField] GameObject DisplaceItem;
    //VINEK VVVV
    [SerializeField] GameObject TPBIBLI;
    [SerializeField] GameObject TPLABO;

    [Header("Dialogue UI")]
    [SerializeField] Sprite LapeyronieFace;
    [SerializeField] Sprite HippoFace;
    [SerializeField] Sprite Pidouxface;
    [SerializeField] Sprite ChaptalFace;
    [SerializeField] string Dodo;
    [SerializeField] string RecupeLaClefVestiaire;

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
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject Player;


    [Header(" INTERACTABLE ZONE ")]
    public float distanceMaxForGrab;
    public float dragSpeed = 2;
    public bool hasSolvedCadenas;


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
    public bool LapeyronieSpoken; // si lapeyronie nous a parl�     
    public bool HasVestiaire;
    public bool CadenasSolved;
    public bool LapeyronieEnd;
    public bool hasBibli;
    public bool hasBook;
    public bool PidouxGaveClue; // si pidoux nous a d�j� fil� son indice pour la clef 
    public bool PidouxEnd;
    public bool ChaptalEnd;
    public bool ChaptalGaveClue;
    public bool hasLabo;
    public bool hasPotion;

    //bool ButtonBibliisActive = false;
    //bool ButtonLaboisActive = false;


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

        if (Input.touchCount == 0) // zone arr�t raycast
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
            Debug.Log("Shoot");

        }

        if (Input.touchCount > 0)
        {

            GiveTheItem();
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
                    UiDialogue(HippoFace, "Je n'ai plus rien � te dire");

                }

                if (!GoneToLapeyronie && !LapeyronieSpoken)
                {
                    
                    // ici on a pas parl� � hippocrate ni � lapeyronie
                    Debug.Log("BlaBlaBla Va voir Lapyrouze");
                    UiDialogue(HippoFace, "Tu ferai bien d'aller voir lapeyronie");
                    GoneToLapeyronie = true;

                }

               

            }
        }
    }


    // ---------------------------- Talk To HIPPO  ----------------------

    // ---------------------------- GiveItem  ----------------------

    public void GiveTheItem()
    {

        Touch touch = Input.GetTouch(0);
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(touch.position);


        if (Physics.Raycast(ray, out hit))
        {
            if (DisplaceItem.GetComponent<displaceTheItem>().HoldingItem)
            {
                #region Scalpel

                if (DisplaceItem.GetComponent<displaceTheItem>().itemIndex == 1)
                {

                    Debug.Log("Tu le scalpel et tu essayes de le donner");
                    if (hit.transform.gameObject.CompareTag("TableauLapeyronie"))
                    {

                        LapeyronieEnd = true;
                        UiDialogue(LapeyronieFace, "Merci pour le scalpel �a met bien");
                        ClearItem();

                    }

                }

                #endregion

                #region Book

                if (DisplaceItem.GetComponent<displaceTheItem>().itemIndex == 2)
                {
                    
                    Debug.Log("Tu le book et tu essayes de le donner");

                    if (hit.transform.gameObject.CompareTag("TableauPidoux"))
                    {

                        UiDialogue(Pidouxface, "superbe ce bouquin merci ! Faudrait parler � chaptal Mtn ");
                        PidouxEnd = true;                        
                        ClearItem();

                    }


                }

                #endregion

                #region Potion

                if (DisplaceItem.GetComponent<displaceTheItem>().itemIndex == 3)
                {

                    Debug.Log("Tu as la potion et tu essayes de la donner");

                    if (hit.transform.gameObject.CompareTag("TableauChaptal"))
                    {

                        ChaptalEnd = true;
                        UiDialogue(Pidouxface, "Merci pour la potion �a fait plaiz");
                        ClearItem();

                    }
                }

                #endregion
            }
        }
    }

    public void ClearItem()
    {

        DisplaceItem.GetComponent<Image>().sprite = DisplaceItem.GetComponent<displaceTheItem>().SpriteNull;
        DisplaceItem.GetComponent<displaceTheItem>().itemIndex = 0;

    }

    // ---------------------------- GiveItem  ----------------------


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
                
            }
        }
    }

    //  --------------------------- FONCTION DE COLLECTE-------------------

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
                    else { Debug.Log("pas la cl� poiru la bilbi"); holdBib = false; }
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
                    else { Debug.Log("yolopasdecl�dulabo"); holdLab = false; }

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

                       //Debug.Log("Ronpiche");
                       UiDialogue(LapeyronieFace,Dodo);

                    }

                    else
                    {

                        // ici hippocrate nous a dit d'aller voir lapeyronie mais on a pas encore parl� � lapeyronie
                        
                        ClefVestiaire.gameObject.SetActive(true);

                        if (LapeyronieSpoken)
                        {

                            // ici hippocrate nous a dit d'aller voir lapeyronie, et on a d�j� parl� � lapeyronie
                            

                            if ( !AccesVestiaire && LapeyronieSpoken)
                            {

                                // ici hippocrate nous a dit d'aller voir lapeyronie, et on a d�j� parl� � lapeyronie et on a PAS r�cup la clef
                                //Debug.Log("Faudrait ptet r�cup la clef");
                                UiDialogue(LapeyronieFace, "et si tu r�cup�rais la clef? ");

                            }
                            if (AccesVestiaire)
                            {
                                
                                if (HasVestiaire)
                                {
                                    if (CadenasSolved)
                                    {

                                        //Debug.Log("Le cadenas a bien �t� d�verouill�, mais o� est mon scalpel? ");
                                        UiDialogue(LapeyronieFace, "cool pour le cadenas mais mon scalpel ou est il ? ");

                                    }
                                    else
                                    {

                                        // ici hippocrate nous a dit d'aller voir lapeyronie, et on a d�j� parl� � lapeyronie et on a r�cup la clef et on est entr�s dans les vestiaires, et on a pas d�verouill� la cadenas
                                        //Debug.Log("Faut d�verouiller le cadenas mtn");
                                        UiDialogue(LapeyronieFace, " il faudrait peu �tre d�verouiller le cadenas maintenant ? ");

                                    }
                                }
                                else
                                {

                                    // ici hippocrate nous a dit d'aller voir lapeyronie, et on a d�j� parl� � lapeyronie et on a r�cup la clef
                                    //Debug.Log("Tu ferai bien d'aller aux vestiaires");
                                    UiDialogue(LapeyronieFace, " tu devrai aller aux vestiaires ");

                                }
                            }
                        }
                        if (!LapeyronieSpoken)
                        {

                            // ici on a hippocrate nous a dit d'aller voir lapeyronie et nous n'avons pas parl� � lapeyronie
                            LapeyronieSpoken = true;
                            //Debug.Log("BLa Bla Bla voici la premi�re Clef");
                            UiDialogue(LapeyronieFace, "Tu peux aller chercher la premi�re clef !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                        }
                    }
                }
                else
                {

                    //Debug.Log("Ho mon beau scalpel, grave refait wola");
                    UiDialogue(LapeyronieFace, "Merci pour le scalpel, pense � aller voir chaptal, qu'il est b� ! ");

                }

            }

            #endregion

            #region PIDOUX

            if (hit.transform.gameObject.CompareTag("TableauPidoux"))
            {
                //Debug.Log("Vous tentez de parler � pidoux");

                if (LapeyronieEnd)
                {
                    if (PidouxEnd)
                    {

                        // ici on a fini avec pidoux et on lui parle
                        //Debug.Log("Je suis si fier de mon ptit fils");
                        UiDialogue(Pidouxface, "Si fier de mon ptit fils");

                    }
                    else
                    {
                        if (!PidouxGaveClue)
                        {

                            // ici lapeyronie nous a fil� l'indice pour pidoux et on ne lui a pas encore parl�
                            //Debug.Log("Comment va ton ami ? Ok voici ce que tu dois faire ");
                            UiDialogue(Pidouxface, "va chercher la clef pour acc�der � la biblioth�que");
                            PidouxGaveClue = true;
                            ClefBibli.gameObject.SetActive(true);

                        }
                        else
                        {
                            // ici pidoux nous a fil� son indice pour la clef mais on a pas la clef
                            if (AccessBibli)
                            {
                                if (!hasBibli)
                                {

                                    // ici on a la clef mais on a pas �t� � la biblioth�que
                                    //Debug.Log("Faut Aller � la bibli mtn");
                                    UiDialogue(Pidouxface, "faudrait ptet y aller � la bilbioth�que non ?");

                                }
                                else
                                {
                                    if (!hasBook)
                                    {

                                        // ici on a r�cup la clef et on a �t� � la bibli mais on a pas r�cup les bouquins.
                                        //Debug.Log("Faudrait r�cup les bouquins/ regarder");
                                        UiDialogue(Pidouxface, "r�cup�re les bouquins �a devrait aller ?");

                                    }
                                }
                            }
                            else
                            {

                                // ici on a re�u l'indice mais nous ne sommes pas all�s chercher la clef
                                //Debug.Log("Et si t'allais chercher la clef");
                                UiDialogue(Pidouxface, "Faudrait que t'ailles chercher la clef pour la bilbi");

                            }
                        }
                    }
                }
                else
                {

                    //Debug.Log("Azy laissez moi dormir � zeubi");
                    UiDialogue(Pidouxface, "ronpiche");

                }
            }

            #endregion

            #region CHAPTAL

            if (hit.transform.gameObject.CompareTag("TableauChaptal"))
            {

                //Debug.Log(" vous tentez fde parler � CHAPTAL");

                if (LapeyronieEnd)
                {
                    if (ChaptalEnd)
                    {

                        // ici nous parlons � chaptal et la qu�te est finie
                        Debug.Log("Oh quel plaisir cette ptite potion d'amour ! ");
                        UiDialogue(ChaptalFace, "Bien ouej pour la potion, tu vas pouvoir soigner ton ami");

                    }
                    else
                    {

                        if (!ChaptalGaveClue)
                        {

                            // ici nous parlons � chaptal il nous a pas donn� l'indice
                            //Debug.Log("Hello mon ptit pote, voici ton indice pour la prochaine salle");
                            UiDialogue(ChaptalFace, "Salut toi alors ton ami est malade? Faudrait que tu aimmes au labo va chercher la clef");
                            ChaptalGaveClue = true;
                            ClefLabo.gameObject.SetActive(true);

                        }
                        else
                        {

                            // ici on parle � chaptal et il nous a d�j� donn� un indice
                            if (AccessLabo)
                            {

                                if (!hasLabo)
                                {

                                    // ici on la clef mais nous ne sommes pas all�s au labo
                                    //Debug.Log("faudrait ptet aller au labo non ?");
                                    UiDialogue(ChaptalFace, "tu devrai aller au labo now");

                                }
                                else
                                {

                                    // ici nous avons la clef et nous sommes all�s au Labo
                                    if (!hasPotion)
                                    {

                                        // ici on est all�s au labo mais on a pas r�cup la potion 
                                        //Debug.Log("Faudrait ptet r�cup la potion ");
                                        UiDialogue(ChaptalFace, "va r�cup�rer la potion");

                                    }                                  
                                }
                            }
                            else
                            {

                                // ici on a parl� � chaptal mais nous n'avons pas chopp� la clef
                                //Debug.Log("Faudrait ptet que tu r�cup la clef du labo");
                                UiDialogue(ChaptalFace, "tu devrai r�cup�rer la clef du labo");

                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Laisse moi dormir zeubi");
                    UiDialogue(ChaptalFace, "ronpiche");

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
            Debug.Log("le cadenas a �t� r�solu");

            hasSolvedCadenas = true;
        }
        else
        {

            Debug.Log("le cadenas a d�j� �t� r�solu");

        }

        
    }

    // ---------------------------- CADENAS SOLVED ---------------------------

    // ---------------------------- DIALOGUE ---------------------------

    public void UiDialogue(Sprite DialogueFace, string TextDialogue )
    {

        Dialogue.gameObject.SetActive(true);
        Dialogue.sprite = DialogueFace;
        TxtDialogue.text = TextDialogue;
        StartCoroutine("DesAppear");

    }

    IEnumerator DesAppear()
    {

        yield return new WaitForSeconds(3f);
        Dialogue.gameObject.SetActive(false);

    }

    // ---------------------------- DIALOGUE ---------------------------

}







