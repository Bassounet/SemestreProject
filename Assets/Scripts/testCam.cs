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
    [SerializeField] Canvas UiCadenas;
    [SerializeField] Text TxtDialogue;
    [SerializeField] GameObject DisplaceItem;
    [SerializeField] GameObject TalkieWalkieBtn;
    [SerializeField] GameObject Registre;
    [SerializeField] GameObject TheBlur;
    [SerializeField] GameObject BtnSkip;
    [SerializeField] GameObject facingDialogue;
    [SerializeField] GameObject GuyNam;
    [SerializeField] GameObject RectScroll;



    //VINEK VVVV

    [SerializeField] GameObject TPBIBLI;
    [SerializeField] GameObject TPLABO;
    [SerializeField] GameObject BlackScreen;

    [SerializeField] GameObject flecheg;
    [SerializeField] GameObject fleched;

    [SerializeField] GameObject Cle;


    //FIN VINEK

    [Header("Dialogue UI")]
    [SerializeField] Sprite LapeyronieFace;
    [SerializeField] Sprite HippoFace;
    [SerializeField] Sprite Pidouxface;
    [SerializeField] Sprite ChaptalFace;
    [SerializeField] Sprite SameFace;
    [SerializeField] Sprite NamePidoux;
    [SerializeField] Sprite NameChaptal;
    [SerializeField] Sprite NameHippocrate;
    [SerializeField] Sprite NameLapeyronie;
    [SerializeField] Button TalkieDialogue;

    [SerializeField] Image ObjectCollected;
    [SerializeField] Text HippocrateSentence;
    [SerializeField] Text WhatUGot;


    [Header("Bibli")]
    [SerializeField] GameObject Livre;
    [SerializeField] Sprite Genealogie;
    [SerializeField] Sprite Fable;
    [SerializeField] Sprite Random;

    [Header("VIRTUAL_CAM")]
    [SerializeField] CinemachineVirtualCamera VirtualCamHall;
    [SerializeField] CinemachineVirtualCamera VirtualCamPortrait;
    [SerializeField] CinemachineVirtualCamera VirtualCamVestiaire;
    [SerializeField] CinemachineVirtualCamera VirtualCamBibli;
    [SerializeField] CinemachineVirtualCamera VirtualCamLabo;
    [SerializeField] CinemachineVirtualCamera VirtualCamCadenas;
    [SerializeField] CinemachineVirtualCamera VirtualCamTalkie;
    [SerializeField] CinemachineVirtualCamera VirtualCamSlideP;
    [SerializeField] GameObject dollyHall;
    [SerializeField] GameObject dollyPortrait;
    [SerializeField] GameObject dollyVestiaire;
    [SerializeField] GameObject dollyBibli;
    [SerializeField] GameObject dollyLabo;
    [SerializeField] GameObject dollyCadenas;
    [SerializeField] GameObject dollyTalkie;
    [SerializeField] GameObject dollySlideP;

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
    [SerializeField] GameObject TargetTalkie;
    [SerializeField] GameObject TargetTalkierest;
    [SerializeField] GameObject Coffr;

    [Header("This IsGame Object")]
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject DoorClosed;
    [SerializeField] GameObject DoorOpen;
    [SerializeField] GameObject WalkieTalkie;
    [SerializeField] GameObject BibliToFall;
    [SerializeField] GameObject BibliFalled;



    [Header(" INTERACTABLE ZONE ")]
    public float distanceMaxForGrab;
    public float dragSpeed = -2;
    public float dragSpeedInvert = 2;
    public bool hasSolvedCadenas;
    [Range(0, 10)] public float shakingForce;
    [Range(0, 10)] public float TimeToShake;
    [SerializeField] GameObject Scalpel;



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
    public bool inTalkie;
    public bool inVestiaire;
    public bool inPortrait ;
    public bool inBibli;
    public bool inCadenas;
    public bool inSlideP;
    public bool holdVes;
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
    public bool RdyForLastDIalogue;
    public bool LapeyronieEnd;
    public bool hasBibli;
    public bool hasBook;
    public bool PidouxGaveClue;// si pidoux nous a déjà filé son indice pour la clef 
    public bool RdyForLastDialoguePid;
    public bool PidouxEnd;
    public bool ChaptalEnd;
    public bool ChaptalGaveClue;
    public bool hasLabo;
    public bool hasPotion;

    public bool cleVespopped;
    public bool cleLabpopped;
    public bool cleBibpopped;

    private CinemachineVirtualCamera actualCam;

    public bool StopGame;

    private bool HascadenasSolved;



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


        if ((holdBib && AccessBibli) || (holdLab && AccessLabo) || (holdVes && AccesVestiaire)) { Cle.SetActive(true); }
        else { Cle.SetActive(false); }

        if (CadenasSolved && !HascadenasSolved)
        {

            Scalpel.SetActive(true);
            DoorClosed.SetActive(false);
            DoorOpen.SetActive(true);
            HascadenasSolved = true;

        }

        if (inCadenas)
        {

            inVestiaire = false;
            UiCadenas.gameObject.SetActive(true);

        }
        else
        {

            UiCadenas.gameObject.SetActive(false);

        }
        if (inSlideP)
        {

            inLabo = false;
            //UiCadenas.gameObject.SetActive(true);

        }
        else
        {

            //iCadenas.gameObject.SetActive(false);

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

        if (Input.touchCount > 0 && shoot == 0 && !StopGame) // fonction lancment de collect quand on appuie 
        {

            Collect();
            ShootTPin();
            ShootTableaux();
            ShootClefs();
            TalkToHippo();
            ShootCadenas();
            ShootRegister();
            ShootForBibli();
            ShootForCoffr();
            Debug.Log("Shoot");

        }

        if (Input.touchCount > 0 && !StopGame)
        {

            GiveTheItem();
            permatouch = Input.GetTouch(0);
            permaray = mainCam.ScreenPointToRay(touch.position);

        }



        #region ControlCam       


        if (Input.GetMouseButtonDown(0) && !StopGame)
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
                inSlideP = false;
                actualCam = VirtualCamLabo;
                MakePositionCam(VirtualCamLabo, pos);
                DontPathOverTheMax(VirtualCamLabo, dollyLabo);

            }
            //else if (inSlideP)
            //{
            //    inLabo = false;
            //    actualCam = VirtualCamSlideP;
            //    MakePositionCam(VirtualCamSlideP, pos);
            //    DontPathOverTheMax(VirtualCamSlideP, dollySlideP);

            //}
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

                Debug.Log("On essaye de parler à Hippo");

                if (GoneToLapeyronie && !LapeyronieSpoken)
                {

                    // ici on a parlé à hippocrate pour voir lapeyronie mais pas encore à lapeyronie
                    Debug.Log("Je n'ai plus rien à dire");
                    UiDialogue(HippoFace, "Je n'ai plus rien à te dire");

                }

                if (!GoneToLapeyronie && !LapeyronieSpoken)
                {
                    
                    // ici on a pas parlé à hippocrate ni à lapeyronie
                    Debug.Log("BlaBlaBla Va voir Lapyrouze");
                    UiDialogue(HippoFace, "Bonjour mon petit, je suis Hippocrate, le père de la médecine moderne! Quel bon vent t'amènes?\n\n-\n\nOh! Ton ami est malade et tu cherches des solutions dans la faculté de médecine pour essayer de l'aider?\n\n-\n\nEt bien je sais peut - être comment tu peux faire!\n\n-\n\nS'il a mal à l'abdomen, c'est un problème interne! Va voir ce tableau là - bas, Lapeyronie pourra sans doutes t'aider! C'est le premier chirurgien du roi Louis XV tu sais!");
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

                        RdyForLastDIalogue = true;
                        UiDialogue(LapeyronieFace, "AAAH ! Mon matériel ! Tu sais qu'il m'a servit à soigner Louis XV? Dans mon temps j'étais un grand chirurgien, j'ai même été président de l'académie royale de chirurgie ! \n\n-\n\nMerci bien jeune homme.\n\n-\n\nOh et pour ton ami, il devrait essayer de se mettre sur le dos, et de prendre une compresse chaude pour calmer la douleur. ");
                        StopGame = true;
                        TalkieDialogue.gameObject.SetActive(true);                        
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

                        UiDialogue(Pidouxface, "Oh ! Mon arrière petit-fils est devenu un célèbre fabuliste? Il a même écrit pour des Rois? Mais c'est merveilleux! Merci beaucoup, me voilà soulagé.. Merci du fond du cœur!\n\n-\n\n Pendant que tu cherchais j'ai réfléchit et il me semble que pour ton ami, un bandage autour de la plaie, désinfectée au préalable serait le plus adaptée à sa blessure.");
                        TalkieDialogue.gameObject.SetActive(true);
                        RdyForLastDialoguePid = true;                        
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
                        UiDialogue(Pidouxface, "Merci pour la potion ça fait plaiz");
                        ClearItem();

                    }
                }

                #endregion
            }
        }
    }

    IEnumerator AppearTheTalkie()
    {

        yield return new WaitForSeconds(1f);
        TalkieWalkieBtn.gameObject.SetActive(true);        

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


            if (hit.transform.gameObject.GetComponent<item>() /*&& hit.distance <= distanceMaxForGrab*/) // pn vérifie que l'objet n'est pas à l'autre bout de la MAP avec une disatnce max de grab
            {
                Debug.Log("It's In ");
                var TargetItemScript = hit.transform.gameObject.GetComponent<item>();

                ObjectCollected.sprite = TargetItemScript.picto;
                ObjectCollected.GetComponent<displaceTheItem>().itemIndex = TargetItemScript.itemIndex;

                hit.transform.gameObject.SetActive(false);
                
                
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
                    else
                    {

                        Debug.Log("pas la clé poiru la bilbi");
                        ShakeTheSam();
                        holdBib = false;

                    }
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
                    else
                    {

                        ShakeTheSam();
                        holdLab = false;

                    }

                }
            }


            if (permahit.transform.GetComponent<detectPorteHallPortrait>().GoToVestiaire)
            {
                if (holdVes)
                {
                    if (AccesVestiaire)
                    {

                        holdVes = false;
                    }
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
                    holdVes= true;
                    var textUI = hit.transform.GetComponent<Clef>().WhatUGat;
                    Debug.Log("Its a Key Vestiaire");
                    WhatUGot.text = textUI;
                    WhatUGot.gameObject.SetActive(true);
                    if (!cleVespopped)
                    {
                        ClefVestiaire.SetActive(false);
                        cleVespopped = true;
                    }

                }

                if (hit.transform.GetComponent<Clef>().KeyBibli)
                {

                    AccessBibli = true;
                    var textUI = hit.transform.GetComponent<Clef>().WhatUGat;
                    Debug.Log("Its a Key Bibli");
                    WhatUGot.text = textUI;
                    WhatUGot.gameObject.SetActive(true);
                    if (!cleBibpopped)
                    {
                        ClefBibli.SetActive(false);
                        cleBibpopped = true;
                    }
                }

                if (hit.transform.GetComponent<Clef>().KeyLabo)
                {

                    AccessLabo = true;
                    var textUI = hit.transform.GetComponent<Clef>().WhatUGat;
                    Debug.Log("Its a Key Bibli");
                    WhatUGot.text = textUI;
                    WhatUGot.gameObject.SetActive(true);
                    if (!cleLabpopped)
                    {
                        ClefLabo.SetActive(false);
                        cleLabpopped = true;
                    }

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
                        UiDialogue(LapeyronieFace, "zzZZZZ");

                    }

                    else
                    {

                        // ici hippocrate nous a dit d'aller voir lapeyronie mais on a pas encore parlé à lapeyronie

                        ClefVestiaire.gameObject.SetActive(true);

                        if (LapeyronieSpoken)
                        {

                            // ici hippocrate nous a dit d'aller voir lapeyronie, et on a déjà parlé à lapeyronie


                            if (!AccesVestiaire && LapeyronieSpoken)
                            {

                                // ici hippocrate nous a dit d'aller voir lapeyronie, et on a déjà parlé à lapeyronie et on a PAS récup la clef
                                //Debug.Log("Faudrait ptet récup la clef");
                                UiDialogue(LapeyronieFace, "Tu ne trouves pas la clé? Elle est dans le pot, dans le hall!");

                            }
                            if (AccesVestiaire)
                            {

                                if (HasVestiaire)
                                {
                                    if (CadenasSolved)
                                    {

                                        //Debug.Log("Le cadenas a bien été déverouillé, mais où est mon scalpel? ");
                                        UiDialogue(LapeyronieFace, "Ramène-moi mes outils bon sang!");

                                        if (RdyForLastDIalogue)
                                        {

                                            UiDialogue(LapeyronieFace, "Va Voir Pidouxxxx");
                                            LapeyronieEnd = true;

                                        }

                                    }
                                    else
                                    {

                                        // ici hippocrate nous a dit d'aller voir lapeyronie, et on a déjà parlé à lapeyronie et on a récup la clef et on est entrés dans les vestiaires, et on a pas déverouillé la cadenas
                                        //Debug.Log("Faut déverouiller le cadenas mtn");
                                        UiDialogue(LapeyronieFace, "Mon matériel n'est pas dans mon casier?");

                                    }
                                }
                                else
                                {

                                    // ici hippocrate nous a dit d'aller voir lapeyronie, et on a déjà parlé à lapeyronie et on a récup la clef
                                    //Debug.Log("Tu ferai bien d'aller aux vestiaires");
                                    UiDialogue(LapeyronieFace, "Mon casier était dans le vestiaire me semble-t-il.");

                                }
                            }
                        }
                        if (!LapeyronieSpoken)
                        {

                            // ici on a hippocrate nous a dit d'aller voir lapeyronie et nous n'avons pas parlé à lapeyronie
                            LapeyronieSpoken = true;
                            //Debug.Log("BLa Bla Bla voici la première Clef");
                            UiDialogue(LapeyronieFace, "Bonjour jeune homme, tu as besoin de mes conseils il me semble. Écoute, je veux bien t'aider mais seulement si tu fais quelque chose pour moi en retour. \n\n-\n\nDepuis que je suis dans ce tableau je n'ai pas eu accès a mes affaires, Ramène-moi mon scalpel préféré, il est jaune, et je t'aiderai avec Tonna mi.\n\n-\n\n Si tout va bien ils sont toujours stockés dans mon casier, la clé des vestiaires est caché dans le pot de fleur me semble-t-il. ");

                        }
                    }
                }
                else
                {

                    //Debug.Log("Ho mon beau scalpel, grave refait wola");
                    UiDialogue(LapeyronieFace, "/*AAAH ! Mon matériel ! Tu sais qu'il m'a servit à soigner Louis XV? Dans mon temps j'étais un grand chirurgien, j'ai même été président de l'académie royale de chirurgie ! \n\n-\n\nMerci bien jeune homme Oh et pour ton ami, il devrait essayer de se mettre sur le dos, et de prendre une compresse chaude pour calmer la douleur.\n\n - \n Ah, alors la cuisse c'est quelque peu délicat, je préfère ne pas dire de bêtises qui risquerait d'aggraver son cas. \n \n - \n Va plutôt voir François Pidoux, il saura être plus juste.Il a été le médecin de trois rois consécutifs, à commencer par Henri II! \n Bonne chance jeune homme et merci encore pour mon scalpel!");
                    

                }

            }

            #endregion

            #region PIDOUX

            if (hit.transform.gameObject.CompareTag("TableauPidoux"))
            {
                //Debug.Log("Vous tentez de parler à pidoux");

                if (LapeyronieEnd)
                {
                    if (PidouxEnd)
                    {

                        // ici on a fini avec pidoux et on lui parle
                        //Debug.Log("Je suis si fier de mon ptit fils");
                        UiDialogue(Pidouxface, "Oh ! Mon arrière petit-fils est devenu un célèbre fabuliste? Il a même écrit pour des Rois? Mais c'est merveilleux! Merci beaucoup, me voilà soulagé.. Merci du fond du cœur!\n\n-\n\n Pendant que tu cherchais j'ai réfléchit et il me semble que pour ton ami, un bandage autour de la plaie, désinfectée au préalable serait le plus adaptée à sa blessure.");

                    }
                    else
                    {
                        if (!PidouxGaveClue)
                        {

                            // ici lapeyronie nous a filé l'indice pour pidoux et on ne lui a pas encore parlé
                            //Debug.Log("Comment va ton ami ? Ok voici ce que tu dois faire ");
                            UiDialogue(Pidouxface, "Oh, bonjour mon petit, tu as l'air tout perturbé, ça va aller?\n\n-\n\nAh mince, ton ami a des ennuis. Je t'avoue que j'aurais besoin d'un petit service dans un premier temps \n\n-\n\nVois - tu, quelque chose me trotte dans la tête depuis ce qui me semble maintenant une éternité \n\n-\n\nJ'ai peur pour ma descendance. J'espère sincèrement du fond du cœur que mon nom n'est pas oublié, que ma famille a prospéré, qu'elle a continué à faire de grandes choses.\n\n-\n\nJ'ai besoin de savoir, alors si tu pouvais me rendre ce service, je vous aiderai avec plaisir toi et ton ami Peut-être dans la bibliothèque qui sait ?\n\n-\n\n Je te conseil de retourner dans des lieux que tu as déjà visité, tu trouveras surement la clé quelque part!");
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
                                    //Debug.Log("Faut Aller à la bibli mtn");
                                    UiDialogue(Pidouxface, "Tu trouveras des informations dans la bibliothèque");

                                }
                                else
                                {
                                    if (!hasBook)
                                    {

                                        // ici on a récup la clef et on a été à la bibli mais on a pas récup les bouquins.
                                        //Debug.Log("Faudrait récup les bouquins/ regarder");
                                        UiDialogue(Pidouxface, "As-tu bien cherché dans les livres à la bibliothèque?");

                                        if (RdyForLastDialoguePid)
                                        {

                                            UiDialogue(Pidouxface, "Va voir Chaptal");
                                            PidouxEnd = true;

                                        }

                                    }
                                }
                            }
                            else
                            {

                                // ici on a reçu l'indice mais nous ne sommes pas allés chercher la clef
                                //Debug.Log("Et si t'allais chercher la clef");
                                UiDialogue(Pidouxface, "La clé de la bibliothèque est sûrement dans un lieu que tu as déjà visité");

                            }
                        }
                    }
                }
                else
                {

                    //Debug.Log("Azy laissez moi dormir à zeubi");
                    UiDialogue(Pidouxface, "zzZZZZ");

                }
            }

            #endregion

            #region CHAPTAL

            if (hit.transform.gameObject.CompareTag("TableauChaptal"))
            {

                if (PidouxEnd)
                {
                    if (ChaptalEnd)
                    {

                        // ici nous parlons à chaptal et la quête est finie
                        Debug.Log("Oh quel plaisir cette ptite potion d'amour ! ");
                        UiDialogue(ChaptalFace, "Bien ouej pour la potion, tu vas pouvoir soigner ton ami!");

                    }
                    else
                    {

                        if (!ChaptalGaveClue)
                        {

                            // ici nous parlons à chaptal il nous a pas donné l'indice
                            //Debug.Log("Hello mon ptit pote, voici ton indice pour la prochaine salle");
                            UiDialogue(ChaptalFace, "Eh salut, quel bon vent t'amène? \n\n-\n\nOh ok je vois, Ca tombe bien je sais exactement comment t'aider! \n\n-\n\nHein? un service? mais non t'inquiète, va plutôt voir le laboratoire ! La clé ,si je dis pas de bêtises, elle est dans la bibliothèque ! \n\n-\n\nOula, en parlant du loup, il se passe du tumulte dans la bibliothèque!  Allez file, va sauver ton ami! ");
                            ChaptalGaveClue = true;
                            Tumult();
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
                                    //Debug.Log("faudrait ptet aller au labo non ?");
                                    UiDialogue(ChaptalFace, "Tu trouveras ta solution au labo!");

                                }
                                else
                                {

                                    // ici nous avons la clef et nous sommes allés au Labo
                                    if (!hasPotion)
                                    {

                                        // ici on est allés au labo mais on a pas récup la potion 
                                        //Debug.Log("Faudrait ptet récup la potion ");
                                        UiDialogue(ChaptalFace, "Le coffre de la potion est sûrement au fond du labo!");

                                    }
                                }
                            }
                            else
                            {

                                // ici on a parlé à chaptal mais nous n'avons pas choppé la clef
                                //Debug.Log("Faudrait ptet que tu récup la clef du labo");
                                UiDialogue(ChaptalFace, "La clé du Labo est dans la bibliothèque mon pote");

                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Laisse moi dormir zeubi");
                    UiDialogue(ChaptalFace, "ZzzzzZZzzz");

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
        float treshold = 0.15f;

        if (inTalkie || inCadenas) {
            fleched.gameObject.SetActive(false);
            flecheg.gameObject.SetActive(false);
        }

        if (inVestiaire || inBibli )
        {
            if (Mathf.Abs(maxPos - currentPos) < treshold)
            {
                flecheg.gameObject.SetActive(false);
            }
            else if (currentPos < treshold)
            {
                fleched.gameObject.SetActive(false);
            }
            else
            {
                fleched.gameObject.SetActive(true);
                flecheg.gameObject.SetActive(true);
            }
        }
        else
        {
            if (Mathf.Abs(maxPos - currentPos) < treshold)
            {
                fleched.gameObject.SetActive(false);
            }
            else if (currentPos < treshold)
            {
                flecheg.gameObject.SetActive(false);
            }
            else
            {
                fleched.gameObject.SetActive(true);
                flecheg.gameObject.SetActive(true);
            }
        }
    }


    // ---------------------------- DON'T Pass over the max Path -------------------------

    // ---------------------------- MOVE THE CAM -------------------------

    public void MakePositionCam(CinemachineVirtualCamera VirtualCam, Vector3 pos)
    {
        if ( inVestiaire || inLabo || inBibli )
        {
            if ( !StopGame)
            {

                VirtualCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += pos.x * dragSpeed;

            }
            

        }
        else if (inHall || inPortrait)
        {
            if (!StopGame)
                VirtualCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += pos.x * -dragSpeed;

        }
        

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
        BlackScreen.SetActive(true);
        
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

    // ---------------------------- DIALOGUE ---------------------------

    public void UiDialogue(Sprite DialogueFace, string TextDialogue )
    {

        Dialogue.gameObject.SetActive(true);
        facingDialogue.GetComponent<Image>().sprite = DialogueFace;
        TxtDialogue.text = TextDialogue;
        GuyNam.gameObject.SetActive(true);
        BtnSkip.SetActive(true);

        if ( DialogueFace == HippoFace)
        {

            GuyNam.gameObject.GetComponent<Image>().sprite = NameHippocrate;

        }

        if (DialogueFace == Pidouxface)
        {

            GuyNam.gameObject.GetComponent<Image>().sprite = NamePidoux;

        }

        if (DialogueFace == LapeyronieFace)
        {

            GuyNam.gameObject.GetComponent<Image>().sprite = NameLapeyronie;

        }

        if (DialogueFace == ChaptalFace)
        {

            GuyNam.gameObject.GetComponent<Image>().sprite = NameChaptal;

        }

        GuyNam.gameObject.SetActive(true);

        RectScroll.GetComponent<ScrollRect>().movementType = ScrollRect.MovementType.Elastic;
        Invoke("ResetPosition", 0.1f);
        StopGame = true;

    }


    // ---------------------------- DIALOGUE ---------------------------

    // ---------------------------- SHAKE ---------------------------

    public void ShakeTheSam()
    {

        actualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shakingForce;
        StartCoroutine("UnShake");

    }

    IEnumerator UnShake()
    {

        yield return new WaitForSeconds(TimeToShake);

        actualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;

    }

    // ---------------------------- SHAKE ---------------------------

    // ---------------------------- TALKIE ---------------------------

    public void AppearTalkie()
    {

        inTalkie = true;
        WalkieTalkie.SetActive(true);
        VirtualCamPortrait.gameObject.SetActive(false);
        VirtualCamTalkie.gameObject.SetActive(true);
        TalkieWalkieBtn.gameObject.SetActive(false);
        BlackScreen.gameObject.SetActive(true);
        StopGame = false;
        Dialogue.gameObject.SetActive(false);
        
    }
    public void BackFromTalkie()
    {

        inTalkie = false;
        WalkieTalkie.SetActive(false);
        VirtualCamPortrait.gameObject.SetActive(true);
        VirtualCamTalkie.gameObject.SetActive(false);
        TalkieWalkieBtn.gameObject.SetActive(false);
        BlackScreen.gameObject.SetActive(true);
        StopGame = false;
        inPortrait = true;
        BtnSkip.SetActive(true);
        UiDialogue(SameFace, "Ca y est tu m'entends? Tu as trouvé un moyen de m'aider? \n \n - \n Ah merci, je vais essayer ça tout de suite \n \n - \n Ca fait du bien, ça me soulage un peu.Merci beaucoup!\n \n Mais là je viens de m'ouvrir la cuisse en tombant, ça fait super mal et ça saigne pas mal, comment je fais?");
        if (RdyForLastDialoguePid)
        {

            UiDialogue(SameFace, "Hello, c'est bon tu m'entends? \n \n - \n Ok cool, c'est super que tu arrives à m'aider, merci mon pote! \n \n Je vais chercher dans mon placard et me bander la jambe!\n \n - \n Ok, ça piquait un peu mais là ça fait du bien, c'est super! \n \n - \nPar contre qu'est-ce que ma tête me fait mal!!\n \n J'ai des nausées c'est horrible..t'as pas une solution sous la main?\n \n j'ai l'impression que je vais tomber dans les pommes");

        }
        Invoke("TurnUpSkip", 3f);
        TalkieDialogue.gameObject.SetActive(false);
        VirtualCamPortrait.LookAt.SetPositionAndRotation(TargetPortraitRest.transform.position, Quaternion.identity);

    }

    // ---------------------------- TALKIE ---------------------------

    // ---------------------------- ShootCadenas ---------------------------

    public void ShootCadenas()
    {

        Touch touch = Input.GetTouch(0);
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.transform.gameObject.CompareTag("CADENAS"))
            {

                hit.transform.gameObject.GetComponent<detectPorteHallPortrait>().f_GoTo();

            }

        }

    }

    // ---------------------------- ShootCadenas ---------------------------

    // ---------------------------- REGISTER ---------------------------

    public void ShootRegister()
    {

        Touch touch = Input.GetTouch(0);
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.transform.gameObject.CompareTag("REGISTER"))
            {

                Registre.SetActive(true);
                inMenu = true;
                TheBlur.SetActive(true);

            }
        }
    }

    public void RegisterOut()
    {

        Registre.SetActive(false);
        inMenu = false;
        TheBlur.SetActive(false);

    }

    // ---------------------------- REGISTER ---------------------------

    // ---------------------------- OK FOR GAME ---------------------------

    public void OkForTheGame()
    {

        StopGame = false;       

    }

    public void ResetPosition()
    {
                
        RectScroll.GetComponent<ScrollRect>().movementType = ScrollRect.MovementType.Unrestricted;

    }

    // ---------------------------- OK FOR GAME ---------------------------

    // ---------------------------- OK FOR GAME ---------------------------

    public void TurnUpSkip()
    {

        BtnSkip.SetActive(true);

    }

    // ---------------------------- OK FOR GAME ---------------------------

    // ---------------------------- ShootBook ---------------------------

    public void ShootForBibli()
    {

        Touch touch = Input.GetTouch(0);
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.normal);

            if (hit.transform.gameObject.CompareTag("BookBibli1"))
            {

                Debug.Log("its genealogie");
                inMenu = true;
                TheBlur.SetActive(true);
                Livre.SetActive(true);
                Livre.GetComponent<Image>().sprite = Genealogie; 

            }

            if (hit.transform.gameObject.CompareTag("BookBibli2"))
            {

                inMenu = true;
                TheBlur.SetActive(true);
                Livre.SetActive(true);
                Livre.GetComponent<Image>().sprite = Fable;

            }

            if (hit.transform.gameObject.CompareTag("BookBibli3"))
            {

                inMenu = true;
                TheBlur.SetActive(true);
                Livre.SetActive(true);
                Livre.GetComponent<Image>().sprite = Random;

            }
        }

    }
    public void ShootForCoffr()
    {

        Touch touch = Input.GetTouch(0);
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.normal);

            if (hit.transform.gameObject.CompareTag("COFFRE"))
            {

                SceneManager.LoadScene("PuzzleSlide"); 

            }

        }

    }

    public void DisableBook()
    {

        inMenu = false;
        Livre.SetActive(false);
        TheBlur.SetActive(false);

    }


    // ---------------------------- ShootBook ---------------------------

    // ---------------------------- Tumulte ---------------------------

    public void Tumult()
    {

        BibliToFall.SetActive(false);
        BibliFalled.SetActive(true);
        ShakeTheSam();

    }

    // ---------------------------- Tumulte ---------------------------

}







