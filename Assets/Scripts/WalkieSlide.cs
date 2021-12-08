using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WalkieSlide : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    float Obj;

    int arrondi;
    // Start is called before the first frame update
    void Start()
    {

            Obj = Random.Range(-12.0f, 12.0f);
            Debug.Log("valeur:" + Obj.ToString());


    }

    // Update is called once per frame
    void Update()
    {
        Block();
        //Debug.Log(Mathf.Round(gameObject.transform.localPosition.x));

        //arrondi = (int)gameObject.transform.localPosition.x;
        //if ((float)arrondi== gameObject.transform.localPosition.x)
        //{Debug.Log(gameObject.transform.localPosition.x);}
        DetectWalk();
        Son();
    }
    void OnMouseDown()
    {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, screenPoint.z));
        
    }

    void OnMouseDrag()
    {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, 0, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;  
    }
    void Block()
    {
        if (gameObject.transform.localPosition.x > 12)
        {
            gameObject.transform.localPosition = new Vector3(12,gameObject.transform.localPosition.y, 0);
        }
        if (gameObject.transform.localPosition.x < -12)
        {
            gameObject.transform.localPosition = new Vector3(-12, gameObject.transform.localPosition.y, 0);
        }
    }
    void DetectWalk() {
        if (Obj - 0.2 < gameObject.transform.localPosition.x && gameObject.transform.localPosition.x < Obj + 0.2)
        {
            Debug.Log("YES");
        }
        else { Debug.Log(gameObject.transform.localPosition.x); }
    }

    void Son()
    {

    }
}
