using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    public string pos;

    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        if (pos == "Y")
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y, screenPoint.z));
        }
        if (pos == "X")
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, screenPoint.z));
        }
    }

    void OnMouseDrag()
    {
        if (pos == "Y")
        {
            Vector3 curScreenPoint = new Vector3(gameObject.transform.localPosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
        if (pos == "X")
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, gameObject.transform.localPosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }
    void OnMouseUp()
    {
        // If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Drag ended!");
        if (pos == "Y")
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, Mathf.Round(gameObject.transform.localPosition.y - 0.25f) + 0.5f, 0);
        }
        if (pos == "X")
        {
            gameObject.transform.localPosition = new Vector3(Mathf.Round(gameObject.transform.localPosition.x - 0.25f) + 0.5f, gameObject.transform.localPosition.y, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Block();
    }
    void Block()
    {
        //Y
        if (pos == "Y" && gameObject.transform.localPosition.y > -0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, -0.5f, 0);
        }
        if (pos == "Y" && gameObject.transform.localPosition.y < -4.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, -4.5f, 0);
        }
        //X
        if (pos == "X" && gameObject.transform.localPosition.x < 0.5f)
        {
            gameObject.transform.localPosition = new Vector3(0.5f, gameObject.transform.localPosition.y, 0);
        }
        if (pos == "X" && gameObject.transform.localPosition.x > 4.5f)
        {
            gameObject.transform.localPosition = new Vector3(4.5f, gameObject.transform.localPosition.y, 0);
        }
    }
}
