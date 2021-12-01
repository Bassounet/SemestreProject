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

    private void Start()
    {

        inActualHall = ScriptTestCam.gameObject.GetComponent<testCam>().inHall;

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {

            GoToPortrait.gameObject.SetActive(true);

        }

    }

    public void f_GoToPortrait()
    {

        CamToEnable.gameObject.SetActive(true);
        CamToDisable.gameObject.SetActive(false);
        GoToPortrait.gameObject.SetActive(false);        
        ScriptTestCam.gameObject.GetComponent<testCam>().inHall =! ScriptTestCam.gameObject.GetComponent<testCam>().inHall;

        if (!ScriptTestCam.gameObject.GetComponent<testCam>().inHall)
        {

            ScriptTestCam.GetComponent<testCam>().pathCam = 0f;

        }
        else
        {

            ScriptTestCam.GetComponent<testCam>().pathCam = 3.76f;

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
