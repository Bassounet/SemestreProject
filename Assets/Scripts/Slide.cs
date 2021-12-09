using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    [SerializeField] Material Fiole;
    [SerializeField] Material videY;
    [SerializeField] Material videX;
    public string pos;
    public bool fiole =false;
    bool Dragging = false;

    float BlockUp = -0.5f;
    float BlockDown = -4.5f;
    float BlockLeft = 0.5f;
    float BlockRight = 4.5f;

    float defUp;
    float defDown;
    float defLeft;
    float defRight;

    public float defX;
    public float defY;

    private Vector3 screenPoint;
    private Vector3 offset;

    private void Awake()
    {
        defUp=BlockUp;  
        defDown=BlockDown;
        defLeft=BlockLeft;
        defRight=BlockRight;

        if (pos == "Y")
        {
            defX = gameObject.transform.localPosition.x;
            gameObject.GetComponent<MeshRenderer>().material = videY;

        }
        if (pos == "X")
        {
            defY = gameObject.transform.localPosition.y;
            gameObject.GetComponent<MeshRenderer>().material = videX;
        }
        if (fiole)
        {
            //Texture verte
            gameObject.GetComponent<MeshRenderer>().material = Fiole;
            //Elle peut déborder en bas
            BlockDown -= 2;
            defDown -= 2;
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
        if (fiole && gameObject.transform.localPosition.y < -5 && !Dragging)
        {
            Debug.Log("win!");
        }
    }
    void Block()
    {
        //Y
        if (pos == "Y" && gameObject.transform.localPosition.y > BlockUp)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, BlockUp, 0);
        }
        if (pos == "Y" && gameObject.transform.localPosition.y < BlockDown)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, BlockDown, 0);
        }
        //X
        if (pos == "X" && gameObject.transform.localPosition.x < BlockLeft)
        {
            gameObject.transform.localPosition = new Vector3(BlockLeft, gameObject.transform.localPosition.y, 0);
        }
        if (pos == "X" && gameObject.transform.localPosition.x > BlockRight)
        {
            gameObject.transform.localPosition = new Vector3(BlockRight, gameObject.transform.localPosition.y, 0);
        }
        //FIGER SUR L'AXE
        if (pos == "Y")
        {
            gameObject.transform.localPosition = new Vector3(defX, gameObject.transform.localPosition.y, 0);
        }
        if (pos == "X")
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, defY, 0);
        }

    }
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
        Dragging = true;
        if (pos == "Y")
        {
            Vector3 curScreenPoint = new Vector3(defX, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
        if (pos == "X")
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, defY, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }
    void OnMouseUp()
    {
        // If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Drag ended!");
        Dragging = false;
        if (pos == "Y")
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, Mathf.Round(gameObject.transform.localPosition.y - 0.5f) + 0.5f, 0);
        }
        if (pos == "X")
        {
            gameObject.transform.localPosition = new Vector3(Mathf.Round(gameObject.transform.localPosition.x - 0.5f) + 0.5f, gameObject.transform.localPosition.y, 0);
        }
        BlockUp = defUp;
        BlockDown = defDown;
        BlockLeft = defLeft;
        BlockRight = defRight;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (CompareTag("Player") && Dragging)
        {
            if (pos == "X")
            {
                if (collision.gameObject.GetComponent<Slide>().pos == "Y")
                {
                    if (gameObject.transform.localPosition.x < collision.transform.localPosition.x)
                    {
                        BlockRight = collision.transform.localPosition.x - 1.5f;
                       // Debug.Log("from gauche");
                    }
                    if (gameObject.transform.localPosition.x > collision.transform.localPosition.x)
                    {
                        BlockLeft = collision.transform.localPosition.x + 1.5f;
                        //Debug.Log("from droite");
                    }
                }
                if(collision.gameObject.GetComponent<Slide>().pos == "X")
                {
                    if (gameObject.transform.localPosition.x < collision.transform.localPosition.x)
                    {
                        BlockRight = collision.transform.localPosition.x - 2f;
                        //Debug.Log("from gauche");
                    }
                    if (gameObject.transform.localPosition.x > collision.transform.localPosition.x)
                    {
                        BlockLeft = collision.transform.localPosition.x + 2f;
                        //Debug.Log("from droite");
                    }
                }
            }
            if (pos == "Y")
            {
                if (collision.gameObject.GetComponent<Slide>().pos == "X")
                {
                    if (gameObject.transform.localPosition.y < collision.transform.localPosition.y)
                    {
                        BlockUp = collision.transform.localPosition.y - 1.5f;
                        //Debug.Log("from down");
                    }
                    if (gameObject.transform.localPosition.y > collision.transform.localPosition.y)
                    {
                        BlockDown = collision.transform.localPosition.y + 1.5f;
                        //Debug.Log("from up");
                    }
                }

                if (collision.gameObject.GetComponent<Slide>().pos == "Y")
                {
                    if (gameObject.transform.localPosition.y < collision.transform.localPosition.y)
                    {
                        BlockUp = collision.transform.localPosition.y - 2f;
                        //Debug.Log("from down");
                    }
                    if (gameObject.transform.localPosition.y > collision.transform.localPosition.y)
                    {
                        BlockDown = collision.transform.localPosition.y + 2f;
                        //Debug.Log("from up");
                    }
                }
            }
        }
    }

}
