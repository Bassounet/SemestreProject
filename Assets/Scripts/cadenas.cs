using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cadenas : MonoBehaviour
{
    [SerializeField] GameObject C1, C2, C3, C4;

    private int currentIndexC1 = 0, currentIndexC2 = 0, currentIndexC3 = 0, currentIndexC4 = 0;

    public int startingIndex =0;
    public string currentCode, MDP;
    private bool winned;

    [SerializeField] GameObject Player;
    [SerializeField] AudioSource Ads;
    [SerializeField] AudioClip Unlock;
    [SerializeField] AudioClip Turn;
    [SerializeField] AudioClip Correct;
    [SerializeField] AudioClip Wrongz;

    void Update()
    {

        #region DON'T MESS WITH NB

        if (currentIndexC1 > 9 )
        {

            currentIndexC1 = 0;

        }

        if ( currentIndexC1 < 0)
        {

            currentIndexC1 = 9;

        }

        if (currentIndexC2 > 9 )
        {

            currentIndexC2 = 0;

        }

        if (currentIndexC2 < 0)
        {

            currentIndexC2 = 9;

        }

        if (currentIndexC3 > 9 )
        {

            currentIndexC3 = 0;

        }

        if ( currentIndexC3 < 0)
        {

            currentIndexC3 = 9;

        }

        if (currentIndexC4 > 9 )
        {

            currentIndexC4 = 0;

        }

        if (currentIndexC4 < 0)
        {

            currentIndexC4 = 9;

        }

        #endregion

        currentCode = currentIndexC1.ToString() + currentIndexC2.ToString() + currentIndexC3.ToString() + currentIndexC4.ToString();

        if (winned)
        {

            Player.GetComponent<testCam>().CadenasSolved = true;

        }

    }

    public void turnRight(GameObject cellule)
    {
        
        cellule.transform.Rotate(Vector3.up * -36f);
        Ads.PlayOneShot(Turn);

    }

    public void turnLeft(GameObject cellule)
    {

        cellule.transform.Rotate(Vector3.up * 36f);
        Ads.PlayOneShot(Turn);

    }

    #region C1

    // ----------------------------------- *** C1 *** ----------------- // 

    public void turnC1R()
    {

        turnRight(C1);
        currentIndexC1--;

    }
    
    public void turnC1L()
    {

        turnLeft(C1);
        currentIndexC1++;

    }

    // ----------------------------------- *** C1 *** ----------------- // 

    #endregion

    #region C2

    // ----------------------------------- *** C2 *** ----------------- // 

    public void turnC2R()
    {

        turnRight(C2);
        currentIndexC2--;

    }

    public void turnC2L()
    {

        turnLeft(C2);
        currentIndexC2++;

    }

    // ----------------------------------- *** C2 *** ----------------- // 

    #endregion

    #region C3

    // ----------------------------------- *** C3 *** ----------------- // 

    public void turnC3R()
    {

        turnRight(C3);
        currentIndexC3--;

    }

    public void turnC3L()
    {

        turnLeft(C3);
        currentIndexC3++;

    }

    // ----------------------------------- *** C3 *** ----------------- // 

    #endregion

    #region C4

    // ----------------------------------- *** C4 *** ----------------- // 

    public void turnC4R()
    {

        turnRight(C4);
        currentIndexC4--;

    }

    public void turnC4L()
    {

        turnLeft(C4);
        currentIndexC4++;

    }

    // ----------------------------------- *** C4 *** ----------------- // 

#endregion

    public void Win()
    {

        Debug.Log("Win");
        winned = true;
        Player.GetComponent<testCam>().CadenasSolevd();
        Player.GetComponent<testCam>().backFromCadenas();
        Ads.PlayOneShot(Unlock);
        Ads.PlayOneShot(Correct);

    }
    
    public void Lose()
    {

        Debug.Log("Lose");
        Ads.PlayOneShot(Wrongz);

    }

    public void testCode()
    {


        if ( currentCode == MDP)
        {

            Win();


        }
        else
        {

            Lose();

        }

    }



}
