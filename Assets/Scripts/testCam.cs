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
    [SerializeField] GameObject TalkieWalkieBtn;
    [SerializeField] GameObject Registre;
    //VINEK VVVV
    [SerializeField] GameObject TPBIBLI;
    [SerializeField] GameObject TPLABO;
    [SerializeField] GameObject BlackScreen;

    [SerializeField] GameObject flecheg;
    [SerializeField] GameObject fleched;
    //FIN VINEK

    [Header("Dialogue UI")]
    [SerializeField] Sprite LapeyronieFace;
    [SerializeField] Sprite HippoFace;
    [SerializeField] Sprite Pidouxface;
    [SerializeField] Sprite ChaptalFace;
    [SerializeField] string Dodo;
    [SerializeField] string RecupeLaClefVestiaire;

    [SerializeField] Image ObjectCollected;
    [SerializeField] Text HippocrateSentence;
    [SerializeField] Text WhatUGot;
    
    [Header("VIRTUAL_CAM")]
    [SerializeField] CinemachineVirtualCamera VirtualCamHall;
    [SerializeField] CinemachineVirtualCamera VirtualCamPortrait;
    [SerializeField] CinemachineVirtualCamera VirtualCamVestiaire;
    [SerializeField] CinemachineVirtualCamera VirtualCamBibli;
    [SerializeField] CinemachineVirtualCamera VirtualCamLabo;
    [SerializeField] CinemachineVirtualCamera VirtualCamCadenas;
    [SerializeField] CinemachineVirtualCamera VirtualCamTalkie;
    [SerializeField] GameObject dollyHall;
    [SerializeField] GameObject dollyPortrait;
    [SerializeField] GameObject dollyVestiaire;
    [SerializeField] GameObject dollyBibli;
    [SerializeField] GameObject dollyLabo;
    [SerializeField] GameObject dollyCadenas;
    [SerializeField] GameObject dollyTalkie;

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

    [Header("This IsGame Object")]
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject DoorClosed;
    [SerializeField] GameObject DoorOpen;
    [SerializeField] GameObject WalkieTalkie;


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

    public bool inMenu; // ce bool permet de figer la cam�ra A UTILISER AVEC PRUDENCE 
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


        if (ObjectCollected.GetComponent<displaceTheItem>().HoldingItem)
        {

            camMoving = false;

        }

        if (Input.touchCount == 0) // zone arr�t raycast
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
                    UiDialogue(HippoFace, "Bonjour mon petit, qu'est-ce qui t'arrive? Oh ! Ton ami est malade et tu cherches des solutions dans la facult� de m�decine pour essayer de l'aider? Et bien je sais peut - �tre comment tu peux faire!");
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
                        UiDialogue(LapeyronieFace, "AAAH ! Mon mat�riel ! Tu sais qu'il m'a servit � soigner Louis XV? Dans mon temps j'�tais un grand chirurgien, j'ai m�me �t� pr�sident de l'acad�mie royale de chirurgie ! Merci bien jeune homme Oh et pour ton ami, il devrait essayer de se mettre sur le dos, et de prendre une compresse chaude pour calmer la douleur. ");
                        StopGame = true;
                        StartCoroutine("AppearTheTalkie");
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


            if (hit.transform.gameObject.GetComponent<item>() /*&& hit.distance <= distanceMaxForGrab*/) // pn v�rifie que l'objet n'est pas � l'autre bout de la MAP avec une disatnce max de grab
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
                        
                        Debug.Log("pas la cl� poiru la bilbi");
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
                        UiDialogue(LapeyronieFace, "zzZZZZ");

                    }

                    else
                    {

                        // ici hippocrate nous a dit d'aller voir lapeyronie mais on a pas encore parl� � lapeyronie

                        ClefVestiaire.gameObject.SetActive(true);

                        if (LapeyronieSpoken)
                        {

                            // ici hippocrate nous a dit d'aller voir lapeyronie, et on a d�j� parl� � lapeyronie


                            if (!AccesVestiaire && LapeyronieSpoken)
                            {

                                // ici hippocrate nous a dit d'aller voir lapeyronie, et on a d�j� parl� � lapeyronie et on a PAS r�cup la clef
                                //Debug.Log("Faudrait ptet r�cup la clef");
                                UiDialogue(LapeyronieFace, "Tu ne trouves pas la cl�? Elle est dans le pot, dans le hall!");

                            }
                            if (AccesVestiaire)
                            {

                                if (HasVestiaire)
                                {
                                    if (CadenasSolved)
                                    {

                                        //Debug.Log("Le cadenas a bien �t� d�verouill�, mais o� est mon scalpel? ");
                                        UiDialogue(LapeyronieFace, "Ram�ne-moi mes outils bon sang!");

                                    }
                                    else
                                    {

                                        // ici hippocrate nous a dit d'aller voir lapeyronie, et on a d�j� parl� � lapeyronie et on a r�cup la clef et on est entr�s dans les vestiaires, et on a pas d�verouill� la cadenas
                                        //Debug.Log("Faut d�verouiller le cadenas mtn");
                                        UiDialogue(LapeyronieFace, "Mon mat�riel n'est pas dans mon casier?");

                                    }
                                }
                                else
                                {

                                    // ici hippocrate nous a dit d'aller voir lapeyronie, et on a d�j� parl� � lapeyronie et on a r�cup la clef
                                    //Debug.Log("Tu ferai bien d'aller aux vestiaires");
                                    UiDialogue(LapeyronieFace, "Mon casier �tait dans le vestiaire me semble-t-il.");

                                }
                            }
                        }
                        if (!LapeyronieSpoken)
                        {

                            // ici on a hippocrate nous a dit d'aller voir lapeyronie et nous n'avons pas parl� � lapeyronie
                            LapeyronieSpoken = true;
                            //Debug.Log("BLa Bla Bla voici la premi�re Clef");
                            UiDialogue(LapeyronieFace, "Bonjour jeune homme, tu as besoin de mes conseils il me semble. �coute, je veux bien t'aider mais seulement si tu fais quelque chose pour moi en retour. Depuis que je suis dans ce tableau je n'ai pas eu acc�s a mes affaires, Ram�ne-moi mon scalpel pr�f�r�, il est jaune, et je t'aiderai avec Tonna mi. Si tout va bien ils sont toujours stock�s dans mon casier, la cl� des vestiaires est cach� dans le pot de fleur me semble-t-il. ");

                        }
                    }
                }
                else
                {

                    //Debug.Log("Ho mon beau scalpel, grave refait wola");
                    UiDialogue(LapeyronieFace, "AAAH ! Mon mat�riel ! Tu sais qu'il m'a servit � soigner Louis XV? Dans mon temps j'�tais un grand chirurgien, j'ai m�me �t� pr�sident de l'acad�mie royale de chirurgie ! Merci bien jeune homme Oh et pour ton ami, il devrait essayer de se mettre sur le dos, et de prendre une compresse chaude pour calmer la douleur. ");
                    

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
                        UiDialogue(Pidouxface, "Oh ! Mon arri�re petit-fils est devenu un c�l�bre fabuliste? Il a m�me �crit pour des Rois? Mais c'est merveilleux! Merci beaucoup, me voil� soulag�.. Merci du fond du c�ur! Pendant que tu cherchais j'ai r�fl�chit et il me semble que pour ton ami, un bandage autour de la plaie, d�sinfect�e au pr�alable serait le plus adapt�e � sa blessure.");

                    }
                    else
                    {
                        if (!PidouxGaveClue)
                        {

                            // ici lapeyronie nous a fil� l'indice pour pidoux et on ne lui a pas encore parl�
                            //Debug.Log("Comment va ton ami ? Ok voici ce que tu dois faire ");
                            UiDialogue(Pidouxface, "Oh, bonjour mon petit, tu as l'air tout perturb�, �a va aller?Ah mince, ton ami a des ennuis. Je t'avoue que j'aurais besoin d'un petit service dans un premier temps Vois - tu, quelque chose me trotte dans la t�te depuis ce qui me semble maintenant une �ternit� J'ai peur pour ma descendance. J'esp�re sinc�rement du fond du c�ur que mon nom n'est pas oubli�, que ma famille a prosp�r�, qu'elle a continu� � faire de grandes choses.J'ai besoin de savoir, alors si tu pouvais me rendre ce service, je vous aiderai avec plaisir toi et ton ami Peut-�tre dans la biblioth�que qui sait ? Je te conseil de retourner dans des lieux que tu as d�j� visit�, tu trouveras surement la cl� quelque part!");
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
                                    UiDialogue(Pidouxface, "Tu trouveras des informations dans la biblioth�que");

                                }
                                else
                                {
                                    if (!hasBook)
                                    {

                                        // ici on a r�cup la clef et on a �t� � la bibli mais on a pas r�cup les bouquins.
                                        //Debug.Log("Faudrait r�cup les bouquins/ regarder");
                                        UiDialogue(Pidouxface, "As-tu bien cherch� dans les livres � la biblioth�que?");

                                    }
                                }
                            }
                            else
                            {

                                // ici on a re�u l'indice mais nous ne sommes pas all�s chercher la clef
                                //Debug.Log("Et si t'allais chercher la clef");
                                UiDialogue(Pidouxface, "La cl� de la biblioth�que est s�rement dans un lieu que tu as d�j� visit�");

                            }
                        }
                    }
                }
                else
                {

                    //Debug.Log("Azy laissez moi dormir � zeubi");
                    UiDialogue(Pidouxface, "zzZZZZ");

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
                        UiDialogue(ChaptalFace, "Bien ouej pour la potion, tu vas pouvoir soigner ton ami!");

                    }
                    else
                    {

                        if (!ChaptalGaveClue)
                        {

                            // ici nous parlons � chaptal il nous a pas donn� l'indice
                            //Debug.Log("Hello mon ptit pote, voici ton indice pour la prochaine salle");
                            UiDialogue(ChaptalFace, "Eh salut, quel bon vent t'am�ne? Oh ok je vois, Ca tombe bien je sais exactement comment t'aider! hein? un service? mais non t'inqui�te, va plut�t voir le laboratoire ! La cl� ,si je dis pas de b�tises, elle est dans la biblioth�que ! oula, en parlant du loup, il se passe du tumulte dans la biblioth�que!  Allez file, va sauver ton ami! ");
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
                                    UiDialogue(ChaptalFace, "Tu trouveras ta solution au labo!");

                                }
                                else
                                {

                                    // ici nous avons la clef et nous sommes all�s au Labo
                                    if (!hasPotion)
                                    {

                                        // ici on est all�s au labo mais on a pas r�cup la potion 
                                        //Debug.Log("Faudrait ptet r�cup la potion ");
                                        UiDialogue(ChaptalFace, "Le coffre de la potion est s�rement au fond du labo!");

                                    }
                                }
                            }
                            else
                            {

                                // ici on a parl� � chaptal mais nous n'avons pas chopp� la clef
                                //Debug.Log("Faudrait ptet que tu r�cup la clef du labo");
                                UiDialogue(ChaptalFace, "La cl� du Labo est dans la biblioth�que mon pote");

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

        if (inVestiaire || inBibli /*|| inPortrait*/)
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
        if ( inVestiaire || inLabo || inBibli || inPortrait)
        {
            if ( !StopGame)
            {

                VirtualCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += pos.x * dragSpeed;

            }
            

        }
        else if (inHall)
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



            }
        }
    }

    // ---------------------------- REGISTER ---------------------------




    }







