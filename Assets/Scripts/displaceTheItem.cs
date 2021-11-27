using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class displaceTheItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    Vector2 BasePos;
    public bool ItsH;
    

    private void Start()
    {
        BasePos = transform.position;
        ItsH = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log("Gatcha");

        Touch fire = Input.GetTouch(0);
        //RaycastHit toucheur;
        Ray rayon = Camera.current.ScreenPointToRay(fire.position);

        //if (Physics.Raycast(rayon, out toucheur))
        //{

        //    Debug.DrawRay(transform.position, toucheur.point);


        //    if (toucheur.transform.gameObject.CompareTag("Hippocrate"))
        //    {

        //        ItsH = true;

        //    }
        //    else
        //    {

        //        ItsH = false;

        //    }

        //}



    }

    public void OnEndDrag(PointerEventData eventData) // quand on lâhce l'item après avoir sélectionné l'item.
    {       

        Debug.Log("End Drag");
        if ( !ItsH)
        {

            transform.position = BasePos;

        }

        if (ItsH)
        {

            transform.position = BasePos;
            Debug.Log("C'est H");

        }
        

    }
    
    public void OnBeginDrag(PointerEventData eventData) // quand on clique pour le sélectionner
    {

        Debug.Log("Go Drag");
                
    }
    
    public void OnDrag(PointerEventData eventData) // quand on a cliqué et que la souris se déplace
    {
        
        transform.position = Input.mousePosition;
        Debug.Log("Ca drag");

        

    }

    public void Update()
    {

        
    }

}

    

    

