using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class detectPorteHallPortrait : MonoBehaviour
{
    [SerializeField] Button GoToPortrait;
    [SerializeField] CinemachineVirtualCamera CamToDisable;
    [SerializeField] CinemachineVirtualCamera CamToEnable;
    [SerializeField] GameObject ScriptTestCam;

    bool inActualHall;

    public bool GoToVestiaire;
    public bool ThePortraix;
    public bool ToThehall;

    private void Start()
    {

        //inActualHall = ScriptTestCam.gameObject.GetComponent<testCam>().inHall;

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {

            GoToPortrait.gameObject.SetActive(true);

        }

    }

    public void f_GoTo()
    {

        CamToEnable.gameObject.SetActive(true);
        CamToDisable.gameObject.SetActive(false);
        GoToPortrait.gameObject.SetActive(false);        
        ScriptTestCam.gameObject.GetComponent<testCam>().inHall =! ScriptTestCam.gameObject.GetComponent<testCam>().inHall;
        

        if (GoToVestiaire)
        {

            Debug.Log("GoToVestiaire");
            ScriptTestCam.gameObject.GetComponent<testCam>().inVestiaire = true;

        }
        if (ThePortraix)
        {

            ScriptTestCam.gameObject.GetComponent<testCam>().inPortrait = true;

        }

        if (ToThehall)
        {

            ScriptTestCam.gameObject.GetComponent<testCam>().inPortrait = false;
            ScriptTestCam.gameObject.GetComponent<testCam>().inVestiaire = false;

        }

     
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            GoToPortrait.gameObject.SetActive(false);

        }

    }
}
