using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class displaceTheItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    Vector2 BasePos;
    public bool HoldingItem;
    public int itemIndex;
    [SerializeField] public Sprite SpriteNull;

    private void Start()
    {
        BasePos = transform.position;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log("Gatcha");       


    }

    public void OnEndDrag(PointerEventData eventData) // quand on lâhce l'item après avoir sélectionné l'item.
    {       

        Debug.Log("End Drag");
        transform.position = BasePos;
        HoldingItem = false;

    }
    
    public void OnBeginDrag(PointerEventData eventData) // quand on clique pour le sélectionner
    {

        Debug.Log("Go Drag");
                
    }
    
    public void OnDrag(PointerEventData eventData) // quand on a cliqué et que la souris se déplace
    {
        
        transform.position = Input.mousePosition;
        Debug.Log("Ca drag");
        HoldingItem = true;

    }

    public void Update()
    {

        
    }

}

    

    

