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
    [SerializeField] GameObject TargetLookAt;


    bool inActualHall;

    public bool GoToVestiaire;
    public bool ThePortraix;
    public bool ToThehall;

    private void Start()
    {

        

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
        CamToEnable.LookAt.SetPositionAndRotation(TargetLookAt.transform.position, Quaternion.identity);
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

            Debug.Log("GoToPortrait");
            ScriptTestCam.gameObject.GetComponent<testCam>().inPortrait = true;

        }

        if (ToThehall)
        {

            Debug.Log("GoToHall");
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
