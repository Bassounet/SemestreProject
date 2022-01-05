using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public float TimeBeforeTalkieAppear;
    [SerializeField]GameObject TalkieWalkie;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitForClue");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForClue()
    {

        yield return new WaitForSeconds(TimeBeforeTalkieAppear);
        TalkieWalkie.SetActive(true);


    }

    public void GoToGame()
    {

        SceneManager.LoadScene("TestWalkieTalkie");

    }

      public void GoVibration()
    {

        Handheld.Vibrate();

    }
}
