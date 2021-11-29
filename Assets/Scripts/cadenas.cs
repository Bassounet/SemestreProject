using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cadenas : MonoBehaviour
{
    [SerializeField] GameObject C1, C2, C3, C4;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void turnRight(GameObject cellule)
    {
        
        cellule.transform.Rotate(Vector3.up * 36f);
        
    }
    
    public void turnLeft(GameObject cellule)
    {

        cellule.transform.Rotate(Vector3.up * -36f);        

    }

    #region C1

    // ----------------------------------- *** C1 *** ----------------- // 

    public void turnC1R()
    {

        turnRight(C1);

    }
    
    public void turnC1L()
    {

        turnLeft(C1);

    }

    // ----------------------------------- *** C1 *** ----------------- // 

    #endregion

    #region C2

    // ----------------------------------- *** C2 *** ----------------- // 

    public void turnC2R()
    {

        turnRight(C2);

    }

    public void turnC2L()
    {

        turnLeft(C2);

    }

    // ----------------------------------- *** C2 *** ----------------- // 

    #endregion

    #region C3

    // ----------------------------------- *** C3 *** ----------------- // 

    public void turnC3R()
    {

        turnRight(C3);

    }

    public void turnC3L()
    {

        turnLeft(C3);

    }

    // ----------------------------------- *** C3 *** ----------------- // 

    #endregion

    #region C4

    // ----------------------------------- *** C4 *** ----------------- // 

    public void turnC4R()
    {

        turnRight(C4);

    }

    public void turnC4L()
    {

        turnLeft(C4);

    }

    // ----------------------------------- *** C4 *** ----------------- // 

#endregion




}
