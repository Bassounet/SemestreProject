using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    [Header("This IsGame Object")]
    [Tooltip(" ** DEV_ZONE ** Rentrez ici les games Objects dont vous avez besoins")]
    [SerializeField] Camera MainCam; // récup de la cam pour la faire tourner
    [SerializeField] Image InputPosition; // image de postiion Doigt

    [Header(" Réglages de vitesse ")]
    [Tooltip(" ** All_Zone ** -- > vitesse de rotation caméra ")]
    public float RotateSpeed; // vitesse de rotation de la caméra

    [Header("UI")]
    [Tooltip("** All_Zone ** -- > Gestion de l'UI")]
    [SerializeField] Image Slots1;
    
    








    void Start()
    {

    }


    void Update()
    {
        // ---------- INPUT ACTIVATION CERCLE BLANC ---------------- //

        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0); // création variable de touch pour récupérer l'index du doigt 1
            InputPosition.gameObject.SetActive(true); // on active l'image du canvas qu'on a récupéré au dessus
            InputPosition.transform.position = touch.position; // on associe la position à chaque tick de l'image à la position récupérée du doigt

        }
        else
        {

            InputPosition.gameObject.SetActive(false); // on désactive l'image

        }

        // ---------- INPUT ACTIVATION CERCLE BLANC FIN ---------------- //


        // ---------- UI ------------ //


        



    }





    // *************** FONTCTIONS DE CONTROLE CAMERA ROTATE *********************** // 


    public void TurnRight()
    {

        MainCam.transform.Rotate(Vector3.up * RotateSpeed);

    } 
    
    public void TurnLeft()
    {

        MainCam.transform.Rotate(Vector3.down * RotateSpeed);

    }

    // *************** FONTCTIONS DE CONTROLE CAMERA ROTATE *********************** // 


}
