using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class displaceTheItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    Vector2 BasePos;
    public bool HoldingItem;
    

    private void Start()
    {
        BasePos = transform.position;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log("Gatcha");       


    }

    public void OnEndDrag(PointerEventData eventData) // quand on l�hce l'item apr�s avoir s�lectionn� l'item.
    {       

        Debug.Log("End Drag");
        transform.position = BasePos;
        HoldingItem = false;

    }
    
    public void OnBeginDrag(PointerEventData eventData) // quand on clique pour le s�lectionner
    {

        Debug.Log("Go Drag");
                
    }
    
    public void OnDrag(PointerEventData eventData) // quand on a cliqu� et que la souris se d�place
    {
        
        transform.position = Input.mousePosition;
        Debug.Log("Ca drag");
        HoldingItem = true;

    }

    public void Update()
    {

        
    }

}

    

    

