using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCam : MonoBehaviour
{


    // **************************** ///////////////////// DEV ZONE ////////////////// ************************** //
    bool camMoving;

    Vector3 endPosition;

    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    void Start()
    {
        camMoving = false;
    }
    

    void Update()
    {
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

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * -dragSpeed, 0);

        if (camMoving)
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



}
