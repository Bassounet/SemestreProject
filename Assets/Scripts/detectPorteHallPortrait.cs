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
    [SerializeField] GameObject Player;


    bool inActualHall;

    public bool GoToVestiaire;
    public bool ThePortraix;
    public bool ToThehall;
    public bool ToBibli;

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

        if (GoToVestiaire && Player.GetComponent<testCam>().AccesVestiaire )
        {

            SendMeToNextWay(ScriptTestCam, CamToEnable, CamToDisable, GoToPortrait);
            Debug.Log("GoToVestiaire");
            ScriptTestCam.gameObject.GetComponent<testCam>().inVestiaire = true;
            
        }

        if (ThePortraix )
        {

            SendMeToNextWay(ScriptTestCam, CamToEnable, CamToDisable, GoToPortrait);
            Debug.Log("GoToPortrait");
            ScriptTestCam.gameObject.GetComponent<testCam>().inPortrait = true;
            
        }

        if (ToBibli && Player.GetComponent<testCam>().AccessBibli )
        {

            SendMeToNextWay(ScriptTestCam, CamToEnable, CamToDisable, GoToPortrait);
            Debug.Log("GoToVestiaire");
            ScriptTestCam.gameObject.GetComponent<testCam>().inBibli = true;            

        }

        if (ToThehall)
        {

            SendMeToNextWay(ScriptTestCam, CamToEnable, CamToDisable, GoToPortrait);
            Debug.Log("GoToHall");
            ScriptTestCam.gameObject.GetComponent<testCam>().inPortrait = false;
            ScriptTestCam.gameObject.GetComponent<testCam>().inVestiaire = false;
            ScriptTestCam.gameObject.GetComponent<testCam>().inBibli = false;            

        }       

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            GoToPortrait.gameObject.SetActive(false);

        }

    }


    public void SendMeToNextWay(GameObject ScriptTestCam, CinemachineVirtualCamera CamToEnable, CinemachineVirtualCamera CamToDisable, Button GoToPortrait)
    {

        CamToEnable.gameObject.SetActive(true);
        CamToEnable.LookAt.SetPositionAndRotation(TargetLookAt.transform.position, Quaternion.identity);
        CamToDisable.gameObject.SetActive(false);
        GoToPortrait.gameObject.SetActive(false);
        ScriptTestCam.gameObject.GetComponent<testCam>().inHall = !ScriptTestCam.gameObject.GetComponent<testCam>().inHall;

    }
}
